
using L009_Logging_and_Tracking.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

//RecreateDatabase();

//AddCountry("Sweden");
//AddCountry("Norway");
//AddCountry("Denmark");
//AddCountry("Finland");

//AddCity("Stockholm", "Sweden");
//AddCity("Göteborg", "Sweden");
//AddCity("Malmö", "Sweden");

//AddCity("Madrid", "Spain");
//AddCity("Oslo", "Norway");
//AddCity("Paris", "France");

//RemoveCityByName("Oslo");
//RemoveCityByName("London");

//UpdateDemo();

Country? myCountry = GetCountryByName("Sweden!");

myCountry.Name = "Sweden!";

SaveCountry(myCountry);

Console.WriteLine();

static void SaveCountry(Country country)
{
    using var db = new DemoContext();

    // Add / Update / Attach start tracking and traverse the object graph
    // (i.e they walk navigation properties and attach related entities)
    db.Attach(country); // Root + children tracked as Unchanged

    // Set tracking state of root entity only (does NOT affect children)
    //db.Entry(country).State = EntityState.Modified;

    // Mark a single property on the root entity as Modified
    db.Entry(country).Property(c => c.Name).IsModified = true;

    db.PrintChangeTrackerDebugInfo();
    
    db.SaveChanges();
}

static Country? GetCountryByName(string countryName)
{
    using var db = new DemoContext();
    return db.Countries.Include(c => c.Cities).FirstOrDefault(c => c.Name == countryName);
}

static void RemoveCityByName(string cityName)
{
    using var db = new DemoContext();

    var city = db.Cities.FirstOrDefault(c => c.Name == cityName);

    if (city is not null)
    {
        db.Cities.Remove(city);
    }

    db.PrintChangeTrackerDebugInfo();

    db.SaveChanges();
}

static void AddCity(string cityName, string countryName)
{
    using var db = new DemoContext();

    if (db.Cities.Any(c => c.Name == cityName && c.Country.Name == countryName))
    {
        return;
    }

    var city = new City() { Name = cityName };

    var country = db.Countries.FirstOrDefault(country => country.Name == countryName)
        ?? new Country() { Name = countryName };

    city.Country = country;

    db.Cities.Add(city);

    db.PrintChangeTrackerDebugInfo();

    db.SaveChanges();
}

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

    db.PrintChangeTrackerDebugInfo("Tracker status after loading data:");

    countries[0].Name = "Sweden?";
    //countries[1].Name = "Norway!";
    //countries[2].Population = 23474345;
    //countries[3].Name = "Finland!";
    //countries[3].Population = 2357634;

    db.PrintChangeTrackerDebugInfo("Tracker status after updates:");

    db.SaveChanges();

    db.PrintChangeTrackerDebugInfo("Tracker status after save:");

    countries[0].Name = "Sweden";

    db.PrintChangeTrackerDebugInfo("Tracker status after second updates:");

    db.SaveChanges();

}

static void RecreateDatabase()
{
    using var db = new DemoContext();

    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}
