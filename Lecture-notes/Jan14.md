# Januari 14

**Code-along:**  
[L012_MongoDb.Driver](https://github.com/everyloop/NEU25G-Databases/blob/main/Code-alongs/L012_MongoDb.Driver/Program.cs)

## 🚀 MongoDB Driver i C#

MongoDB Driver är det **officiella C#-biblioteket** för att arbeta mot MongoDB.

Det är samma typ av driver som används i:

- mongosh (JS-driver)
- VS Code extension
- andra språk (Java, Python, Node osv)

➡️ **Samma databas – bara olika språk.**

## Installera

Via NuGet:

```
MongoDB.Driver
```

> Installera **endast detta paket** – det drar in resten automatiskt.


## Grundflöde

```csharp
var client = new MongoClient(connectionString);
var db = client.GetDatabase("myDb");
var users = db.GetCollection<User>("users");
```

Steg:

1. Skapa `MongoClient`
2. Hämta databas
3. Hämta collection


## Datamodell

```csharp
public class User
{
    [BsonId]
    public ObjectId Id { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
}
```

Vanliga attribut:

- `[BsonId]` → primärnyckel
- `[BsonElement("email")]` → ändra fältnamn
- `[BsonIgnore]` → ignorera property




## CRUD

### Create

```csharp
await users.InsertOneAsync(user);
```

### Read

Hämta alla:

```csharp
var list = await users.Find(_ => true).ToListAsync();
```

Med filter (lambda):

```csharp
var result = await users
    .Find(u => u.Age > 30)
    .ToListAsync();
```

Med filter (builder):

```csharp
var filter = Builders<User>.Filter.Gt(u => u.Age, 30);
var result = await users.Find(filter).ToListAsync();
```



### Update

```csharp
var filter = Builders<User>.Filter.Eq(u => u.Email, "a@test.se");
var update = Builders<User>.Update.Set(u => u.Age, 35);

await users.UpdateOneAsync(filter, update);
```

### Delete

```csharp
await users.DeleteOneAsync(u => u.Email == "a@test.se");
```


## Filter Builders

AND:

```csharp
var f = Builders<User>.Filter;

var filter =
    f.Gt(u => u.Age, 18)
    & f.Eq(u => u.City, "Stockholm");
```

OR:

```csharp
var filter = f.Or(
    f.Eq(u => u.Role, "Admin"),
    f.Eq(u => u.Role, "Manager")
);
```


## Sort / Skip / Limit

```csharp
var result = await users
    .Find(filter)
    .SortByDescending(u => u.Age)
    .Skip(10)
    .Limit(5)
    .ToListAsync();
```



# 🎯 Sammanfattning

- MongoDB Driver = C#-API för MongoDB
- Samma funktioner som i mongosh
- Builders = hjälpmetoder för filter & updates
- LINQ är möjligt men inte primärt fokus
- Passar perfekt för backend-appar

