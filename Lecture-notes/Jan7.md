# Januari 7

**Lecture slides:**  
[Introduktion till NoSQL.pdf](https://github.com/everyloop/NEU25G-Databases/blob/main/Resources/Introduktion%20till%20NoSQL.pdf)  

## 📌 NoSQL – kort förklaring

**NoSQL** (“Not Only SQL”) är ett samlingsnamn för databaser som inte i första hand bygger på den traditionella relationsmodellen med tabeller och joins. De är istället designade för att hantera **stora datamängder, distribuerade system och flexibla datastrukturer**.

### Typiska egenskaper
- **Schemafritt eller flexibelt schema** (strukturen kan variera mellan poster)
- **Horisontell skalning** (lätt att sprida data över många servrar)
- Optimerade för **prestanda och specifika användningsfall**

NoSQL är inte en ersättare för relationsdatabaser, utan **ett komplement som löser andra typer av problem.**
### Vanliga typer av NoSQL-databaser

NoSQL är ett paraplybegrepp som innefattar många olika typer av databaser. I denna kurs ger vi en översikt över de 4 vanligaste:
<div style="margin-left: 20px;">
1️⃣ <b>Dokumentdatabaser</b> (t.ex. MongoDB) – lagrar JSON-liknande dokument<br>
2️⃣ <b>Key-value-databaser</b> (t.ex. Redis) – enkla, snabba uppslag<br>
3️⃣ <b>Grafdatabaser</b> (t.ex. Neo4j) – relationer i nätverk/grafer<br> 
4️⃣ <b>Wide-column stores </b> (t.ex. Cassandra) – stora, distribuerade dataset<br>
</div><br>

På [db-engines.com](https://db-engines.com/en/ranking_categories) hittar du alla vanliga databaser nedbrutna i de olika kategorierna.

### När används NoSQL?
- webbappar med mycket trafik  
- stora datamängder / Big Data  
- realtidsanalys, loggar, IoT  
- när datamodellen behöver vara flexibel

## 1️⃣ Dokumentdatabaser – översikt

Dokumentdatabaser är en typ av NoSQL-databas där data lagras som **dokument** i stället för i tabeller och rader.  
Dokumenten är oftast **JSON- eller JSON-liknande strukturer** och kan ha olika fält och form beroende på innehållet.

### Centrala egenskaper
- Schema är **flexibelt** — poster behöver inte ha exakt samma struktur  
- Data lagras som **hela objekt** snarare än uppdelade rader  
- Bra stöd för **inbäddade objekt och hierarkiska strukturer**  
- Skalar enkelt **horisontellt över flera servrar**, förutsatt att datamodellen är utformad för det.

De lämpar sig särskilt för:
- applikationer med **snabbt föränderliga datamodeller**  
- webb- och mobilappar  
- loggar, användarprofiler, events, IoT-data  



## MongoDB – dokumentdatabas i praktiken

MongoDB är en av de mest använda dokumentdatabaserna och lagrar data som dokument i formatet **BSON** (binär JSON).

### Grundidéer i MongoDB
- En databas innehåller **collections** (ungefär som tabeller)  
- En collection innehåller **documents** (BSON-objekt)  
- Fält kan skilja sig mellan dokument i samma collection  
- Relationer hanteras ofta via **inbäddning** i stället för joins  

### Exempel på dokument
```json
{
  "name": "Anna",
  "age": 22,
  "courses": ["Databaser", "Webbutveckling"]
}
```

### Styrkor med MongoDB
- mycket bra för **snabb utveckling och prototyper**  
- prestanda vid stora datamängder och distribuerade system  
- naturlig matchning mot objekt i moderna programmeringsspråk  

### Begränsningar
- saknar traditionella SQL-joins (även om aggregering finns)  
- mindre strikt dataintegritet än relationsdatabaser  
- kräver eftertanke kring struktur för att undvika duplicerad data  

## 2️⃣ Key-value-databaser – översikt

Key-value-databaser lagrar data som **par av nyckel och värde**, ungefär som en stor, distribuerad dictionary eller hash-table.  
Varje post nås via sin **unika nyckel**, och databasen bryr sig inte om värdets interna struktur.

### Centrala egenskaper
- extremt **snabba uppslag och skrivningar**  
- mycket enkel datamodell (key → value)  
- lämpar sig väl för **cache, sessioner och temporär data**  
- ofta byggda för **horisontell skalning och hög prestanda**

Värden kan vara:
- strängar  
- nummer  
- binär data  
- ibland mer avancerade strukturer (beroende på databasen)

Key-value-databaser prioriterar **prestanda före komplexa frågor** — till skillnad från relationsdatabaser, som prioriterar struktur och relationer.

## Redis – key-value-databas i praktiken

Redis är en mycket populär och extremt snabb **in-memory** key-value-databas.  
Den lagrar data främst i RAM, vilket ger **mycket låg latens och hög prestanda**.

### Grundidéer i Redis
- arbetar med datatyper som **strings, lists, sets, sorted sets, hashes**  
- data lagras **primärt i minnet**, men kan även skrivas beständigt till disk, och återställas vid omstart.  
- vanliga användningsområden:
  - cache-lager  
  - sessionshantering (t.ex. webbappar)  
  - meddelandeköer och pub/sub  
  - räknare, rate-limiting, realtidsdata  

### Exempel (ett värde lagrat under en nyckel)
```
key: "user:1001"
value: "{ name: 'Anna', points: 42 }"
```

### Styrkor
- extrem prestanda  
- enkel modell  
- mycket bra för **snabb åtkomst-data**

### Begränsningar
- inte lämpad för komplexa frågor eller relationer  
- kräver eftertanke kring minnesanvändning  
- mindre bra för långlivad, tungt strukturerad data  

## 3️⃣ Grafdatabaser – översikt

**I grafdatabaser är relationerna egna, lagrade objekt** — lika viktiga som noderna — och databasen är optimerad för att följa dem direkt.

I stället för tabeller lagras information som en **graf av noder och relationer**.

- **Noder** = objekt / entiteter (t.ex. person, produkt, plats)  
- **Relationer** = kopplingar mellan noder (t.ex. känner, köpte, tillhör)  
- Relationer har **riktning och kan bära data**

Grafdatabaser är särskilt bra när frågorna handlar om **hur saker hänger ihop**.

### Centrala egenskaper
- extremt starka vid **nätverks- och relationsbaserade frågor**
- relationer navigeras utan tunga joins
- bra för **komplex, sammanlänkad data**
- modellen är nära hur vi ofta tänker om verkligheten

Vanliga användningsområden:
- sociala nätverk  
- rekommendationssystem  
- bedrägeriupptäckt  
- nätverk / organisationsstrukturer  
- kunskapsgrafer  


## Neo4j – grafdatabas i praktiken

Neo4j är den mest kända och mest använda **property graph-databasen**.

Den använder:
- **noder** med egenskaper  
- **relationer** med egenskaper  
- frågespråket **Cypher**, som är designat för att uttrycka grafförfrågningar på ett läsbart sätt

### Exempel på graf (förenklat)

En person som **känner** en annan:

```cypher
(:Person { name: "Anna" })-[:KNOWS]->(:Person { name: "Erik" })
```

En fråga som hittar Annas vänner:

```cypher
MATCH (a:Person { name: "Anna" })-[:KNOWS]->(friend)
RETURN friend;
```

Här är relationen själva kärnan i modellen — inte en join som i SQL.

### Styrkor med Neo4j
- mycket bra för **relationstunga domäner**
- intuitiv datamodell
- hög prestanda för traverseringar (”följ relationer”)
- Cypher är lättläst och uttrycksfullt

### Begränsningar
- mindre lämpad för **stora batch-operationer över många datapunkter**
- svagare vid strikt tabellstruktur och aggregering i stor skala
- kräver annan modellering än i relationsdatabaser  

## 4️⃣ Wide-column stores – översikt

Wide-column-databaser är designade för att hantera **stora datamängder i distribuerade system**, med fokus på **snabba skrivningar och förutsägbara läsningar**.

Till skillnad från klassiska relationsdatabaser, där modellen utgår från tabeller och normalisering, är wide-column-databaser **åtkomstmönster-orienterade**.
  
Man designar tabeller utifrån **hur datan ska läsas**, inte för generell användning.

### Centrala idéer
- Data organiseras runt en **partition key**  
- All data som hör till samma nyckel lagras tillsammans i en **partition**
- En partition kan innehålla **många rader över tid** (t.ex. händelser eller mätvärden)
- Rader inom samma partition kan ha **olika kolumner**
- Mycket bra för **loggar, händelsedata, tidsserier och IoT-flöden**

Wide-column-stores prioriterar:
- **skrivprestanda och skalbarhet**
- **distribuering och replikering över många noder**
- **tillgänglighet framför komplexa ad-hoc-frågor**

De används ofta när systemen behöver hantera:
- stora mängder **kontinuerliga händelser**
- **geo-distribuerad drift**
- **höga krav på uppetid och failover**

## Cassandra – wide-column store i praktiken

Apache Cassandra är en av de mest kända **distribuerade wide-column-databaserna**.  
Den är utvecklad för system som behöver **mycket hög skalbarhet, tillgänglighet och skrivkapacitet**.

### Grundidéer i Cassandra
- Data organiseras i **keyspaces → tables → partitioner**
- Varje tabell designas efter ett **specifikt åtkomstmönster**
- Frågor görs nästan alltid via **partition key**
- Konsistensnivå kan **justeras per operation** (t.ex. strong vs eventual)

### Typiska användningsområden
- loggar och event-strömmar  
- tidsseriedata och mätvärden  
- IoT och sensordata  
- telemetry, användarhändelser, aktivitetshistorik  

### Designfilosofi jämfört med RDBMS
- **Relationsdatabas (t.ex. SQL Server):**  
  generell modell, normalisering, många olika frågor möjligt  
- **Cassandra:**  
  varje tabell designas för **en specifik fråga / åtkomstväg**  
  (andra typer av frågor är ofta ineffektiva eller otillåtna)

### Styrkor
- extremt bra vid **skrivtunga arbetslaster**
- horisontell skalning över många noder
- hög tillgänglighet och inbyggd replikering
- förutsägbara uppslag via partition key

### Begränsningar
- svagare för **ad-hoc-frågor och aggregat**
- saknar joins och komplex relationslogik
- inte avsedd som analys- eller datalagerlösning

## 📌 Normalisering vs denormalisering i NoSQL

I klassiska relationsdatabaser strävar man efter **normalisering** – att undvika redundant data genom att dela upp information i flera tabeller och använda relationer (joins).  
Syftet är dataintegritet, konsekvens och flexibilitet för många olika typer av frågor.

I många NoSQL-databaser gäller istället en annan princip:

### 🟣 Denormalisering som medvetet designval
- Data **dupliceras medvetet** för att optimera specifika åtkomstmönster  
- Varje tabell / struktur utformas för **en viss typ av fråga**  
- Integritet hanteras ofta i **applikationslogik eller synkprocesser**, inte via joins

Denormalisering gör att systemen kan erbjuda:
- **snabba, förutsägbara läsningar**
- bättre skalbarhet i distribuerade miljöer
- enklare hantering av stora händelse- och loggflöden

---

### 🗂 Exempel i de NoSQL-modeller vi går igenom

- **Dokumentdatabaser (MongoDB)** – relaterad data lagras ofta **inbäddad i ett dokument**  
- **Key–value (Redis)** – varje nyckel representerar ofta en **färdig vy av data**  
- **Grafdatabaser (Neo4j)** – relationer lagras explicit istället för att beräknas via joins
- **Wide-column (Cassandra)** – samma data kan lagras i flera tabeller för olika frågor  

> Kort sagt: i NoSQL prioriterar man **åtkomstmönster och prestanda** före strikt normalisering.

## När bör man välja vilken databas?

| Databas | Datatyp / modell | Styrkor | Begränsningar | När den passar bäst |
|--------|------------------|--------|--------------|--------------------|
| **SQL Server** | Relationsdatabas | Stark dataintegritet, transaktioner, flexibla frågor, mogna verktyg | Mindre flexibel struktur, svagare vid mycket stora distribuerade system | Klassiska verksamhetssystem, order/ekonomi, tydliga relationer |
| **MongoDB** | Dokumentdatabas | Flexibel struktur, inbäddad data, snabb utveckling | Mer duplicerad data, svagare vid komplexa relationer | Webb- & mobilappar, innehåll, profiler, händelsedata |
| **Redis** | Key–value i minnet | Extrem prestanda, cache, sessioner, TTL, counters/queues | Ingen generell query, kräver RAM, ej primär databas | Delad cache, sessioner, tokens, realtidsräknare |
| **Neo4j** | Grafdatabas | Stark på relationer & traverseringar, Cypher är uttrycksfullt | Svag för tabelliknande data och tunga batcher | Sociala grafer, rekommendationer, nätverk, bedrägeriupptäckt |
| **Cassandra** | Wide-column store | Skrivtung skala, hög tillgänglighet, förutsägbara uppslag | Begränsad adhoc-frågor, inga joins, denormalisering | Loggar, IoT, tidsserier, distribuerad händelsedata |

> Kort sagt: olika databaser är optimerade för **olika typer av data och användningsmönster** — därför väljer man teknik utifrån problemet, inte tvärtom.

I moderna system är det dessutom vanligt att använda flera databastyper samtidigt (polyglot persistence) — t.ex. SQL Server för transaktioner, Redis för cache och MongoDB för loggar.

## Varför NoSQL uppstod – och varför gapet har minskat

Under 2000-talet växte nya typer av system fram (webbplattformar, sociala nätverk, globala onlinetjänster) med krav som var ovanliga tidigare:

- mycket stora datamängder  
- hög skrivvolym och kontinuerliga händelser  
- behov av horisontell skalning över många servrar  
- flexibel datastruktur som kunde förändras snabbt

Samtidens relationsdatabaser var starka på **transaktioner, struktur och integritet**, men saknade stöd för dessa nya behov. Därför växte olika **NoSQL-databaser** fram, optimerade för specifika användningsmönster såsom dokumentdata, cache, grafer eller stora distribuerade skrivflöden.

Sedan dess har dock moderna RDBMS (som SQL Server och PostgreSQL) utvecklats kraftigt – med funktioner som bättre replikering, partitionering, in-memory-tabeller, columnstore-index, JSON-stöd och molnbaserad skalning. Det gör att de idag kan lösa många problem som tidigare krävde NoSQL.

> Kort sagt: NoSQL uppstod för att lösa verkliga skalnings- och flexibilitetsproblem som dåtidens RDBMS inte hanterade.  
> I dag är skillnaderna mindre, men NoSQL-databaser är fortfarande starkast där behoven är **extremt stora, distribuerade eller specialiserade**, medan relationsdatabaser förblir förstahandsvalet i många verksamhetssystem.
