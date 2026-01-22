# Januari 21

## Vad är ETL?

**ETL** står för:

- **Extract** – hämta data från ett källsystem  
- **Transform** – omforma data (rensa, normalisera, ändra struktur)  
- **Load** – ladda in data i ett målsystem  

I praktiken:

> Vi flyttar data mellan system som har olika datamodeller  
> (t.ex. relationsdatabas → dokumentdatabas).



## Vad är CSV?

**CSV = Comma Separated Values**

- Ett enkelt textformat för tabulär data
- Varje rad = en post
- Kolumner separeras med komma (eller semikolon)

Exempel:

````text
OrderId,Customer,Amount
1001,Anna,250
1002,Erik,400
````

**Egenskaper**
- Väldigt vanligt utbytesformat
- Lätt att öppna i Excel
- Inget schema, inga datatyper – bara text
- Mindre strukturerat än JSON

**Jämförelse:**
- JSON → hierarkiskt (objekt, arrayer)
- CSV → platt tabellstruktur



## CSV och JSON som mellansteg

För att flytta data mellan SQL Server och MongoDb, och tvärtom, så använde vi CSV och/eller Json som mellansteg.

Varför mellansteg?

- Enkelt att:
  - spara filer
  - debugga
  - flytta mellan system
- Format som nästan alla verktyg stödjer
- Bra för batch-import/export

Detta är ett typiskt **ETL-flöde**:
- Extract: SQL query → CSV
- Transform: justera kolumner
- Load: import till MongoDB


## mongoimport & mongoexport

Detta är **kommandoradsverktyg (CLI)** från MongoDB:

### mongoexport
- Exporterar data från MongoDB till:
  - JSON
  - CSV

Exempel:

````bash
mongoexport --db sample --collection orders --type=csv --out orders.csv
````

### mongoimport
- Importerar data från:
  - CSV
  - JSON
- Skapar dokument i MongoDB

Exempel:

````bash
mongoimport --db test --collection orders --type=csv --file orders.csv --headerline
````

### Compass (GUI)

Vi såg även att man kan:
- importera/exportera direkt i MongoDB Compass
- enklare att använda
- men:
  - svårare att automatisera
  - passar sämre för produktion

**Sammanfattning**
- CLI → bäst för automation
- Compass → bra för test/labb


## Exemplet: Orders & OrderDetails

I MongoDB hade vi en **sample collection** där varje dokument innehöll:

- en order
- en `items`-array


Vi använde:

````js
$unwind: "$items"
````

→ plattar ut arrayen  
→ en rad per item

### Resultat

Vi exporterade två filer:

**orders.csv**
- 1 rad per order

**orderDetails.csv**
- 1 rad per item
- innehåller:
  - OrderId (Mongo _id)
  - OrderRad
  - produktinfo

### SQL

Vi importerade:

- Orders
- OrderDetails

och skapade:

- PK: Orders.OrderId
- FK: OrderDetails.OrderId

Detta visar:
- hur dokumentdata normaliseras
- hur relationer uppstår i SQL



## Miljövariabler & PATH

När vi installerade:
- `mongoimport`
- `mongoexport`

behövde vi lägga till dem i **PATH**.

### Vad är en miljövariabel?

En variabel som finns i operativsystemet och kan användas av program.

Exempel:
- connection strings
- API-nycklar
- sökvägar till program

Vi har tidigare använt:
- **user secrets**  
→ samma idé, men lagrat per projekt

### Vad är PATH?

`PATH` är en miljövariabel som innehåller:

> en lista med mappar där systemet letar efter program

Om vi skriver:

````bash
mongoexport
````

så letar systemet i:
- C:\Program Files\...
- osv enligt PATH

Om programmet inte finns där → "command not found"

### Därför lade vi till:

MongoDBs bin-mapp i PATH  
→ så vi kan köra kommandon från valfri mapp


# Sammanfattning

Vi har sett:

- Vad ETL är
- Skillnad mellan CSV och JSON
- Hur filer används som mellansteg
- Hur man importerar/exporterar MongoDB-data
- Hur man plattar ut dokumentdata med `$unwind`
- Hur miljövariabler och PATH fungerar

Detta är ett **realistiskt arbetsflöde** som används i riktiga system.
