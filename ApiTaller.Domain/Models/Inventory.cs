using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public partial class Inventory : GeneralEntity
{
    public int ProductId { get; set; }
    public int Amount { get; set; }
    public virtual Product ProductIdNavigation { get; set; }
}
