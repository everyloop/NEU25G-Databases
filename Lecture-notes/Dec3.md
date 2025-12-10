# December 3


## 📌 Backupstrategi för SQL Server

Målet är:

1) **Skydda data** (både mot korruption och bortfall)

2) Kunna **återställa snabbt** (RTO – Recovery Time Objective)

3) **Minimera förlust** av data (RPO – Recovery Point Objective)

All backup-planering utgår från:

- Hur lång tid du har på dig att återställa
- Hur mycket data du får tappa
- Hur stor databasen är
- Hur viktigt systemet är

### 🔐 3-2-1-regeln (tillämpad på SQL Server)

| Regel              | Förklaring                                                    |
| ------------------ | ------------------------------------------------------------- |
| **3 kopior**       | Produktionsdata + minst 2 backuper                            |
| **2 olika medier** | T.ex. disk + bandsystem eller disk + moln                     |
| **1 offsite**      | backup som förvaras på annan plats (moln, separat datacenter) |

Många lägger därför:
- **Primär backup på disk/NAS**
- **Sekundär i moln eller offline lagring**
- **En kopia isolerad från nätverket** (mot ransomware)

## 💾 Backup-typer i SQL Server

### 1️⃣ Full backup

- Tar **hela databasen** (allt data)
- Basen för alla andra typer av backuper
- Krävs för restore
- Körs vanligtvis **dagligen** eller **veckovis** beroende på datamängd

**Restore-process:** Full → Diff → Log backuper (i ordning)

### 2️⃣ Differential backup

- Innehåller **ändringar sedan senaste full backup**
- Återställning: **full + senaste diff**
- Används för att **minska backupstorlek och återställningstid**
- Vanlig intervall: **dagligen** eller **flera gånger per dag**

### 3️⃣ Transaction log backup

- Sparar **loggar över alla transaktioner** sedan senaste log backup
- Endast tillgänglig i **Full** och **Bulk-Logged recovery mode**
- Tillåter **point-in-time restore**
- Körs ofta var **5–15:e minut** i kritiska system

**Fördel:** Minimalt datatapp  
**Nackdel:** Kräver plan och lagringsutrymme

### 4️⃣ Tail-Log Backup

- Tas **innan restore** om databasen är skadad
- Sista chans att rädda transaktionsloggen
- Kan endast tas om databasen fortfarande läser loggen
- Krävs för **point-in-time** återställning vid katastrof

## 🧭 Recovery Mode (Modellen styr restore-möjligheter)
| Recovery mode   | Kan återställas point-in-time?   | Kräver log backups? |
| --------------- | -------------------------------- | ------------------- |
| **Simple**      | ❌                                | ❌                   |
| **Full**        | ✔️                               | ✔️                  |
| **Bulk-logged** | ✔️ (men ej för bulk-operationer) | ✔️                  |

### När använda?

**Full:** Normalt produktionsläge.

**Bulk-logged:** Tillfälligt vid massinladdning för att minska loggstorlek.

**Simple:** Utveckling/Rapport/mindre kritiska databaser utan krav på point-in-time recovery.

### 📂 Restore-kedjan (väldigt förenklat)
```
Full
+ Differential (om används)
+ Transaction log backups (i rätt ordning)
(+ Tail log, om katastrofåterställning)
```

### ⚠️ Andra viktiga punkter

#### Testa restore!

- Backup är **värdelös** om du inte kan återställa.
- En .bak-fil som endast lagras på samma disk som databasen **= ingen backup**
- Kör regelbundet **restore-test** på sandlåda eller sekundär server.

## 📝 Typisk rekommenderad backup-plan (vanligt mönster)

**Full:** varje natt  
**Diff:** var 4–6 timme  
**Log:** var 5–15 minut  
**Offsite kopia:** dagligen  
**Restore-test:** varje månad
