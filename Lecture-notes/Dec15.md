# December 15

## 📌 Logging

### Vad är EF Core logging?

**EF Core logging** innebär att Entity Framework Core kan skriva ut information om vad som händer “bakom kulisserna”, framför allt:

- Vilka **SQL-frågor** som genereras

- När databasanrop görs

- Hur lång tid frågor tar

- Eventuella varningar eller fel

Det är ett **debug- och inlärningsverktyg**, inte något man främst använder i produktion.

### Varför är logging viktigt?

När man jobbar med EF Core ser man oftast bara **LINQ-kod**, t.ex.:

```cs
var artists = db.Artists
    .Where(a => a.Albums.Count > 2)
    .Include(a => a.Albums)
    .ToList();
```

Med logging kan man:

- Se **vilken SQL** detta faktiskt blir

- Förstå skillnaden mellan t.ex. Include, Select och lazy loading

- Upptäcka ineffektiva frågor

- Lära sig hur LINQ översätts till SQL

### Simple logging – det vanligaste sättet

Det finns flera olika alternativ för att logging i EFCore.

I code-along nedan använde vi oss av [Simple Logging](https://learn.microsoft.com/en-us/ef/core/logging-events-diagnostics/simple-logging).

**Code-along:**  
[L009_Logging_and_Tracking](https://github.com/everyloop/NEU25G-Databases/blob/main/Code-alongs/L009_Logging_and_Tracking/Model/DemoContext.cs)  

## 📌 Change Tracking

### Vad är Change Tracking?

**Change tracking** är EF Cores mekanism för att hålla reda på:

- vilka objekt (entiteter) som är laddade från databasen

- om deras värden har **ändrats**

- om något ska **INSERT**, **UPDATE** eller **DELETE** vid SaveChanges()

**Kort sagt:**  
👉 EF Core vet vad som har ändrats utan att du själv behöver skriva SQL.

### Grundidén (mental modell)

1) EF Core laddar data från databasen

2) Objekten läggs i **DbContext**

3) DbContext håller ett internt register över:

    - ursprungliga värden

    - nuvarande värden

4) Vid SaveChanges():

    - jämförs värdena

    - rätt SQL genereras automatiskt

### Exempel: ändra ett objekt

```cs
using var db = new MusicContext();

var artist = db.Artists.First();
artist.Name = "New Artist Name";

db.SaveChanges();
```

***Vad händer här?**

- artist är **tracked**

- EF Core märker att Name ändras

- SaveChanges() genererar:

```SQL
UPDATE Artists SET Name = 'New Artist Name' WHERE ArtistId = 1;
```

Du skrev **ingen SQL själv**.

### Entity State (mycket viktigt)

Varje entitet i DbContext har ett **state**:

| State       | Betydelse          |
| ----------- | ------------------ |
| Added     | Ny → INSERT        |
| Modified  | Ändrad → UPDATE    |
| Deleted   | Borttagen → DELETE |
| Unchanged | Ingen ändring      |
| Detached  | Spåras inte        |

Du kan se detta via:

```cs
db.Entry(artist).State
```

### AsNoTracking – när tracking stängs av

```cs
var artists = db.Artists
    .AsNoTracking()
    .ToList();
```

- Entiteterna **spåras inte**

- Förändringar ignoreras

- Snabbare och mindre minne

💡 Används när:

- data bara ska visas (t.ex. i DataGrid)

- inga ändringar ska sparas

Läs mer i [dokumentationen](https://learn.microsoft.com/en-us/ef/core/change-tracking/).

**Code-along:**  
[L009_Logging_and_Tracking](https://github.com/everyloop/NEU25G-Databases/blob/main/Code-alongs/L009_Logging_and_Tracking/Program.cs)  

## 📌 Connected VS Disconnected Scenario

### Grundidén

Skillnaden mellan **connected** och **disconnected** scenario handlar om:

**Hur länge ett DbContext lever och om EF Core fortfarande “känner till” objekten.**

Detta påverkar:

- change tracking

- hur uppdateringar sparas

- hur mycket minne som används

- hur koden måste skrivas

### Connected scenario

Ett **connected scenario** innebär att:

- samma DbContext lever kvar

- entiteterna är **tracked hela tiden**

- EF Core vet direkt vad som ändras

👉 Vanligt i:

- desktop-appar (WPF/WinForms)

- korta operationer

- demos och labbar

### Exempel (connected)

```cs
using var db = new MusicContext();

var artist = db.Artists.First();
artist.Name = "New Name";

db.SaveChanges();
```

Vad händer?

- `artist` är tracked

- EF Core vet att `Name` ändrats

- **UPDATE** sker automatiskt

Ingen extra kod behövs.

#### Fördelar

✅ Enkel kod  
✅ Automatisk change tracking  
✅ Mindre risk för fel  
✅ Lätt att förstå för nybörjare  

####  Nackdelar

❌ DbContext kan bli långlivad  
❌ Mycket data i minnet  
❌ Mindre skalbart  
❌ Kan bli svårfelsökt i stora appar  

### Disconnected scenario

Ett **disconnected scenario** innebär att:

- DbContext skapas → data hämtas → DbContext stängs

- objekten lever vidare **utan EF Core**

- EF Core vet inte om ändringar har skett

👉 Vanligt i:

- webappar (ASP.NET)

- API:er

- WPF med “ladda → redigera → spara”-flöde

### Exempel (disconnected)

```cs
Artist artist;

using (var db = new MusicContext())
{
    artist = db.Artists.First();
}

// DbContext är borta här
artist.Name = "New Name";

using (var db = new MusicContext())
{
    db.Artists.Update(artist);
    db.SaveChanges();
}
```

Vad händer?

- Objektet var **detached**

- Du måste tala om för EF Core:

    - ”det här objektet ska uppdateras”

#### Alternativ: Attach + State

```cs
db.Attach(artist);
db.Entry(artist).State = EntityState.Modified;
db.SaveChanges();
```

#### Fördelar

✅ Mindre minnesanvändning  
✅ Bättre skalbarhet  
✅ Standard i webbutveckling  
✅ Tydligare transaktioner  

#### Nackdelar

❌ Mer kod  
❌ Lättare att göra fel  
❌ EF Core vet inte vad som ändrats automatiskt  

Läs gärna [denna artikel](https://dev.to/christianaugustyn/entity-framework-core-connected-vs-disconnected-3dk7).