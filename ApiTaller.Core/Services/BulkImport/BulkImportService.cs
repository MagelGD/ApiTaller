using ApiTaller.Core.Helpers;
using ApiTaller.Domain.Dtos.BulkImport;
using ApiTaller.Domain.Interfaces.Services.BulkImport;
using ApiTaller.Domain.Models;
using ApiTaller.Infrastructure.Data;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.BulkImport
{
    public class BulkImportService : IBulkImportService
    {
        private readonly DataContext _context;
        private readonly ILogger<BulkImportService> _logger;

        public BulkImportService(DataContext context, ILogger<BulkImportService> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region Product Templates & Import

        public async Task<byte[]> GenerateProductTemplateAsync(CancellationToken ct = default)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Productos");

            // Encabezados
            string[] headers = new[]
            {
                "Tipo de Producto *",
                "Código *",
                "Referencia",
                "Nombre del Producto *",
                "Descripción",
                "Precio Compra (Costo) *",
                "Precio Venta *",
                "Stock Inicial",
                "Stock Mínimo",
                "Aplica a (moto/car/both)"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                var cell = worksheet.Cell(1, i + 1);
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#1E293B");
                cell.Style.Font.FontColor = XLColor.White;
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }

            // Filas de Ejemplo Demostrativas
            object[][] sampleRows = new object[][]
            {
                new object[] { "Baterías", "BAT-12V-7A", "12V 7AH", "Batería Moura 12V 7Ah", "Batería sellada libre de mantenimiento", 85000, 125000, 10, 2, "both" },
                new object[] { "Iluminación", "LED-H4-45W", "H4-LED", "Bombillo LED H4 45W", "Luz alta y baja blanco frío 6500K", 32000, 55000, 15, 3, "both" }
            };

            for (int r = 0; r < sampleRows.Length; r++)
            {
                for (int c = 0; c < sampleRows[r].Length; c++)
                {
                    worksheet.Cell(r + 2, c + 1).Value = XLCellValue.FromObject(sampleRows[r][c]);
                }
            }

            // Validación desplegable en "Aplica a"
            worksheet.Range("J2:J1000").CreateDataValidation().List("moto, car, both", true);

            // Ajustar columnas
            worksheet.Columns().AdjustToContents();

            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            return memoryStream.ToArray();
        }

        public async Task<BulkImportResultDto> ImportProductsAsync(Stream fileStream, int responsibleUserId, bool dryRun = false, CancellationToken ct = default)
        {
            var result = new BulkImportResultDto();

            using var workbook = new XLWorkbook(fileStream);
            var worksheet = workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
            {
                result.Success = false;
                result.Message = "El archivo no contiene ninguna hoja de cálculo válida.";
                return result;
            }

            var rows = worksheet.RowsUsed().Skip(1).ToList();
            if (!rows.Any())
            {
                result.Success = false;
                result.Message = "El archivo está vacío o no contiene filas de datos para importar.";
                return result;
            }

            if (rows.Count > 1000)
            {
                result.Success = false;
                result.Message = "El archivo excede el límite máximo de 1.000 filas por importación.";
                return result;
            }

            result.TotalRows = rows.Count;

            // ─── PASADA 1: VALIDACIÓN PRELIMINAR (VALIDATE-ALL-FIRST) ───
            var existingTypes = await _context.ProductType
                .Where(pt => pt.IsActive)
                .ToListAsync(ct);

            var existingTypesMap = existingTypes
                .GroupBy(pt => TextNormalizationHelper.NormalizeForComparison(pt.Type))
                .ToDictionary(g => g.Key, g => g.First());

            var existingCodes = await _context.Product
                .Select(p => p.Code.Trim().ToLower())
                .ToListAsync(ct);
            var existingCodesSet = new HashSet<string>(existingCodes);

            var seenCodesInExcel = new Dictionary<string, int>(); // code -> rowNumber
            var newCategoriesNeeded = new Dictionary<string, string>(); // normKey -> displayName

            var validRowData = new List<ProductRowData>();

            for (int i = 0; i < rows.Count; i++)
            {
                var row = rows[i];
                int rowNumber = row.RowNumber(); // 2-indexed en Excel

                string rawType = row.Cell(1).GetString().Trim();
                string code = row.Cell(2).GetString().Trim();
                string reference = row.Cell(3).GetString().Trim();
                string name = row.Cell(4).GetString().Trim();
                string description = row.Cell(5).GetString().Trim();
                string rawPrice = row.Cell(6).GetString().Trim();
                string rawSalePrice = row.Cell(7).GetString().Trim();
                string rawStock = row.Cell(8).GetString().Trim();
                string rawMinStock = row.Cell(9).GetString().Trim();
                string vehicleType = row.Cell(10).GetString().Trim().ToLowerInvariant();

                // 1. Campos obligatorios
                if (string.IsNullOrWhiteSpace(rawType))
                    result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Tipo de Producto", ErrorMessage = "El tipo de producto es obligatorio." });

                if (string.IsNullOrWhiteSpace(code))
                    result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Código", ErrorMessage = "El código es obligatorio." });

                if (string.IsNullOrWhiteSpace(name))
                    result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Nombre del Producto", ErrorMessage = "El nombre del producto es obligatorio." });

                // 2. Precios
                decimal price = 0;
                if (string.IsNullOrWhiteSpace(rawPrice) || !TryParseDecimal(rawPrice, out price) || price < 0)
                {
                    result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Precio Compra", ErrorMessage = "El precio de compra debe ser un número mayor o igual a 0." });
                }

                decimal salePrice = 0;
                if (string.IsNullOrWhiteSpace(rawSalePrice) || !TryParseDecimal(rawSalePrice, out salePrice) || salePrice < 0)
                {
                    result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Precio Venta", ErrorMessage = "El precio de venta debe ser un número mayor o igual a 0." });
                }

                // 3. Stock
                int stockQuantity = 0;
                if (!string.IsNullOrWhiteSpace(rawStock) && (!int.TryParse(rawStock, out stockQuantity) || stockQuantity < 0))
                {
                    result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Stock Inicial", ErrorMessage = "El stock inicial debe ser un número entero mayor o igual a 0." });
                }

                int minStock = 0;
                if (!string.IsNullOrWhiteSpace(rawMinStock) && (!int.TryParse(rawMinStock, out minStock) || minStock < 0))
                {
                    result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Stock Mínimo", ErrorMessage = "El stock mínimo debe ser un número entero mayor o igual a 0." });
                }

                // 4. VehicleType
                if (string.IsNullOrWhiteSpace(vehicleType))
                {
                    vehicleType = "both";
                }
                else if (vehicleType != "moto" && vehicleType != "car" && vehicleType != "both")
                {
                    result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Aplica a", ErrorMessage = "El tipo de vehículo debe ser 'moto', 'car' o 'both'." });
                }

                // 5. Validación de Código Duplicado dentro del Excel
                if (!string.IsNullOrWhiteSpace(code))
                {
                    string codeKey = code.ToLowerInvariant();
                    if (seenCodesInExcel.TryGetValue(codeKey, out int previousRow))
                    {
                        result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Código", ErrorMessage = $"El código '{code}' está duplicado en este archivo (aparece primero en la fila {previousRow})." });
                    }
                    else
                    {
                        seenCodesInExcel[codeKey] = rowNumber;
                    }

                    // 6. Validación de Código Duplicado contra BD
                    if (existingCodesSet.Contains(codeKey))
                    {
                        result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Código", ErrorMessage = $"El código '{code}' ya existe en el catálogo de este taller." });
                    }
                }

                // 7. Agrupar categorías nuevas si no hubo error en Tipo de Producto
                if (!string.IsNullOrWhiteSpace(rawType))
                {
                    string normType = TextNormalizationHelper.NormalizeForComparison(rawType);
                    if (!existingTypesMap.ContainsKey(normType) && !newCategoriesNeeded.ContainsKey(normType))
                    {
                        newCategoriesNeeded[normType] = TextNormalizationHelper.ToCleanTitleCase(rawType);
                    }
                }

                validRowData.Add(new ProductRowData
                {
                    RowNumber = rowNumber,
                    RawType = rawType,
                    Code = code,
                    Reference = reference,
                    Name = name,
                    Description = description,
                    Price = price,
                    SalePrice = salePrice,
                    StockQuantity = stockQuantity,
                    MinStock = minStock,
                    VehicleType = vehicleType
                });
            }

            // Si se detectaron errores, abortar sin tocar la base de datos
            if (result.Errors.Any())
            {
                result.Success = false;
                result.ErrorCount = result.Errors.Count;
                result.SuccessCount = 0;
                result.Message = $"Se encontraron {result.Errors.Count} error(es) de validación. Ningún registro fue insertado.";
                return result;
            }

            if (dryRun)
            {
                result.Success = true;
                result.SuccessCount = validRowData.Count;
                result.CreatedCategoriesCount = newCategoriesNeeded.Count;
                result.Message = $"Archivo válido. Se detectaron {validRowData.Count} producto(s) listos para importar.";
                return result;
            }

            // ─── PASADA 2: INSERCIÓN ATÓMICA EN TRANSACCIÓN ───
            using var transaction = await _context.Database.BeginTransactionAsync(ct);
            try
            {
                // 1. Crear categorías nuevas necesarias
                int createdCategories = 0;
                foreach (var kvp in newCategoriesNeeded)
                {
                    var newProductType = new ProductType
                    {
                        Type = kvp.Value,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        ResponsibleUserId = responsibleUserId
                    };
                    _context.ProductType.Add(newProductType);
                    createdCategories++;
                }

                if (createdCategories > 0)
                {
                    await _context.SaveChangesAsync(ct);
                }

                // 2. Recargar mapa de tipos
                var allTypes = await _context.ProductType
                    .Where(pt => pt.IsActive)
                    .ToListAsync(ct);
                var allTypesMap = allTypes
                    .GroupBy(pt => TextNormalizationHelper.NormalizeForComparison(pt.Type))
                    .ToDictionary(g => g.Key, g => g.First());

                // 3. Crear productos
                var productsToInsert = new List<(Product product, int stockQuantity, int minStock)>();
                foreach (var rowData in validRowData)
                {
                    string normType = TextNormalizationHelper.NormalizeForComparison(rowData.RawType);
                    int productTypeId = allTypesMap[normType].Id;

                    var product = new Product
                    {
                        ProducTypeId = productTypeId, // Respetando el typo de la entidad
                        ProductName = rowData.Name,
                        Code = rowData.Code,
                        Reference = rowData.Reference,
                        Description = rowData.Description,
                        Price = rowData.Price,
                        SalePrice = rowData.SalePrice,
                        VehicleType = rowData.VehicleType,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        ResponsibleUserId = responsibleUserId
                    };

                    _context.Product.Add(product);
                    productsToInsert.Add((product, rowData.StockQuantity, rowData.MinStock));
                }

                await _context.SaveChangesAsync(ct);

                // 4. Crear registros en Inventory para cada producto
                foreach (var item in productsToInsert)
                {
                    var inventory = new ApiTaller.Domain.Models.Inventory
                    {
                        ProductId = item.product.Id,
                        StockQuantity = item.stockQuantity,
                        MinStock = item.minStock,
                        LastUpdate = DateTime.UtcNow,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        ResponsibleUserId = responsibleUserId
                    };
                    _context.Inventory.Add(inventory);
                }

                await _context.SaveChangesAsync(ct);
                await transaction.CommitAsync(ct);

                result.Success = true;
                result.SuccessCount = productsToInsert.Count;
                result.ErrorCount = 0;
                result.CreatedCategoriesCount = createdCategories;
                result.Message = $"Se importaron exitosamente {productsToInsert.Count} productos y se crearon {createdCategories} categorías nuevas.";
                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                _logger.LogError(ex, "Error al ejecutar la transacción de importación masiva de productos");
                result.Success = false;
                result.Message = "Ocurrió un error interno al guardar los registros en base de datos. Se canceló la transacción.";
                return result;
            }
        }

        private class ProductRowData
        {
            public int RowNumber { get; set; }
            public string RawType { get; set; } = string.Empty;
            public string Code { get; set; } = string.Empty;
            public string Reference { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public decimal SalePrice { get; set; }
            public int StockQuantity { get; set; }
            public int MinStock { get; set; }
            public string VehicleType { get; set; } = "both";
        }

        #endregion

        #region Product Type Templates & Import

        public async Task<byte[]> GenerateProductTypeTemplateAsync(CancellationToken ct = default)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Tipos de Producto");

            worksheet.Cell(1, 1).Value = "Nombre del Tipo de Producto *";
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#1E293B");
            worksheet.Cell(1, 1).Style.Font.FontColor = XLColor.White;

            worksheet.Cell(2, 1).Value = "Baterías";
            worksheet.Cell(3, 1).Value = "Iluminación";
            worksheet.Cell(4, 1).Value = "Sensores y Relés";

            worksheet.Columns().AdjustToContents();

            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            return memoryStream.ToArray();
        }

        public async Task<BulkImportResultDto> ImportProductTypesAsync(Stream fileStream, int responsibleUserId, bool dryRun = false, CancellationToken ct = default)
        {
            var result = new BulkImportResultDto();

            using var workbook = new XLWorkbook(fileStream);
            var worksheet = workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
            {
                result.Success = false;
                result.Message = "El archivo no contiene ninguna hoja de cálculo válida.";
                return result;
            }

            var rows = worksheet.RowsUsed().Skip(1).ToList();
            if (!rows.Any())
            {
                result.Success = false;
                result.Message = "El archivo está vacío.";
                return result;
            }

            result.TotalRows = rows.Count;

            var existingTypes = await _context.ProductType
                .Where(pt => pt.IsActive)
                .ToListAsync(ct);

            var existingSet = new HashSet<string>(existingTypes.Select(t => TextNormalizationHelper.NormalizeForComparison(t.Type)));
            var seenInExcel = new HashSet<string>();
            var typesToCreate = new List<string>();

            for (int i = 0; i < rows.Count; i++)
            {
                var row = rows[i];
                int rowNumber = row.RowNumber();
                string typeName = row.Cell(1).GetString().Trim();

                if (string.IsNullOrWhiteSpace(typeName))
                {
                    result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Nombre", ErrorMessage = "El nombre del tipo es obligatorio." });
                    continue;
                }

                string norm = TextNormalizationHelper.NormalizeForComparison(typeName);
                if (existingSet.Contains(norm) || seenInExcel.Contains(norm))
                {
                    result.SkippedDuplicates++;
                    continue;
                }

                seenInExcel.Add(norm);
                typesToCreate.Add(TextNormalizationHelper.ToCleanTitleCase(typeName));
            }

            if (result.Errors.Any())
            {
                result.Success = false;
                result.ErrorCount = result.Errors.Count;
                result.Message = $"Se encontraron {result.Errors.Count} errores. No se insertó ningún tipo de producto.";
                return result;
            }

            if (dryRun)
            {
                result.Success = true;
                result.SuccessCount = typesToCreate.Count;
                result.Message = $"Archivo válido. Se detectaron {typesToCreate.Count} tipo(s) de producto nuevos para importar.";
                return result;
            }

            using var transaction = await _context.Database.BeginTransactionAsync(ct);
            try
            {
                foreach (var name in typesToCreate)
                {
                    _context.ProductType.Add(new ProductType
                    {
                        Type = name,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        ResponsibleUserId = responsibleUserId
                    });
                }

                await _context.SaveChangesAsync(ct);
                await transaction.CommitAsync(ct);

                result.Success = true;
                result.SuccessCount = typesToCreate.Count;
                result.Message = $"Se importaron exitosamente {typesToCreate.Count} tipos de producto ({result.SkippedDuplicates} omitidos por ya existir).";
                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                _logger.LogError(ex, "Error importando tipos de producto");
                result.Success = false;
                result.Message = "Ocurrió un error al guardar en base de datos.";
                return result;
            }
        }

        #endregion

        #region Service Catalog Templates & Import

        public async Task<byte[]> GenerateServiceCatalogTemplateAsync(CancellationToken ct = default)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Servicios");

            string[] headers = new[]
            {
                "Tipo de Servicio *",
                "Nombre del Servicio *",
                "Descripción",
                "Precio Mano de Obra *",
                "Tiempo Estimado (Minutos)",
                "Aplica a (moto/car/both)"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                var cell = worksheet.Cell(1, i + 1);
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#1E293B");
                cell.Style.Font.FontColor = XLColor.White;
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }

            object[][] sampleRows = new object[][]
            {
                new object[] { "Diagnóstico Eléctrico", "Revisión de Sistema de Carga", "Medición de alternador, regulador y batería con multímetro", 35000, 45, "both" },
                new object[] { "Instalaciones", "Instalación de Alarma / GPS", "Conexión a ramal eléctrico y corte de corriente", 50000, 60, "both" }
            };

            for (int r = 0; r < sampleRows.Length; r++)
            {
                for (int c = 0; c < sampleRows[r].Length; c++)
                {
                    worksheet.Cell(r + 2, c + 1).Value = XLCellValue.FromObject(sampleRows[r][c]);
                }
            }

            worksheet.Range("F2:F1000").CreateDataValidation().List("moto, car, both", true);
            worksheet.Columns().AdjustToContents();

            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            return memoryStream.ToArray();
        }

        public async Task<BulkImportResultDto> ImportServiceCatalogsAsync(Stream fileStream, int responsibleUserId, bool dryRun = false, CancellationToken ct = default)
        {
            var result = new BulkImportResultDto();

            using var workbook = new XLWorkbook(fileStream);
            var worksheet = workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
            {
                result.Success = false;
                result.Message = "El archivo no contiene ninguna hoja de cálculo válida.";
                return result;
            }

            var rows = worksheet.RowsUsed().Skip(1).ToList();
            if (!rows.Any())
            {
                result.Success = false;
                result.Message = "El archivo está vacío.";
                return result;
            }

            result.TotalRows = rows.Count;

            // ─── PASADA 1: VALIDACIÓN PRELIMINAR ───
            var existingTypes = await _context.ServiceType
                .Where(st => st.IsActive)
                .ToListAsync(ct);

            var existingTypesMap = existingTypes
                .GroupBy(st => TextNormalizationHelper.NormalizeForComparison(st.Name))
                .ToDictionary(g => g.Key, g => g.First());

            var existingServices = await _context.ServiceCatalog
                .Where(sc => sc.IsActive)
                .Select(sc => sc.Name.Trim().ToLower())
                .ToListAsync(ct);
            var existingServicesSet = new HashSet<string>(existingServices);

            var seenServicesInExcel = new Dictionary<string, int>();
            var newCategoriesNeeded = new Dictionary<string, string>();
            var validRowData = new List<ServiceCatalogRowData>();

            for (int i = 0; i < rows.Count; i++)
            {
                var row = rows[i];
                int rowNumber = row.RowNumber();

                string rawType = row.Cell(1).GetString().Trim();
                string name = row.Cell(2).GetString().Trim();
                string description = row.Cell(3).GetString().Trim();
                string rawPrice = row.Cell(4).GetString().Trim();
                string rawMinutes = row.Cell(5).GetString().Trim();
                string vehicleType = row.Cell(6).GetString().Trim().ToLowerInvariant();

                if (string.IsNullOrWhiteSpace(rawType))
                    result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Tipo de Servicio", ErrorMessage = "El tipo de servicio es obligatorio." });

                if (string.IsNullOrWhiteSpace(name))
                    result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Nombre del Servicio", ErrorMessage = "El nombre del servicio es obligatorio." });

                decimal defaultPrice = 0;
                if (string.IsNullOrWhiteSpace(rawPrice) || !TryParseDecimal(rawPrice, out defaultPrice) || defaultPrice < 0)
                {
                    result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Precio Mano de Obra", ErrorMessage = "El precio de mano de obra debe ser un número mayor o igual a 0." });
                }

                int defaultMinutes = 30;
                if (!string.IsNullOrWhiteSpace(rawMinutes) && (!int.TryParse(rawMinutes, out defaultMinutes) || defaultMinutes <= 0))
                {
                    result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Tiempo Estimado", ErrorMessage = "El tiempo estimado debe ser un número entero de minutos mayor a 0." });
                }

                if (string.IsNullOrWhiteSpace(vehicleType))
                {
                    vehicleType = "both";
                }
                else if (vehicleType != "moto" && vehicleType != "car" && vehicleType != "both")
                {
                    result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Aplica a", ErrorMessage = "El tipo de vehículo debe ser 'moto', 'car' o 'both'." });
                }

                // Duplicados
                if (!string.IsNullOrWhiteSpace(name))
                {
                    string normName = TextNormalizationHelper.NormalizeForComparison(name);
                    if (seenServicesInExcel.TryGetValue(normName, out int prevRow))
                    {
                        result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Nombre del Servicio", ErrorMessage = $"El servicio '{name}' está duplicado en este archivo (aparece primero en la fila {prevRow})." });
                    }
                    else
                    {
                        seenServicesInExcel[normName] = rowNumber;
                    }

                    if (existingServicesSet.Contains(name.ToLowerInvariant()))
                    {
                        result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Nombre del Servicio", ErrorMessage = $"El servicio '{name}' ya existe en el catálogo de este taller." });
                    }
                }

                if (!string.IsNullOrWhiteSpace(rawType))
                {
                    string normType = TextNormalizationHelper.NormalizeForComparison(rawType);
                    if (!existingTypesMap.ContainsKey(normType) && !newCategoriesNeeded.ContainsKey(normType))
                    {
                        newCategoriesNeeded[normType] = TextNormalizationHelper.ToCleanTitleCase(rawType);
                    }
                }

                validRowData.Add(new ServiceCatalogRowData
                {
                    RowNumber = rowNumber,
                    RawType = rawType,
                    Name = name,
                    Description = description,
                    DefaultPrice = defaultPrice,
                    DefaultMinutes = defaultMinutes,
                    VehicleType = vehicleType
                });
            }

            if (result.Errors.Any())
            {
                result.Success = false;
                result.ErrorCount = result.Errors.Count;
                result.SuccessCount = 0;
                result.Message = $"Se encontraron {result.Errors.Count} error(es) de validación. Ningún servicio fue insertado.";
                return result;
            }

            if (dryRun)
            {
                result.Success = true;
                result.SuccessCount = validRowData.Count;
                result.CreatedCategoriesCount = newCategoriesNeeded.Count;
                result.Message = $"Archivo válido. Se detectaron {validRowData.Count} servicio(s) listos para importar.";
                return result;
            }

            // ─── PASADA 2: INSERCIÓN ATÓMICA ───
            using var transaction = await _context.Database.BeginTransactionAsync(ct);
            try
            {
                int createdCategories = 0;
                foreach (var kvp in newCategoriesNeeded)
                {
                    var newServiceType = new ServiceType
                    {
                        Name = kvp.Value,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        ResponsibleUserId = responsibleUserId
                    };
                    _context.ServiceType.Add(newServiceType);
                    createdCategories++;
                }

                if (createdCategories > 0)
                {
                    await _context.SaveChangesAsync(ct);
                }

                var allTypes = await _context.ServiceType
                    .Where(st => st.IsActive)
                    .ToListAsync(ct);
                var allTypesMap = allTypes
                    .GroupBy(st => TextNormalizationHelper.NormalizeForComparison(st.Name))
                    .ToDictionary(g => g.Key, g => g.First());

                foreach (var rowData in validRowData)
                {
                    string normType = TextNormalizationHelper.NormalizeForComparison(rowData.RawType);
                    int serviceTypeId = allTypesMap[normType].Id;

                    var serviceCatalog = new ServiceCatalog
                    {
                        ServiceTypeId = serviceTypeId,
                        Name = rowData.Name,
                        Description = rowData.Description,
                        DefaultPrice = rowData.DefaultPrice,
                        DefaultMinutes = rowData.DefaultMinutes,
                        TimeUnit = "minutes",
                        VehicleType = rowData.VehicleType,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        ResponsibleUserId = responsibleUserId
                    };

                    _context.ServiceCatalog.Add(serviceCatalog);
                }

                await _context.SaveChangesAsync(ct);
                await transaction.CommitAsync(ct);

                result.Success = true;
                result.SuccessCount = validRowData.Count;
                result.ErrorCount = 0;
                result.CreatedCategoriesCount = createdCategories;
                result.Message = $"Se importaron exitosamente {validRowData.Count} servicios y se crearon {createdCategories} tipos de servicio nuevos.";
                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                _logger.LogError(ex, "Error importando catálogo de servicios");
                result.Success = false;
                result.Message = "Ocurrió un error interno al guardar en base de datos.";
                return result;
            }
        }

        private class ServiceCatalogRowData
        {
            public int RowNumber { get; set; }
            public string RawType { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public decimal DefaultPrice { get; set; }
            public int DefaultMinutes { get; set; }
            public string VehicleType { get; set; } = "both";
        }

        #endregion

        #region Service Type Templates & Import

        public async Task<byte[]> GenerateServiceTypeTemplateAsync(CancellationToken ct = default)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Tipos de Servicio");

            worksheet.Cell(1, 1).Value = "Nombre del Tipo de Servicio *";
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#1E293B");
            worksheet.Cell(1, 1).Style.Font.FontColor = XLColor.White;

            worksheet.Cell(2, 1).Value = "Diagnóstico Eléctrico";
            worksheet.Cell(3, 1).Value = "Instalaciones y Accesorios";
            worksheet.Cell(4, 1).Value = "Mantenimiento Preventivo";

            worksheet.Columns().AdjustToContents();

            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            return memoryStream.ToArray();
        }

        public async Task<BulkImportResultDto> ImportServiceTypesAsync(Stream fileStream, int responsibleUserId, bool dryRun = false, CancellationToken ct = default)
        {
            var result = new BulkImportResultDto();

            using var workbook = new XLWorkbook(fileStream);
            var worksheet = workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
            {
                result.Success = false;
                result.Message = "El archivo no contiene ninguna hoja de cálculo válida.";
                return result;
            }

            var rows = worksheet.RowsUsed().Skip(1).ToList();
            if (!rows.Any())
            {
                result.Success = false;
                result.Message = "El archivo está vacío.";
                return result;
            }

            result.TotalRows = rows.Count;

            var existingTypes = await _context.ServiceType
                .Where(st => st.IsActive)
                .ToListAsync(ct);

            var existingSet = new HashSet<string>(existingTypes.Select(t => TextNormalizationHelper.NormalizeForComparison(t.Name)));
            var seenInExcel = new HashSet<string>();
            var typesToCreate = new List<string>();

            for (int i = 0; i < rows.Count; i++)
            {
                var row = rows[i];
                int rowNumber = row.RowNumber();
                string typeName = row.Cell(1).GetString().Trim();

                if (string.IsNullOrWhiteSpace(typeName))
                {
                    result.Errors.Add(new BulkImportErrorDto { RowNumber = rowNumber, Field = "Nombre", ErrorMessage = "El nombre del tipo es obligatorio." });
                    continue;
                }

                string norm = TextNormalizationHelper.NormalizeForComparison(typeName);
                if (existingSet.Contains(norm) || seenInExcel.Contains(norm))
                {
                    result.SkippedDuplicates++;
                    continue;
                }

                seenInExcel.Add(norm);
                typesToCreate.Add(TextNormalizationHelper.ToCleanTitleCase(typeName));
            }

            if (result.Errors.Any())
            {
                result.Success = false;
                result.ErrorCount = result.Errors.Count;
                result.Message = $"Se encontraron {result.Errors.Count} errores. No se insertó ningún tipo de servicio.";
                return result;
            }

            if (dryRun)
            {
                result.Success = true;
                result.SuccessCount = typesToCreate.Count;
                result.Message = $"Archivo válido. Se detectaron {typesToCreate.Count} tipo(s) de servicio nuevos para importar.";
                return result;
            }

            using var transaction = await _context.Database.BeginTransactionAsync(ct);
            try
            {
                foreach (var name in typesToCreate)
                {
                    _context.ServiceType.Add(new ServiceType
                    {
                        Name = name,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        ResponsibleUserId = responsibleUserId
                    });
                }

                await _context.SaveChangesAsync(ct);
                await transaction.CommitAsync(ct);

                result.Success = true;
                result.SuccessCount = typesToCreate.Count;
                result.Message = $"Se importaron exitosamente {typesToCreate.Count} tipos de servicio ({result.SkippedDuplicates} omitidos por ya existir).";
                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                _logger.LogError(ex, "Error importando tipos de servicio");
                result.Success = false;
                result.Message = "Ocurrió un error al guardar en base de datos.";
                return result;
            }
        }

        #endregion

        #region Helpers

        private static bool TryParseDecimal(string value, out decimal result)
        {
            // Aceptar punto o coma decimal
            value = value.Replace("$", "").Replace(" ", "").Trim();
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return true;
            if (decimal.TryParse(value, NumberStyles.Any, new CultureInfo("es-CO"), out result))
                return true;
            return decimal.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out result);
        }

        #endregion
    }
}
