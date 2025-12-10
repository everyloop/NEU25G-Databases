# December 10

## 📌 Skapa och konfigurera en model

### Conventions

Standardregler som EF Core använder för att automatiskt konfigurera modeller.
Du behöver inte skriva något – EF Core gissar t.ex. primärnycklar, relationer och datatyper baserat på namngivning och typer.

*”EF Core gör det åt dig automatiskt.”*

### Data Annotations

Attribut du placerar direkt på dina C#-modeller för att styra beteendet, t.ex. ```[Key]```, ```[Required]```, ```[MaxLength]```.

*”Konfiguration skrivs i modellen via attribut.”*

### Fluent API

Konfiguration som skrivs i ```OnModelCreating``` i ```DbContext``` med metodkedjor. Ger mest kontroll och används när conventions eller annotations inte räcker.

*"Full flexibilitet – allt kan konfigureras här."*

### Kort översikt:

- **Conventions:** automatisk standard.
- **Annotations:** små justeringar direkt på klassen.
- **Fluent API:** full kontroll i kod, gäller över allt annat.

### Precedence (prioritetsordning)

EF Core har en bestämd ordning för vilken konfiguration som gäller när flera sätt används samtidigt.

#### Prioritetsordning (från svagast till starkast):

- **Conventions** – standardregler (lägsta prioritet)
- **Data Annotations** – gäller över conventions
- **Fluent API** – gäller över både annotations och conventions (högsta prioritet)

**Kort sagt:** Om samma sak konfigureras på flera ställen vinner alltid Fluent API, därefter annotations, och sist conventions.

**Code-along:**  
[L006_ModelConfiguration](https://github.com/everyloop/NEU25G-Databases/blob/main/Code-alongs/L006_ModelConfiguration/Model/ConfigDemoContext.cs)

## 📌 Länkar (Model-konfigurering):
### [Overview](https://learn.microsoft.com/en-us/ef/core/modeling/)

### [Entity types](https://learn.microsoft.com/en-us/ef/core/modeling/entity-types?tabs=data-annotations)

### [Entity properties](https://learn.microsoft.com/en-us/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwith-nrt)

### [Keys](https://learn.microsoft.com/en-us/ef/core/modeling/keys?tabs=data-annotations)

### [Generated Values](https://learn.microsoft.com/en-us/ef/core/modeling/generated-properties?tabs=data-annotations)

### [Data Seeding](https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding)

## 📌 Ladda relaterad data

EF Core laddar **inte automatiskt relaterad data** för att undvika:

- **onödig belastning mot databasen**,
- **stora mängder data**, och
- **oväntade eller dyra JOIN-operationer**.

Det ger bättre kontroll över prestanda och gör att du själv väljer när och hur mycket relaterad data som ska hämtas.

**Det finns tre vanliga sätt att ladda relaterad data:**

### 1️⃣ Eager Loading (förhandsladdning)

Du använder ```.Include()``` och ```.ThenInclude()``` för att hämta relaterad data direkt i samma fråga.

```cs
context.Orders
    .Include(o => o.Customer);
```

[Dokumentation](https://learn.microsoft.com/en-us/ef/core/querying/related-data/eager)

### 2️⃣ Explicit Loading

Du laddar relaterade objekt manuellt vid behov, efter att huvudobjektet redan har laddats.

```cs
context.Entry(order).Reference(o => o.Customer).Load();
```

[Dokumentation](https://learn.microsoft.com/en-us/ef/core/querying/related-data/explicit)

### 3️⃣ Lazy Loading

Relaterad data laddas automatiskt när den först nås i koden – kräver proxyfunktioner eller manuell konfiguration.

```cs
var customer = order.Customer; // Triggar ny DB-fråga
```
[Dokumentation](https://learn.microsoft.com/en-us/ef/core/querying/related-data/lazy)

**Code-along:**  
[L005_ScaffoldedMusic](https://github.com/everyloop/NEU25G-Databases/blob/main/Code-alongs/L005_ScaffoldedMusic/Program.cs)  
