# Januari 5

## 📌 SQLite

SQLite är en **filbaserad relationsdatabas**. I stället för en databasserver (som SQL Server) lagras all data i en enda .db-fil.
Den är lättviktig, snabb och kräver ingen installation, serverprocess eller nätverkskoppling.

### Typiska användningsområden

- mobilappar och desktopprogram
- prototyper och labbmiljöer
- små system och inbäddade lösningar
- undervisning och test

### Styrkor

- enkel att komma igång med
- minimalt underhåll
- fullt SQL-stöd för tabeller, index, transaktioner
- fungerar på i princip alla system och kan användas från nästan alla programmeringsspråk.

### Begränsningar

- inte avsedd för mycket stora databaser
- begränsad samtidig skrivning
- saknar vissa funktioner som finns i SQL Server (t.ex. avancerade datatyper, stored procedures m.m.)

[Läs mer på sqlite.org](https://sqlite.org/about.html)

## 📌 SQLite i Entity Framework Core

**Code-along:**  
[L011_Sqlite](https://github.com/everyloop/NEU25G-Databases/tree/main/Code-alongs/L011_Sqlite)

### Installera provider

EF Core använder en särskild [provider för SQLite](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite).

### Konfigurera DbContext
```cs
protected override void OnConfiguring(DbContextOptionsBuilder options)
{
    options.UseSqlite("Data Source=students.db");
}
```

*Notera: Databasen skapas automatiskt som en fil om den inte finns.*

### Migrationer fungerar som vanligt
```
Add-Migration Init
Update-Database
```

### Skillnad mot SQL Server:

- Inga serverkopplingar
- Migrationer uppdaterar filen direkt

## 📌 SQLite Browser (DB Browser for SQLite)

Ett grafiskt verktyg för att:
- öppna och inspektera `.db`-filer  
- visa tabeller och relationer  
- köra SQL-frågor manuellt  
- verifiera resultatet av EF Core-operationer  

**Typiskt arbetsflöde i kursen**
1. Skapa/modifiera modellen i EF Core  
2. Kör migrationer  
3. Öppna databasen i SQLite Browser  
4. Inspektera tabeller, data och schema  

Det ger en **visuell koppling** mellan:
- C#-klasser (modell)  
- EF Core (ORM & migrations)  
- Databasstrukturen  

[Ladda ner här!](https://sqlitebrowser.org/dl/)

## 📌 När bör man välja SQLite i praktiken?

**Bra val**
- prototyper  
- lokala verktyg / labbar  
- offline-appar  
- små interna system  

**Mindre bra val**
- webbappar med många samtidiga användare  
- system med stora datamängder  
- krav på avancerad databaslogik  

## 📌 Sammanfattning
- SQLite är en **enkel, filbaserad databas** som passar utmärkt för lärande och små system.  
- EF Core används på **samma sätt** som mot SQL Server, men med en annan provider.  
- Migrationer fungerar likadant — resultatet kan granskas i **SQLite Browser**.  
- Mycket av det vi lärt oss tidigare i kursen kan även appliceras här.  
