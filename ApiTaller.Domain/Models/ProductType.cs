using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public partial class ProductType : GeneralEntity
{

    public string Type { get; set; }
    /// <summary>SAAS-1: ID del taller al que pertenece este tipo de producto</summary>
    public int WorkshopId { get; set; }
   
}
