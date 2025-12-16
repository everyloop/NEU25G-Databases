using System;
using System.Collections.Generic;

namespace CompanyDemo.Domain;

public partial class OrderDetail
{
    public string Id { get; set; } = null!;

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public double? UnitPrice { get; set; }

    public int? Quantity { get; set; }

    public double? Discount { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
