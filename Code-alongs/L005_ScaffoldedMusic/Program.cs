

using L005_ScaffoldedMusic.Model;
using Microsoft.EntityFrameworkCore;

using var db = new MusicContext();

//var query = db.Artists
//    .Where(artist => artist.Name.StartsWith("R"))
//    .Include(artist => artist.Albums)
//    .ThenInclude(album => album.Tracks);

//var query = db.Tracks
//    .Include(t => t.Album)
//    .ThenInclude(a => a.Artist)
//    .Include(t => t.Genre);

//Console.WriteLine(query.ToQueryString());

//var tracks = query.ToList();


//var albums = db.Albums.ToList();

//db.Entry(albums[0]).Reference(a => a.Artist).Load();

//db.Entry(albums[0]).Collection(a => a.Tracks).Load();

var artist = db.Artists.ToList();

var albums = db.Albums.ToList();

Console.WriteLine();