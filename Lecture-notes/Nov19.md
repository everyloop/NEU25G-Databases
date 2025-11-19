# November 19

**Lecture slides:**  
[Aggregering.pdf](https://github.com/everyloop/NEU25G-Databases/blob/master/Resources/Aggregering.pdf)  
[Datatyper och variabler.pdf](https://github.com/everyloop/NEU25G-Databases/blob/master/Resources/Datatyper%20och%20variabler.pdf)

## Aggregering

**Aggregerad data** är sammanställd eller “ihopslagen” data som uttrycker ett helhetsvärde i stället för enskilda rader.

Exempel på aggregat:
- Antal kunder i en tabell
- Totalsumma av alla ordrar
- Medelvärde av temperatur per dag

Det är alltså data som representerar en hel grupp av rader.

En **aggregeringsfunktion** tar flera rader och returnerar ett enda värde.

Vanliga aggregeringsfunktioner i SQL:
| Funktion  | Förklaring            |
| --------- | --------------------- |
| COUNT() | Räknar antal rader    |
| SUM()   | Summerar värden       |
| AVG()   | Returnerar medelvärde |
| MIN()   | Minsta värdet         |
| MAX()   | Största värdet        |


**GROUP BY** används när du vill:

Dela upp rader i grupper, och

Köra aggregeringsfunktioner per grupp.

**Exempel:** Antal ordrar per kund
```SQL
SELECT CustomerID, COUNT(*) AS OrderCount
FROM Orders
GROUP BY CustomerID;
```

Detta ger en rad per CustomerID, där varje rad innehåller antal ordrar.

**Viktig regel:**

Alla kolumner i SELECT som inte är aggregeringsfunktioner måste finnas i GROUP BY.

**HAVING** filtrerar efter att grupperna skapats.

Skillnad:
- WHERE filtrerar rader innan grupperingen
- HAVING filtrerar grupper efter grupperingen

**Exempel:** Kunder som har fler än 5 ordrar:
```SQL
SELECT CustomerID, COUNT(*) AS OrderCount
FROM Orders
GROUP BY CustomerID
HAVING COUNT(*) > 5;
```
Detta är omöjligt med WHERE, eftersom COUNT() inte existerar innan grupperingen.

**Code-along:**  
[004_Aggregation.sql](https://github.com/everyloop/NEU25G-Databases/blob/main/SQL/004_Aggregation.sql)

## Format()

T-SQL funktionen format() använder .NET's formateringssträngar. D.v.s samma strängar som man kan använda i .toString() metoden i C#:

[Formateringssträngar i .NET](https://learn.microsoft.com/en-us/dotnet/standard/base-types/formatting-types)

**Code-along:**  
[005_%C3%96vningsuppgifter_CRUD.sql](https://github.com/everyloop/NEU25G-Databases/blob/main/SQL/005_%C3%96vningsuppgifter_CRUD.sql)

## Datatyper

En **datatyp** bestämmer vilken typ av värden en kolumn kan lagra, t.ex. text, heltal, decimaler, datum eller binär data.

Datatypen styr:
- hur mycket minne som används
- vilka operationer som är tillåtna
- hur data sorteras och jämförs
- hur data valideras

Rätt datatyp är viktigt för prestanda, lagringsåtgång och datakvalitet.

### 🔢 Numeriska datatyper
| Datatyp                             | Beskrivning                                |
| ----------------------------------- | ------------------------------------------ |
| **INT**                             | Heltal (–2,147,483,648 till 2,147,483,647) |
| **BIGINT**                          | Större heltal                              |
| **SMALLINT**                        | Mindre heltal                              |
| **TINYINT**                         | Mycket små heltal (0–255)                  |
| **DECIMAL(p,s)** / **NUMERIC(p,s)** | Exakta decimaltal, bra för pengar          |
| **FLOAT**                           | Flyttal (approximerade tal)                |

### 📝 Sträng- / textdatatyper
| Datatyp                          | Beskrivning                                              |
| -------------------------------- | -------------------------------------------------------- |
| **VARCHAR(n)**                   | Variabel text (ASCII)                                    |
| **NVARCHAR(n)**                  | Variabel text (Unicode) – **rekommenderas i moderna system** |
| **CHAR(n)**                      | Fast textlängd                                           |
| **NCHAR(n)**                     | Fast textlängd (Unicode)                                 |
| **VARCHAR(MAX) / NVARCHAR(MAX)** | Stora textfält                                           |

### 📅 Datum och tid
| Datatyp            | Beskrivning                                           |
| ------------------ | ----------------------------------------------------- |
| **DATE**           | Endast datum                                          |
| **TIME**           | Endast tid                                            |
| **DATETIME**       | Datum + tid (sekundnoggrannhet)                       |
| **DATETIME2**      | Nyare, bättre precision och intervall (**rekommenderas**) |
| **SMALLDATETIME**  | Datum och tid med lägre precision                     |
| **DATETIMEOFFSET** | Datum + tid + tidszon                                 |

Läs mer här: [Datatyper i T-SQL](https://learn.microsoft.com/en-us/sql/t-sql/data-types/data-types-transact-sql?view=sql-server-ver16)

## Konvertering mellan datatyper
**Datakonvertering** innebär att SQL Server omvandlar ett värde från en datatype till en annan.

Exempel:
- text → tal
- text → datum
- decimal → int
- datum → text

Det finns två typer av konvertering:

### 1️⃣ Implicit konvertering

SQL Server konverterar automatiskt när det är rimligt.

Exempel:
```SQL
SELECT 10 + '20';
```

'20' konverteras automatiskt till tal → resultat blir 30.

### 2️⃣ Explicit konvertering
Du tvingar själv SQL Server att konvertera.

Här används CAST(), CONVERT() och FORMAT().

### 📌 CAST()

Standardiserad SQL-funktion

Används för att konvertera ett värde till en annan datatyp.

Syntax:
```SQL
CAST(value AS datatype)
```

Exempel:
```SQL
SELECT CAST('123' AS INT);
```
```SQL
SELECT CAST(123.45 AS INT);  -- Ger 123
```

Används när du vill ha ren, standard-SQL-kompatibel kod,
och inte behöver formatera datum/tid till text

### 📌 CONVERT()
**SQL Server-specifik funktion (Endast T-SQL)**

Kan göra samma konverteringar som CAST(), men den är kraftfullare för datumformat och vissa specialfall.

Syntax:
```SQL
CONVERT(datatype, value [, style])
```

style används ofta för att formatera datum → text.

Exempel:
```SQL
SELECT CONVERT(VARCHAR(10), GETDATE(), 120);  -- 2025-01-15
SELECT CONVERT(VARCHAR(10), GETDATE(), 104);  -- 15.01.2025 (tysk stil)
```

**När ska man använda CONVERT()?**
- När du behöver formatera datum eller tal till text
- När du jobbar med äldre SQL Server-kod som använder "style"-koder

### 📌 FORMAT()
**Modernare funktion**, mycket flexibel, men **långsammare** eftersom den använder .NET-formattering i bakgrunden.

Syntax:
```SQL
FORMAT(value, 'format_string' [, culture])
```

Exempel:
```SQL
SELECT FORMAT(GETDATE(), 'yyyy-MM-dd');
SELECT FORMAT(GETDATE(), 'dd MMM yyyy', 'sv-SE');  -- "15 jan 2025"
```

**Typiska användningar:**
- Anpassad datumformattering
- Språkanpassad visning
- Valutaformat

**Nackdel:**

**30–100x** långsammare än CONVERT() i stora dataset

**Code-along:**  
[007_Converting_datatypes.sql](https://github.com/everyloop/NEU25G-Databases/blob/main/SQL/007_Converting_datatypes.sql)

## Identity
IDENTITY i SQL Server är en egenskap du kan lägga på en kolumn för att den automatiskt ska generera nya sekventiella värden — oftast används detta för primärnycklar.

```SQL
IDENTITY(seed, increment)
```
- seed = startvärdet (t.ex. 1)
- increment = hur mycket värdet ökar varje gång (t.ex. 1)

## GUID
Globally Unique Identifier (GUID) är en typ av identifierare som används i programvara och som är tänkt att vara globalt unikt. Termen Universally Unique Identifier (UUID) förekommer också. Det totala antalet unika nycklar är 2128 (cirka 3,4×1038) så sannolikheten för att samma tal genereras fler än en gång är mycket liten. Om varje människa på jorden genererade 600 miljoner nycklar skulle sannolikheten för att två likadana genereras ligga på 50%. En nyckel innehåller oftast 128 bitar.

En GUID är uppbyggd av 32 hexadecimala siffror och 4 bindestreck och ser ut på följande sätt, 123e4567-e89b-12d3-a456-426655440000

## Primary key

En kolumn markerad som primary key måste innehålla unika värden.

Som primary key används vanligen en av följande:
1. Ett löpnummer (integer tillsammans med [identity](https://www.red-gate.com/simple-talk/databases/sql-server/learn/sql-server-identity-column/))
2. Ett [GUID](https://sv.wikipedia.org/wiki/Globally_Unique_Identifier) (datatyp uniqueidentifier, tillsammans med newid() för att generera guid)
3. Något som redan är unikt, t.ex personnummer, produktnummer, ISBN

**Code-along:**  
[006_Identity_%26_GUID.sql](https://github.com/everyloop/NEU25G-Databases/blob/main/SQL/006_Identity_%26_GUID.sql)

## Variabler

En variabel i T-SQL är ett namn som håller ett värde i minnet under tiden ett skript, ett batch-kommando eller en stored procedure körs.

Variabler existerar bara **inom samma batch eller block** där de deklarerats.

### 📌 Deklarera en variabel

Du deklarerar en variabel med **DECLARE**.

**Syntax:**
```SQL
DECLARE @variabelnamn datatype;
```

**Exempel:**
```SQL
DECLARE @count INT;
DECLARE @name NVARCHAR(50);
DECLARE @today DATE;
```

### 📌 Tilldela värden till variabler
Du kan tilldela med SET eller SELECT.

1️⃣ **SET**

Bra för enskilda värden.
```SQL
SET @count = 10;
SET @name = 'Anna';
```

2️⃣ **SELECT**

Kan sätta flera variabler samtidigt och räknas ofta som mer flexibelt.

```SQL
SELECT @today = GETDATE(), 
       @count = 5;
```

### 📌 Tilldela från en SELECT-fråga
```SQL
SELECT @name = FirstName
FROM Customers
WHERE CustomerID = 12;
```

### 📌 Använda variabler

Du använder dem som vanliga värden i SQL:
```SQL
DECLARE @minAge INT = 18;

SELECT *
FROM Users
WHERE Age > @minAge;
```

**Code-along:**  
[008_Variabler.sql](https://github.com/everyloop/NEU25G-Databases/blob/main/SQL/008_Variabler.sql)

## Temporära tabeller
En temporär tabell är en tabell som skapas i systemdatabasen **tempdb** och automatiskt tas bort när:

- sessionen avslutas (lokala temporära tabeller)
- ingen längre använder den (globala temporära tabeller)
- eller ett scope/block avslutas (för vissa konstruktioner)

De används som en tillfällig arbetsyta för data vid komplexa frågor, ETL, loopar, mellanresultat m.m.

**Code-along:**  
[009_Tempor%C3%A4ra_tabeller_och_tabellvariabler.sql](https://github.com/everyloop/NEU25G-Databases/blob/main/SQL/009_Tempor%C3%A4ra_tabeller_och_tabellvariabler.sql)