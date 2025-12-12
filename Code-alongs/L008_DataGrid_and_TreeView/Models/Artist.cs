using System;
using System.Collections.Generic;

namespace L008_DataGrid_and_TreeView.Models;

public partial class Artist
{
    public int ArtistId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
