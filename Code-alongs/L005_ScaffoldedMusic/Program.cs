

using L005_ScaffoldedMusic.Model;

using var db = new MusicContext();

foreach (var artist in db.Artists.Where(a => a.Name.StartsWith("R")))
{
    Console.WriteLine($"{artist.Name}");
}