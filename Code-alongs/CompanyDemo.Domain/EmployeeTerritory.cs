using System;
using System.Collections.Generic;

namespace CompanyDemo.Domain;

public partial class EmployeeTerritory
{
    public string Id { get; set; } = null!;

    public int? EmployeeId { get; set; }

    public int? TerritoryId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Territory? Territory { get; set; }
}
