# Januari 12

## 📊 Aggregation Pipelines i MongoDB

Aggregation pipelines används när vi vill:

- sammanställa data  
- filtrera  
- gruppera  
- räkna  
- skapa rapporter/statistik  


## Grundidé

En aggregation pipeline är **en kedja av steg där varje steg bearbetar resultatet från föregående steg.**

Syntax:

```js
db.myCollection.aggregate([
  { $match: {...} },
  { $group: {...} },
  { $sort: {...} }
])
```

Varje steg börjar med **$**.





## Viktiga pipeline-steg

### 🔹 $match

Filtrerar dokument  
=Motsvarar WHERE

```js
{ $match: { age: { $gt: 30 } } }
```

Tips:

- Lägg `$match` tidigt → bättre prestanda  
- Kan använda index  

---

### 🔹 $project

Väljer vilka fält som ska med  
=Motsvarar SELECT

```js
{ $project: { name: 1, email: 1, _id: 0 } }
```

- `1` = inkludera  
- `0` = exkludera  

Kan även:

- byta namn på fält  
- skapa nya fält  

---

### 🔹 $group

Grupperar data  
=Motsvarar GROUP BY

```js
{
  $group: {
    _id: "$city",
    count: { $sum: 1 }
  }
}
```

Vanliga operatorer:

- `$sum`  
- `$avg`  
- `$min`  
- `$max`  
- `$push`  

---

### 🔹 $sort

Sorterar resultat

```js
{ $sort: { age: -1 } }
```

- `1` = stigande  
- `-1` = fallande  

---

### 🔹 $skip

Hoppar över dokument  
= Pagination

```js
{ $skip: 10 }
```

---

### 🔹 $limit

Begränsar antal resultat

```js
{ $limit: 5 }
```

---

### 🔹 $lookup

Join mellan collections  
= Motsvarar JOIN

```js
{
  $lookup: {
    from: "orders",
    localField: "_id",
    foreignField: "userId",
    as: "orders"
  }
}
```

Resultat:

- nytt fält (`orders`)  
- innehåller array med matchande dokument  

---

### 🔹 $out

Skriver resultatet till ny collection

```js
{ $out: "resultCollection" }
```

- Skapar eller ersätter collection  
- Pipeline-resultatet sparas  

---

### 🔹 $merge

Mer flexibel variant av `$out`

```js
{
  $merge: {
    into: "summary",
    whenMatched: "replace",
    whenNotMatched: "insert"
  }
}
```

Kan:

- uppdatera befintliga dokument  
- infoga nya  
- styra hur merge sker  




## Views

En **view** är:

> **En sparad aggregation pipeline som beter sig som en collection.**

Skapas med:

```js
db.createView(
  "activeUsers",
  "users",
  [
    { $match: { isActive: true } }
  ]
)
```

Egenskaper:

- lagrar **ingen data**  
- kör pipeline varje gång  
- alltid uppdaterad  
- kan användas som vanlig collection  

Bra för:

- rapporter  
- förenkla queries  
- “virtuella tabeller”  



## När använder man aggregation?

När du behöver:

- statistik  
- summeringar  
- top-listor  
- join-liknande logik  
- datatransformation  



## Viktiga tips

✔️ `$match` tidigt → bättre prestanda  
✔️ Pipelines är ordningskänsliga  
✔️ Resultatet går vidare steg för steg  
✔️ Kan byggas visuellt i Compass  



## Jämförelse med SQL

| SQL | MongoDB |
|------|----------|
| WHERE | $match |
| SELECT | $project |
| GROUP BY | $group |
| JOIN | $lookup |
| ORDER BY | $sort |
| LIMIT | $limit |
| OFFSET | $skip |



# 🎯 Sammanfattning

- Aggregation pipeline = kedja av steg  
- Varje steg transformerar resultatet  
- Kraftfullt verktyg för:
  - rapporter  
  - statistik  
  - sammanställningar  
- Kan:
  - visas i Compass  
  - sparas som views  
  - skrivas till collection  
