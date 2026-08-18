using ApiTaller.Domain.Dtos.BulkImport;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.BulkImport
{
    public interface IBulkImportService
    {
        // Productos
        Task<byte[]> GenerateProductTemplateAsync(CancellationToken ct = default);
        Task<BulkImportResultDto> ImportProductsAsync(Stream fileStream, int responsibleUserId, CancellationToken ct = default);

        // Tipos de Producto
        Task<byte[]> GenerateProductTypeTemplateAsync(CancellationToken ct = default);
        Task<BulkImportResultDto> ImportProductTypesAsync(Stream fileStream, int responsibleUserId, CancellationToken ct = default);

        // Catálogo de Servicios
        Task<byte[]> GenerateServiceCatalogTemplateAsync(CancellationToken ct = default);
        Task<BulkImportResultDto> ImportServiceCatalogsAsync(Stream fileStream, int responsibleUserId, CancellationToken ct = default);

        // Tipos de Servicio
        Task<byte[]> GenerateServiceTypeTemplateAsync(CancellationToken ct = default);
        Task<BulkImportResultDto> ImportServiceTypesAsync(Stream fileStream, int responsibleUserId, CancellationToken ct = default);
    }
}
