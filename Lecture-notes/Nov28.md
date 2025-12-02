# November 28

**Lecture slides:**  
[Set-based programming.pdf](https://github.com/everyloop/NEU25G-Databases/blob/main/Resources/Set-based%20programming.pdf)  

## Views (Vy)
### ⭐ Vad är en VIEW?

En **view** är ett *sparat SELECT-statement* som beter sig som en virtuell tabell.

En view:
- innehåller **ingen egen data** (med några specialfall)
- lagrar endast **definitionen** av en fråga
- exekveras varje gång man selekterar från den
- fungerar som ett slags “fönster” mot underliggande tabeller

Man kan tänka:  

**Tabell = data**  
**View = fråga/vy över data**

### ⭐ Varför använder man views?

**✔ 1. Enklare frågor**

En komplicerad SELECT kan döpas till något enklare:
```SQL
SELECT * FROM vw_SalesSummary
```

**✔ 2. Säkerhet**

Man kan ge användare rätt att läsa från en view, **men inte direkt från tabellerna.**

**✔ 3. Abstraktion**

Viewen döljer tabellernas struktur från applikationer.

**✔ 4. Återanvändning**

Många system delar samma view-definitioner gränssnittsmässigt.

**✔ 5. Partitionering / filtrering**

Views kan visa en delmängd av en tabell.

### ⭐ Hur skapar man en view?
```SQL
CREATE VIEW dbo.vw_Employees
AS
SELECT EmployeeID, FirstName, LastName
FROM dbo.Employees;
```

Användning:
```SQL
SELECT * FROM dbo.vw_Employees;
```

### ⭐ Kan man uppdatera genom en view?

Ja — **om viewen är enkel** (en tabell, inga aggregat, inga joins), kan man:
- INSERT
- UPDATE
- DELETE

genom viewen, och operationen går direkt till tabellen.

Exempel:
```SQL
UPDATE dbo.vw_Employees
SET FirstName = 'Anna'
WHERE EmployeeID = 5;
```

→ Uppdaterar tabellen **Employees**.

**Men:**
Joinade eller aggregerande views är normalt read-only.

### ⭐ WITH SCHEMABINDING — vad är det?

```WITH SCHEMABINDING``` gör att viewen **låses till de exakta tabeller och kolumner den bygger på**.

Exempel:
```SQL
CREATE VIEW dbo.vw_Sales
WITH SCHEMABINDING
AS
SELECT OrderID, OrderDate
FROM dbo.Orders;
```

**Effekter av SCHEMABINDING:**

**✔ 1. Tabeller kan inte ändras så att viewen bryts**

Du kan inte:
- ta bort kolumner
- ändra datatyper
- byta schema
- droppa tabellen

…om en schemabunden view använder dem.

Exempel:
```SQL
ALTER TABLE dbo.Orders DROP COLUMN OrderDate; 
```

→ ❌ Misslyckas om viewen är schemabunden.

Det är alltså ett **skydd mot oavsiktliga förändringar**.

**✔ 2. Du måste referera objekt med schema**

t.ex. dbo.Orders — inte bara Orders.

**✔ 3. Du måste inkludera alla kolumner explicit**

Inga SELECT *.

Detta ger stabilare views.

