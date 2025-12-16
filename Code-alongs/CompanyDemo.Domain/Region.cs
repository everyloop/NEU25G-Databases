using System;
using System.Collections.Generic;

namespace CompanyDemo.Domain;

public partial class Region
{
    public int Id { get; set; }

    public string? RegionDescription { get; set; }

    public virtual ICollection<Territory> Territories { get; set; } = new List<Territory>();
}
