
RecreateDatabase();

AddCity("Stockholm", "Sverige");
AddCity("Göteborg", "Sverige");
AddCity("Malmö", "Sverige");
AddCity("Oslo", "Norge");
AddCity("Bergen", "Norge");

static void AddCity(string cityName, string countryName)
{
    using var db = new SqliteContext();

    var country = db.Countries.FirstOrDefault(c => c.Name == countryName) 
        ?? new Country() { Name = countryName };

    var city = new City() { Name = cityName, Country = country };
    
    db.Cities.Add(city);

    db.SaveChanges();
}

static void RecreateDatabase()
{
    using var db = new SqliteContext();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}

