# November 21

**Lecture slides:**  
[Joins.pdf](https://github.com/everyloop/NEU25G-Databases/blob/master/Resources/Join.pdf)  

## Join

En **JOIN** används i SQL för att kombinera rader från två eller flera tabeller baserat på ett gemensamt värde.

Exempel:
Koppla ihop Customers och Orders via CustomerID.

```SQL
FROM Customers
JOIN Orders ON Customers.CustomerID = Orders.CustomerID
```

JOIN gör det möjligt att presentera data som logiskt hör ihop men ligger i olika tabeller.`

## 📌 De vanligaste typerna av JOIN

Det finns 5 typer av join:

- **INNER JOIN**
- **LEFT JOIN** (Left Outer Join)
- **RIGHT JOIN** (Right Outer Join)
- **FULL JOIN** (Full Outer Join)
- **CROSS JOIN**

Vi går igenom dem en i taget.

## 1️⃣ INNER JOIN

Returnerar **endast rader som matchar i båda tabellerna.**

```SQL
SELECT *
FROM Customers c
INNER JOIN Orders o
    ON c.CustomerID = o.CustomerID;
```

Resultat:
Endast kunder som har gjort minst en order.

**Vanligaste JOIN-typen.**

## 2️⃣ LEFT JOIN (Left Outer Join)

Returnerar **alla rader från vänstra tabellen**, och matchande rader från högra tabellen.
Om ingen match finns → NULL-värden från högra tabellen.

```SQL
SELECT *
FROM Customers c
LEFT JOIN Orders o
    ON c.CustomerID = o.CustomerID;
```

Resultat:
- Alla kunder
- Order-information där match finns
- Kunder utan order får NULL i Order-kolumnerna

**Perfekt för att hitta saknade relationer.**

Exempel:
```SQL
WHERE o.CustomerID IS NULL   -- kunder utan ordrar
```

## 3️⃣ RIGHT JOIN (Right Outer Join)

Motsatsen till LEFT JOIN.
Returnerar **alla rader från högra tabellen**, och matchande från vänstra.

```SQL
SELECT *
FROM Customers c
RIGHT JOIN Orders o
    ON c.CustomerID = o.CustomerID;
```

Resultat:
Alla ordrar, och kunder där match finns.
Kunder som saknas sätts till NULL.

## 4️⃣ FULL JOIN (Full Outer Join)

Returnerar:
- matchande rader
- alla rader från vänstra tabellen
- alla rader från högra tabellen

Där det saknas match fylls NULL.

```SQL
SELECT *
FROM Customers c
FULL JOIN Orders o
    ON c.CustomerID = o.CustomerID;
```

**Används mest vid datamigrering, felsökning och jämförelser.**

## 5️⃣CROSS JOIN

Ger **alla kombinationer** mellan tabellerna (kartesisk produkt).

```SQL
SELECT *
FROM Products
CROSS JOIN Categories;
```

Exempel:
10 produkter × 5 kategorier → 50 rader.

**Används sällan**, men ibland för att generera kalendrar, testdata eller kombinatorik.

## Relationstyper
Relationstyper beskriver **hur rader i en tabell hör ihop** med rader i en annan tabell i en datamodell.
Relationen baseras på **primary keys** och **foreign keys**.

De påverkar:
- hur man strukturerar tabeller
- hur man undviker redundans (normalisering)
- hur man gör JOIN:ar i SQL
- hur data hänger ihop konsekvent

## 1️⃣ One-to-One (1–1)

En rad i tabell A **kan bara ha en matchande rad** i tabell B, och tvärtom.

**Exempel:**

Tabell A: Users  
Tabell B: UserProfiles

Varje användare har **exakt ett** profilobjekt.
```
Users 1 --- 1 UserProfiles
```

📌 **Används när:**

- man delar upp en stor tabell (vertikal partitionering)
- man lagrar känslig data separat
- man lagrar valfri extradata (t.ex. PremiumProfile som bara vissa har)

📌 **Implementering:**
- PK i Users = FK + PK i UserProfiles

## 2️⃣ One-to-Many (1–N / 1–många)

En rad i tabell A **kan ha många matchande rader** i tabell B.
Men en rad i tabell B hör till endast en rad i A.

**Exempel:**

En kund kan lägga många ordrar.
```
Customers 1 --- ∞ Orders
```

Orders har en foreign key CustomerID.

📌 **Används när:**

- en “ägare” har många “detaljer”
- klassiska master–detail-relationer

📌 **Implementering:**
- PK i Customers
- FK i Orders

## 3️⃣ Many-to-Many (M–N / många–många)

En rad i tabell **A kan vara kopplad till många rader** i tabell B.  
Och rader i tabell **B kan vara kopplade till många rader** i tabell A.

Direkt M–N existerar inte som tabell–tabell-relation i SQL Server. Man måste göra en **bridging/link/junction-tabell**.

📘 Exempel:

Studenter går många kurser, och kurser har många studenter.

```
Students ∞ --- ∞ Courses
            |
            ∞
         StudentCourses (länktabell)
```

📌 **Implementering:**

StudentCourses
- StudentID (FK → Students)
- CourseID (FK → Courses)
- Primärnyckel: alltid kombinerad PK på båda (eller surrogate key + unique constraint)

## 4️⃣ Self-referencing (rekursiv) relation

En rad i tabellen **refererar till en annan rad i samma tabell**.

📘 Exempel:

Employees har en manager som också är en employee:
```
Employees
-----------
EmployeeID (PK)
Name
ManagerID (FK → Employees.EmployeeID)
```

Relation:
```
Employees 1 --- ∞ Employees
          (self-referencing)
```
📌 **Används för:**
- organisationshierarkier (chefer → anställda)
- kategoriträd (kategori → underkategori)
- geografiska hierarkier (område → delområde)
- menystrukturer (menu → submenu)

📌 **Implementering:**

Fältet ManagerID är en foreign key till samma tabell.