# Januari 19

**Code-along:**  
[L014_RepositoryPattern](https://github.com/everyloop/NEU25G-Databases/tree/main/Code-alongs/L014_RepositoryPattern)

## Repository Pattern & DDD – Lektionsanteckningar

### 🎯 Mål med lektionen
- Förstå vad **Repository Pattern** är  
- Se hur det används i praktiken  
- Koppla mönstret till **Domain-Driven Design (DDD)**  
- Implementera samma kontrakt mot:
  - MongoDB
  - SQL Server (EF Core)


## 📌 Kort om DDD (Domain-Driven Design)

DDD handlar om:

> Bygga system utifrån verksamheten (domänen) – inte tekniken.

### Viktiga begrepp

| Begrepp | Förklaring | Exempel |
|----------|------------|----------|
| **Entity** | Har identitet, lever över tid | `Movie` |
| **Value Object** | Saknar identitet, är bara värden | `ImdbInfo` |
| **Aggregate** | Grupp objekt som hör ihop | Movie + ImdbInfo |
| **Aggregate Root** | Huvudentiteten | Movie |
| **Ubiquitous Language** | Samma språk i kod & verksamhet | `GetTopRatedMovies()` |

### Grundprincip
- Domänklasser ska **inte känna till databasen**
- Mongo/SQL är implementationdetaljer


## 📌 Vad är Repository Pattern?

Ett repository är:

> Ett lager som ansvarar för att hämta och spara domänobjekt  
> utan att applikationen bryr sig om databasteknik.

### Exempel

````csharp
IMovieRepository repo;

await repo.GetTopRatedMoviesAsync(...);
````

Applikationen vet inte:
- om det är Mongo
- om det är SQL
- hur queryn ser ut


## 📌 Varför använder man repository?

✔ Separera affärslogik från databas  
✔ Enklare att testa  
✔ Lätt att byta databas  
✔ Koden blir mer läsbar  
✔ Följer DDD-principer  


## 📌 Repository – Interface först

````csharp
interface IMovieRepository
{
    Task<Movie?> GetByImdbIdAsync(int imdbId);
    Task<IReadOnlyList<Movie>> GetTopRatedMoviesAsync(int count);
}
````

### Viktigt
- Metoder speglar **affärsbehov**
- Inte tekniska queries
- Använd domänspråk


## 📌 Implementationer

### Mongo
````csharp
class MongoMovieRepository : IMovieRepository
````

### SQL
````csharp
class SqlMovieRepository : IMovieRepository
````

➡ Samma kontrakt  
➡ Olika tekniker  

## 📌 Poängen – utbytbarhet

````csharp
IMovieRepository repo =
    useMongo
        ? new MongoMovieRepository(...)
        : new SqlMovieRepository(...);
````

Resten av koden ändras inte.


## 📌 Hur repositories används i verkligheten

- Man börjar med:
  - GetById
  - Add
  - Update
- Nya metoder skapas **när behov uppstår**
- Repository växer organiskt
- Varje metod ska motsvara ett use case

❌ Undvik:
````csharp
Find(Expression<Func<T,bool>>)
````

✔ Föredra:
````csharp
GetActiveSubscriptions()
GetTopRatedMovies()
````


## 📌 Viktiga lärdomar

✔ Domän först  
✔ Repository = kontrakt  
✔ Databas = implementation  
✔ Samma kod → olika datakällor  
✔ Lättare testning  
✔ Bättre arkitektur  


## 🚀 Sammanfattning

- Repository pattern är **praktisk DDD**
- Vi programmerar mot **kontrakt**
- Databasen blir utbytbar
- Koden speglar verksamheten
