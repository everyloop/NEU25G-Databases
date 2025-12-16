using System;
using System.Collections.Generic;

namespace CompanyDemo.Domain;

public partial class Order
{
    public int Id { get; set; }

    public string? CustomerId { get; set; }

    public int? EmployeeId { get; set; }

    public string? OrderDate { get; set; }

    public string? RequiredDate { get; set; }

    public string? ShippedDate { get; set; }

    public int? ShipVia { get; set; }

    public double? Freight { get; set; }

    public string? ShipName { get; set; }

    public string? ShipAddress { get; set; }

    public string? ShipCity { get; set; }

    public string? ShipRegion { get; set; }

    public string? ShipPostalCode { get; set; }

    public string? ShipCountry { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
