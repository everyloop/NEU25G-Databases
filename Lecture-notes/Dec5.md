# December 5

**Lecture slides:**  
[Optimering.pdf](https://github.com/everyloop/NEU25G-Databases/blob/main/Resources/Optimering.pdf)  

## 📌 Heap Table

En **heap** är en tabell **utan klustrat index**. Raderna ligger lagrade i godtycklig ordning i datablock (pages) utan någon bestämd sortering.

### ✔️ Fördelar

- **Snabb insättning** (INSERT) för vissa scenarier – eftersom SQL Server inte behöver hålla ordningen.
- Bra för staging / temporära batch-inläsningar.
- Kräver ingen underhållskostnad för klustrat index.

### ❌ Nackdelar
- **Dålig sökprestanda** utan andra index – SQL Server måste göra Table Scan.
- Forwarded records kan uppstå vid UPDATE av rader som inte får plats på sin ursprungliga page → ger fragmentering och slö queries.
- Restore av korrupta rader svårare – saknar stabil “rad-ankare” (RID istället för clustering key).

### 🕒 När använda?

Använd *endast* när:

- Tabell används som **staging/landing** i ETL-processer.
- Data läses sekventiellt utan behov av lookup.
- Data snabbt läses in och sedan töms.
- Du har få eller inga UPDATE-operationer.

**I [OLTP-system](https://www.geeksforgeeks.org/dbms/difference-between-olap-and-oltp-in-dbms/) är heap nästan alltid fel val.**

## 📌 Klustrat index (Clustered Index)

Ett klustrat index **bestämmer den fysiska sorteringsordningen** i tabellen. Tabellen är det klustrade indexet.

Varje tabell kan ha **max 1** klustrat index.

### ✔️ Fördelar

- **Snabb sökning på kolumn(er) i clustering key.**
- **Bra för intervallfrågor** (BETWEEN, > < osv).
- **Mer kompakt lagring** än heap (inga forwarded records).
- **Bra för JOINs** där clustering key används.

### ❌ Nackdelar

- **INSERT kan bli långsammare** om clustering key inte är monoton (t.ex. GUID → splits & fragmentation).
- **UPDATE av clustering key är dyrt** — flyttar hela raden.
- **Kräver underhåll** (index rebuild/reorganize).

## 🕒 När använda?

Nästan alltid – *men välj clustering key med omsorg:*

### Bra kandidater för clustering key:

- Monotont ökande värde (IDENTITY, SEQUENCE, datetime2 med kontroll)
- Små, stabila värden som ändras sällan
- Unika och selektiva kolumner

### Dåliga kandidater:

- GUID (särskilt NEWID) → mycket fragmentation
(NEWSEQUENTIALID är mycket bättre)
- “Feta” kolumner (t.ex. nvarchar(100))
- Kolumner som ofta uppdateras

## 📌 Oklustrade index (Nonclustered Index)

Ett nonclustered index är en separat datastruktur som pekar på rader via **clustering key** eller **RID** (om heap).

Som en bok:
- Klustrat index = hela boken sorterad
- Oklustrat index = innehållsförteckning med sidnummer

Du kan ha **många** nonclustered index på en tabell.

### ✔️ Fördelar

- **Gör sökningar mycket snabbare** på ofta använda kolumner.
- Möjlighet till **covering index** (där indexet innehåller alla kolumner queryn behöver).
- Förbättrar JOIN- och WHERE-prestanda utan att påverka tabellens ordning.

### ❌ Nackdelar

- **Påverkar INSERT/UPDATE/DELETE negativt** → alla index måste uppdateras.
- Tar extra lagringsutrymme.
- Kan fragmenteras kraftigt vid höga skrivvolymer.
- För många index = sämre totalprestanda.

### 🕒 När använda?

- För att snabba upp sökningar med **WHERE, JOIN, ORDER BY**.
- För kolumner med **hög selektivitet** (många unika värden).
- För queries som körs ofta och behöver bra prestanda.
- När du kan skapa ett **covering index** som drastiskt minskar IO.

### Undvik:
- Index på kolumner med mycket få distinkta värden (t.ex. kön, boolean) – ger liten nytta.
- För många index på tabeller med många writes.

## 📌 Kort jämförelse
| Typ                 | Struktur                  | Fördelar                         | Nackdelar                           | Vanligt användningsområde              |
| ------------------- | ------------------------- | -------------------------------- | ----------------------------------- | -------------------------------------- |
| **Heap**            | Osorterad lagring         | Snabba inserts, minimal overhead | Dålig sökning, forwarded records    | Staging, ETL, batch                    |
| **Klustrat index**  | Tabell sorterad efter key | Snabb sökning, stabil struktur   | Långsammare insättningar om fel key | OLTP-tabeller, primär lagringsstruktur |
| **Oklustrat index** | Separat index             | Snabbare queries, covering       | Långsammare writes, mer lagring     | Optimera specifika queries             |

## 📌 Rekommendationer (best practice)

### ✔️ 1. Undvik heap – använd klustrat index som standard

Enda undantaget är staging-tabeller.

### ✔️ 2. Välj rätt clustering key

Optimal key är:
- Stabil
- Small (4–8 bytes)
- Monotont ökande
- Selektiv

Vanligt: **INT IDENTITY** eller **BIGINT IDENTITY**.

### ✔️ 3. Skapa oklustrade index utifrån workloaden

- Analysera execution plans
- Indexera JOIN/WHERE/ORDER BY
- Håll antalet index rimligt

### ✔️ 4. Använd INCLUDE-kolumner för covering index

- Ökar prestanda stort utan att påverka sorteringsstrukturen.

### ✔️ 5. Underhåll index

- REBUILD → när fragmentering > 30%
- REORGANIZE → vid 5–30%