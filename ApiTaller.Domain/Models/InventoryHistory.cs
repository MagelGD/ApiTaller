using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public partial class InventoryHistory : GeneralEntity
{
    public int InventoryId { get; set; }
    public int Amount { get; set; }
    public virtual Inventory InventoryIdNavigation { get; set; }
}
