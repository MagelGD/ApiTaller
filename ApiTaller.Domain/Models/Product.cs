using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public partial class Product : GeneralEntity
{
    public int ProducTypeId { get; set; }
    public string ProductName { get; set; }
    public int Price { get; set; }
    public int SalePrice { get; set; }
    public string Code { get; set; }
    public string Reference { get; set; }
    public string Description { get; set; }
   
    public virtual ProductType ProductTypeIdNavigation { get; set; }



}
