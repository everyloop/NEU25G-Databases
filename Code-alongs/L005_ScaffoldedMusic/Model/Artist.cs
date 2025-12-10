using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace L005_ScaffoldedMusic.Model;

[DebuggerDisplay("{Name}, Albums: {Albums.Count}")]
public partial class Artist
{
    public int ArtistId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
