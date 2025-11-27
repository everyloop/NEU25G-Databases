# November 24

**Lecture slides:**  
[ACID-Transaktioner.pdf](https://github.com/everyloop/NEU25G-Databases/blob/master/Resources/ACID-Transaktioner.pdf)  

## 📌 Vad är ACID?

ACID är en uppsättning egenskaper som **garanterar att transaktioner i en databas är säkra, konsekventa och pålitliga** — även vid krascher, strömavbrott eller samtidiga användare.

**Transaktion** = en grupp SQL-operationer som ska behandlas som en enda enhet.

**ACID** står för:

- **A**tomicity 
- **C**onsistency 
- **I**solation 
- **D**urability 

Vi går igenom dem.

### 1️⃣ A = Atomicity

En transaktion är **allt eller inget**.

SQL Server måste garantera:

✔️ Antingen lyckas alla operationer  
❌ Eller så rullas allt tillbaka (ROLLBACK)

Det får aldrig lämnas ett halvgjort tillstånd.

Exempel:
Överföra pengar mellan två konton:

```SQL
BEGIN TRAN;

UPDATE Accounts SET Balance = Balance - 100 WHERE ID = 1;
UPDATE Accounts SET Balance = Balance + 100 WHERE ID = 2;

COMMIT TRAN;
```

Om rad 2 misslyckas → båda ändringarna rullas tillbaka.

### 2️⃣ C = Consistency

Databasen måste gå från **ett giltigt tillstånd till ett annat giltigt tillstånd**.
Alla constraints måste vara uppfyllda före och efter transaktionen:

- FOREIGN KEY
- CHECK constraints
- UNIQUE
- NOT NULL
- Datatyper
- Trigger-regler

SQL Server släpper inte igenom en transaktion som bryter mot schema-regler — den rullas tillbaka.

### 3️⃣ I = Isolation

**Samtidiga transaktioner ska inte kunna störa varandra.**
Hur mycket de får se av varandra styrs av isolation levels.

Syftet är att undvika problem som:
- dirty reads
- non-repeatable reads
- phantom reads

Exempel på isolation levels i SQL Server:

- **READ UNCOMMITTED** (lägst, "dirty reads" tillåtna)
- **READ COMMITTED** (default)
- **REPEATABLE READ**
- **SNAPSHOT** (row-versioning)
- **SERIALIZABLE** (högst)

Ju högre isolering → desto fler lås → tryggare men långsammare.

###  D = Durability

När en transaktion är *committad* är den permanent — även vid:

- serverkrasch
- strömavbrott
- systemfel

SQL Server garanterar detta via:

- transaction log (log file)
- write-ahead logging (WAL)
- återställningsmekanismer (recovery)

Transaktionsloggen skrivs **innan** data skrivs till disken, för maximal säkerhet.

### 📌 Kort sammanfattning
| ACID-egenskap   | Betydelse                           |
| --------------- | ----------------------------------- |
| **Atomicity**   | Alla ändringar sker eller inga sker |
| **Consistency** | Databasregler får aldrig brytas     |
| **Isolation**   | Transaktioner stör inte varandra    |
| **Durability**  | Committade data överlever krascher  |

### 📘 Varför ACID är viktigt i SQL Server

Det gör att databasen:
- håller datan korrekt
- klarar samtidiga användare
- klarar systemfel
- beter sig förutsägbart

ACID är en av grundorsakerna till att relationsdatabaser fortfarande är industristandard för kritiska system.

## 🔥 Vad är SQL-injection?

**SQL-injection** är en attack där **angriparen skickar manipulerad text som innehåller SQL-kod** till ett system som bygger upp SQL-frågor från användarinmatning.

Målet är att:
- köra egen SQL-kod
- läsa data man inte ska se
- ändra eller radera data
- ta över systemet

SQL-injection beror **nästan alltid** på att applikationen bygger SQL med strängar, t.ex.:

```cs
"SELECT * FROM Users WHERE Name = '" + userInput + "'"
```

### 🚨 Exempel på SQL-injection

Anta att användaren ska skriva in sitt namn:

```SQL
SELECT * FROM Users WHERE Name = 'Anna'
```

En angripare skriver istället:

```SQL
' OR 1=1 --
```

Den färdiga frågan blir:

```SQL
SELECT * FROM Users WHERE Name = '' OR 1=1 --'
```

Effekt:
- **OR 1=1** gör villkoret alltid sant
- **--** kommenterar bort resten  
**→ Alla användare returneras.**

Detta är en klassisk SQL-injection.

### 🧨 Farligare exempel: radera tabell

Angriparen skriver:
```SQL
'; DROP TABLE Users; --
```


I applikationen blir det:

```SQL
SELECT * FROM Users WHERE Name = ''; DROP TABLE Users; --'
```

**→ Tabellen raderas** om applikationen och databasen tillåter flera statements, och om applikationen ansluter med en användare som har rättigheterna att ta bort tabellen.

### 🛡️ Hur förhindrar man SQL-injection?

Historiskt har det funnits flera försök till lösningar, men det "moderna" sättet, och det enda som alltid fungerar till 100% är:

## ⭐ PARAMETERIZED QUERIES

Parameterized queries (parametriserade frågor) innebär att **man skickar värden och SQL-kod separat**.

Det betyder:
- databasens query parser vet vad som är **kod** och vad som är **data**

användardata **KAN ALDRIG** tolkas som SQL-kod

Parametrar ser olika ut beroende på språk/ramverk:

### Exempel i C#:

```cs
var cmd = new SqlCommand(
    "SELECT * FROM Users WHERE Name = @name", conn);

cmd.Parameters.AddWithValue("@name", userInput);
```

Här skickas:
- SQL → "SELECT * FROM Users WHERE Name = @name"
- Värdet för @name → hanteras som **data**

SQL Server kommer **aldrig** köra användardatan som kod.

### 🛡️ Varför är parametrar säkra?

För att SQL Server behandlar värdet som en **literal**, inte som körbar SQL.

Jämför:

❌ Sårbart:

```cs
"... WHERE Name = '" + userInput + "'"
```

✔️ Säkert:
```SQL
... WHERE Name = @Name
```

Även om användaren skriver:

```SQL
' OR 1=1 --
```

så lagras det som en **sträng**, inte SQL-kod → och matchar normalt inga rader.

### 📌 Fördelar med parameterized queries

✔️ Förhindrar SQL-injection  
✔️ Bättre prestanda (query plan caching)  
✔️ Rätt hantering av datatyper  
✔️ Rätt hantering av specialtecken  
✔️ Enklare och mer robust kod  

Det är den *enda* fullständigt tillförlitliga metoden mot SQL-injection.

**Code-along:**  
[L002_SQL_injection_demo](https://github.com/everyloop/NEU25G-Databases/blob/main/Code-alongs/L002_SQL_injection_demo/Program.cs)