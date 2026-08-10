using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public partial class Product : GeneralEntity
{
    public int ProducTypeId { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal SalePrice { get; set; }
    public string Code { get; set; } = null!;
    public string Reference { get; set; } = null!;
    public string Description { get; set; } = null!;
    /// <summary>
    /// CAR-11: Tipo de vehículo al que aplica este producto.
    /// 'moto' | 'car' | 'both'
    /// </summary>
    public string VehicleType { get; set; } = "both";
    /// <summary>SAAS-1: ID del taller al que pertenece este producto</summary>
    public int WorkshopId { get; set; }
   
    public virtual ProductType ProductTypeIdNavigation { get; set; } = null!;



}
