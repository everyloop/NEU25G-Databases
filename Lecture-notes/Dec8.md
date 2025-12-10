# December 8

## 📌 Entity Framework Core

**Entity Framework Core (EF Core)** är ett modernt, lättviktigt objekt-relationsramverk [ORM](https://en.wikipedia.org/wiki/Object%E2%80%93relational_mapping) för .NET som låter dig arbeta med databaser genom **C#-klasser i stället för SQL-frågor.**

Det betyder att du:
- skriver kod mot **objekt och modeller**,
- EF Core översätter automatiskt till SQL,
- och hanterar databasanslutningar, spårning och uppdateringar åt dig.

### Kortfattat:

EF Core gör databasen ”osynlig” och låter dig jobba med C#-objekt i stället för att själv skriva SQL – samtidigt som det kan generera och uppdatera databasschemat (migrations).

## 📌 Länkar (EF Core):

### [Introduction](https://learn.microsoft.com/en-us/ef/core/)

### [DbContext](https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/)

### [Querying Data](https://learn.microsoft.com/en-us/ef/core/querying/)

### [Saving Data](https://learn.microsoft.com/en-us/ef/core/saving/basic)

## 📌 Hantering av databasscheman

EF Core har två huvudsätt att hålla din EF Core-modell och databasschemat i synk. Valet beror på vad som ska vara ”**source of truth**”.

**Migrations:** Används när EF Core-modellen är utgångspunkten. När du ändrar modellen skapas och tillämpas stegvisa schemaändringar i databasen så att den matchar modellen.

**Reverse Engineering:** Används när databasschemat är utgångspunkten. EF Core läser då databasen och genererar en DbContext och entitetsklasser som motsvarar schemat.

## 🔄 Migrations (Code First)

Migrations används för att **skapa och uppdatera databasschemat utifrån dina C#-modeller**.

- Du börjar med C#-klasser (modeller)
- Kör ```Add-Migration``` → EF Core skapar en migreringsfil med SQL-ändringar
- Kör ```Update-Database``` → ändringarna tillämpas på databasen

**Kort sagt:** *Migrations = skapa eller ändra databasen från kod.*

[Dokumentation](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)

## 🔁 Reverse Engineering (Database First)

Reverse engineering används när du har en **befintlig databas** och vill **generera C#-klasser från den**.

- Kör ```Scaffold-DbContext```
- EF Core läser databasen och skapar modeller + DbContext

**Kort sagt:** Reverse engineering = skapa kod från befintlig databas.

[Dokumentation](https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding/?tabs=dotnet-core-cli)

**Code-along:**  
[L004_EFcore_Intro](https://github.com/everyloop/NEU25G-Databases/tree/main/Code-alongs/L004_EFcore_Intro)