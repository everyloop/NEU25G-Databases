
using L009_Logging_and_Tracking.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

//RecreateDatabase();

//AddCountry("Sweden");
//AddCountry("Norway");
//AddCountry("Denmark");
//AddCountry("Finland");

UpdateDemo();

static void AddCountry(string countryName)
{
    using var db = new DemoContext();

    var country = new Country() { Name = countryName };

    db.Countries.Add(country);

    db.SaveChanges();
}

static void UpdateDemo()
{
    using var db = new DemoContext();

    var countries = db.Countries.ToList();

    Console.WriteLine("Tracker status after loading data:");
    Console.WriteLine(db.ChangeTracker.DebugView.LongView);

    countries[0].Name = "Sweden!";
    countries[1].Name = "Norway!";
    countries[2].Population = 23474345;
    countries[3].Name = "Finland!";
    countries[3].Population = 2357634;

    db.ChangeTracker.DetectChanges();

    Console.WriteLine("\nTracker status after updates:");
    Console.WriteLine(db.ChangeTracker.DebugView.LongView);

    db.SaveChanges();

    Console.WriteLine("\nTracker status after save:");
    Console.WriteLine(db.ChangeTracker.DebugView.LongView);

}

static void RecreateDatabase()
{
    using var db = new DemoContext();

    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}
