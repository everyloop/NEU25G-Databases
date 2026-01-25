# Januari 23

SQL Server har sedan version 2016 inbyggt stöd för att arbeta med JSON. 

Detta innebär **inte** att SQL Server blir en dokumentdatabas, utan att den relationella databasen får möjlighet att hantera **semi-strukturerad data** på ett kontrollerat sätt.

JSON-stödet är främst tänkt som ett komplement till den relationsbaserade modellen.


### När JSON i SQL Server är ett bra val

- När de flesta kolumner är kända och stabila, men vissa fält är:
  - valfria
  - varierande
  - förändras över tid
- När man behöver lagra:
  - konfiguration
  - metadata
  - extra attribut
- Vid integration med externa system som skickar JSON
- I ETL-flöden (import/export)

Exempel:
- Users med fast struktur + ExtraInfo (JSON)
- Products med OptionalInfo (JSON)

---

### När JSON i SQL Server bör undvikas

- När data ofta:
  - filtreras
  - sorteras
  - joinas
- När fälten är centrala för affärslogik
- När man behöver hårda constraints och FK

**Tumregel:**

> Om du ofta använder ett JSON-fält i WHERE, JOIN eller ORDER BY  
> → det borde troligen vara en vanlig kolumn istället.

---

### När MongoDB (eller annan dokumentdatabas) är bättre

- När majoriteten av datat är naturligt hierarkiskt
- När strukturen varierar kraftigt mellan dokument
- När man oftast läser/skriver hela dokument
- När schema-on-read passar bättre än schema-on-write

Kort sagt:

- SQL Server + JSON → relationell databas med lite flexibilitet  
- MongoDB → dokumentdatabas i grunden

---

### Viktig princip

SQL Server **lagrar JSON som text** (nvarchar).  
Det finns ingen separat JSON-datatyp.

Därför behövs:
- ISJSON
- Constraints
- Disciplin i design


## JSON_OBJECT & JSON_ARRAY

**Används för att bygga JSON-strukturer i T-SQL utan att skriva råa JSON-strängar.**

### JSON_OBJECT
Skapar ett JSON-objekt (key/value):

````sql
SELECT JSON_OBJECT(
  'Name': 'Keyboard',
  'Price': 299
);
````

Resultat:

````json
{"Name":"Keyboard","Price":299}
````

---

### JSON_ARRAY
Skapar en JSON-array:

````sql
SELECT JSON_ARRAY('Red', 'Green', 'Blue');
````

Resultat:

````json
["Red","Green","Blue"]
````

---

### Kombinera dem (nästning)

````sql
SELECT JSON_OBJECT(
  'Name': 'Mouse',
  'Tags': JSON_ARRAY('USB', 'Gaming'),
  'Details': JSON_OBJECT(
      'Color': 'Black',
      'Weight': 120
  )
);
````

---

### Jämfört med rå sträng

````sql
N'
{
  "Name": "Mouse",
  "Tags": ["USB", "Gaming"],
  "Details": {
    "Color": "Black",
    "Weight": 120
  }
}'
````

**Fördelar med JSON_OBJECT/JSON_ARRAY**
- Mindre risk för syntaxfel
- Automatisk escaping
- Lättare när värden kommer från variabler/kolumner

**Tumregel:**  
Bygg JSON som struktur – inte som sträng.


## ISJSON, JSON_VALUE, JSON_MODIFY

Tabell:

````sql
create table products
(
  Id int primary key identity(1,1),
  Name nvarchar(50),
  Price decimal,
  OptionalInfo nvarchar(max)
    check (OptionalInfo is null or IsJson(OptionalInfo) = 1)
)
````


### ISJSON
Kontrollerar om en sträng är giltig JSON.

````sql
SELECT ISJSON(OptionalInfo) FROM Products;
````

Returnerar `1` eller `0`.

Används ofta i:
- CHECK constraint
- Validering vid import


### JSON_VALUE
Hämtar **ett skalärt värde** ur JSON.

````sql
SELECT JSON_VALUE(OptionalInfo, '$.Color')
FROM Products;
````

- Returnerar text/nummer/bool
- Fungerar inte för objekt/arrayer


### JSON_MODIFY
Ändrar eller lägger till ett värde i JSON.

````sql
UPDATE Products
SET OptionalInfo =
    JSON_MODIFY(OptionalInfo, '$.Color', 'Red')
WHERE Id = 1;
````

Om fältet saknas → skapas.


### Tumregel
- JSON_VALUE → läsa värde  
- JSON_MODIFY → skriva värde  
- ISJSON → validera  


## FOR JSON AUTO & FOR JSON PATH

Används för att **skapa JSON från SQL-resultat**.


### FOR JSON AUTO
Automatisk struktur baserad på tabeller och joins.

````sql
SELECT * FROM Products
FOR JSON AUTO;
````

- Snabbt
- Mindre kontroll


### FOR JSON PATH
Full kontroll över struktur och namn.

````sql
SELECT
  Id AS ProductId,
  Name,
  Price
FROM Products
FOR JSON PATH;
````

Kan nästla med subqueries.


### Skillnad

| AUTO | PATH |
|-----|-----|
| Enkelt | Flexibelt |
| Mindre kontroll | Mer kontroll |
| Bra för snabba tester | Bäst för API/struktur |


## Scalar-valued vs Table-valued functions

### Scalar-valued function
Returnerar **ett värde**.

````sql
SELECT LEN('Hello');
````


### Table-valued function
Returnerar **en tabell**.

````sql
SELECT * FROM GENERATE_SERIES(1,5);
````

eller

````sql
SELECT value FROM STRING_SPLIT('A,B,C', ',');
````


### Varför viktigt?
Vissa funktioner används i:

````sql
SELECT kolumn
````

Andra används i:

````sql
FROM funktion(...)
````


## OPENJSON

**Table-valued function som gör JSON → tabell.**

````sql
DECLARE @json nvarchar(max) =
N'{
  "Tags": ["Red","Blue"],
  "Stock": 10
}';

SELECT *
FROM OPENJSON(@json);
````
**Resultat:**
| key   | value          | type |
| ----- | -------------- | ---- |
| Tags  | ["Red","Blue"] | 4    |
| Stock | 10             | 2    |


### Med path

````sql
SELECT *
FROM OPENJSON(@json, '$.Tags');
````
**Resultat:**
| key | value | type |
| --- | ----- | ---- |
| 0   | Red   | 1    |
| 1   | Blue  | 1    |


### Med schema (WITH)

````sql
SELECT *
FROM OPENJSON(@json)
WITH (
  Stock int '$.Stock',
  Tags nvarchar(max) '$.Tags' AS JSON
);
````
**Resultat:**
| Stock | Tags           |
| ----- | -------------- |
| 10    | ["Red","Blue"] |


### Mental modell
OPENJSON = flatten  

(JSON → rader)


## OPENROWSET

**Läser data från extern källa som om det vore en tabell.**

Vanligast för filer:

````sql
SELECT *
FROM OPENROWSET(
  BULK 'C:\data\products.json',
  SINGLE_CLOB
) AS src;
````

Ger en kolumn med hela filens innehåll.

Kombineras ofta med OPENJSON:

````sql
SELECT *
FROM OPENJSON(
  (SELECT BulkColumn
   FROM OPENROWSET(BULK 'file.json', SINGLE_CLOB) AS x)
);
````


### Mental modell
OPENROWSET = hämta data  
OPENJSON = tolka data  


# Snabb sammanfattning

SQL Server är i grunden en relationsdatabas, men har stöd för att lagra och arbeta med JSON när viss flexibilitet behövs. JSON-funktionerna är främst tänkta för integration, konfigurationsdata och semi-strukturerad information – inte för att ersätta normaliserade tabeller. 

Med `JSON_OBJECT` och `JSON_ARRAY` kan man bygga JSON, med `FOR JSON` kan man skapa JSON från tabeller, och med `OPENJSON` kan man göra om JSON till tabellform. Tillsammans gör detta det möjligt att röra sig mellan relationsmodell och dokumentstruktur på ett kontrollerat sätt.


| Funktion | Syfte |
|--------|------|
| JSON_OBJECT / JSON_ARRAY | Bygga JSON |
| ISJSON | Validera JSON |
| JSON_VALUE | Läsa värde |
| JSON_MODIFY | Ändra värde |
| FOR JSON | Tabell → JSON |
| OPENJSON | JSON → tabell |
| OPENROWSET | Fil → tabell |

