using System;
using System.Collections.Generic;

namespace CompanyDemo.Domain;

public partial class Territory
{
    public int Id { get; set; }

    public string? TerritoryDescription { get; set; }

    public int? RegionId { get; set; }

    public virtual ICollection<EmployeeTerritory> EmployeeTerritories { get; set; } = new List<EmployeeTerritory>();

    public virtual Region? Region { get; set; }
}
