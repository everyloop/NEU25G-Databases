# Januari 9

## 📌 Vad är MongoDB?

MongoDB är en **NoSQL-databas** av typen **dokumentdatabas**.

Till skillnad från relationsdatabaser (SQL Server, MySQL m.fl.):

- Ingen tabellstruktur  
- Ingen fast schema-definition  
- Data lagras som **dokument** (liknar JSON)

### Grundbegrepp

| MongoDB   | SQL-världen |
|------------|------------|
| Database   | Database   |
| Collection | Tabell     |
| Document   | Rad        |
| Field      | Kolumn     |


### Dokument

Ett dokument är ett objekt med key-value-par:

```json
{
  "_id": ObjectId("..."),
  "name": "Anna",
  "email": "anna@test.se",
  "age": 30
}
```

Egenskaper:

- Lagras internt som **BSON** (binärt JSON-format)  
- Kan innehålla:
  - subdokument
  - listor (arrays)
- Har **alltid ett `_id`**
  - Skapas automatiskt
  - Måste vara unikt


### Collections

- En collection är en samling dokument  
- Inget krav på att alla dokument har samma struktur  
- Skapas automatiskt när man:
  - sätter in data
  - skapar index



## 🖥️ mongosh (MongoDB Shell)

`mongosh` är MongoDBs **kommandoradsverktyg**.

Det är:
- Ett JavaScript-REPL
- Med inbyggda MongoDB-kommandon



### Ansluta

```bash
mongosh
```

Eller mot specifik server:

```bash
mongosh "mongodb://localhost:27017"
```


## Grundkommandon i mongosh

### Visa databaser
```js
show dbs
```

### Byt databas
```js
use myDatabase
```

### Visa collections
```js
show collections
```



## CRUD-operationer

### Create
```js
db.users.insertOne({
  name: "Anna",
  email: "anna@test.se",
  age: 30
})
```



### Read

Hämta alla:
```js
db.users.find()
```

Med filter:
```js
db.users.find({ name: "Anna" })
```

En rad:
```js
db.users.findOne({ name: "Anna" })
```



### Update

```js
db.users.updateOne(
  { name: "Anna" },
  { $set: { age: 31 } }
)
```



### Delete

```js
db.users.deleteOne({ name: "Anna" })
```



## Viktigt att veta

- MongoDB är **case-sensitive**
- Collections skapas automatiskt
- `_id` indexeras alltid
- Alla ändringar:
  - uppdaterar index automatiskt



## 🧭 MongoDB Compass

MongoDB Compass är ett **grafiskt verktyg (GUI)** för MongoDB.

Används för att:

- Se databaser och collections
- Bläddra i dokument
- Köra queries utan kod
- Skapa index
- Bygga aggregation pipelines visuellt



## Vad kan man göra i Compass?

### 1. Utforska data
- Klicka på collection
- Se dokument i tabellform
- Expandera objekt



### 2. Köra queries

Filter-rutan:
```json
{ "name": "Anna" }
```

Sort:
```json
{ "age": -1 }
```


### 3. Skapa index

Indexes-fliken:
- Se alla index
- Skapa nya
- Ta bort index


### 4. Aggregations

Visual builder för:
- group
- filter
- project
- sort

Bra för:
- statistik
- rapporter
- sammanställningar



## 🔑 Index (kort sammanfattning)

- Index gör sökningar snabba  
- Utan index → MongoDB läser hela collectionen  
- Med index → snabb uppslagning  

Exempel:

```js
db.users.createIndex({ email: 1 })
```

- `_id` har alltid index  
- Index uppdateras automatiskt  



## 📌 När ska man använda MongoDB?

MongoDB passar bra när:

- Data är flexibel
- Strukturen ändras ofta
- Man jobbar dokument-baserat
- Snabb utveckling prioriteras

Passar sämre när:

- Mycket komplexa relationer
- Transaktioner över många tabeller
- Strikt schema krävs



## 🎯 Sammanfattning

- MongoDB = dokumentdatabas  
- Data lagras som objekt (BSON)  
- mongosh = kommandoradsverktyg  
- Compass = grafiskt verktyg  
- CRUD fungerar likt SQL  
- Index är viktiga vid större datamängder  

### [Här finns dokumentationen till mongodb](https://www.mongodb.com/docs/development/)