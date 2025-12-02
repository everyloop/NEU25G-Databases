# December 1

Vi har kollat på hur man skapar logins och användare, samt delar ut rättigheter för vad de olika användarna får göra i en databas.

## 1️⃣ Login (servernivå)

**Login** är *serverprincipals*, alltså **användare på SQL Server-instansen**.

Du ser dem i SSMS:  
📌 *Security → Logins*

Ett login är det du använder för att **logga in** på SQL Server.

Kan vara:
- Windows-användare/grupp
- SQL login (“SQL authentication”)
- Managed identities/Azure AD (i molnet)

**Login = kan ansluta till servern.**  
Men login har *inte automatiskt* rättigheter i databaser.

## 2️⃣ Database User (databasanvändare)

I varje databas finns **users**.

Du ser dem i SSMS:  
📌 *Databas → Security → Users*

Ett **login måste mappas** till en user för att kunna göra något i databasen.

Man kan ha:
```SQL
LoginA → UserA i Databas1
LoginA → UserB i Databas2
```

eller
```SQL
LoginA → dbo (schema owner) i DatabasX
```

Viktigt:  
👉 En user måste ha ett login.  
👉 Ett login *kan* mappas till exakt **en user per databas**.

## 3️⃣ User-mapping (kopplingen mellan login och user)

I SSMS när du öppnar:

**Login → Properties → User Mapping**

…så definierar du:

- **vilka databaser loginet ska finnas i**
-  **vad usern ska heta i varje databas**
- **vilka database roles usern ska vara medlem i**

Det är här man kopplar samman:
- login (servernivå)
- user (databasenivå)

## 4️⃣ Server Roles (serveromfattande rättigheter)

Dessa gäller på *servernivå*, inte i databaser.

Finns i SSMS:
📌 *Security → Server Roles*

Vanliga:

| Server Role       | Betydelse                           |
| ----------------- | ----------------------------------- |
| **sysadmin**      | All behörighet, ignorerar ALLA DENY |
| **securityadmin** | Skapa logins, ge rättigheter        |
| **serveradmin**   | Starta/stoppa instans, konfigurera  |
| **processadmin**  | Döda sessioner                      |
| **dbcreator**     | Skapa databaser                     |


⚠️ *tänk på att DENY inte gäller för* ***sysadmin***

## 5️⃣ Database Roles (rättigheter inom en databas)

Finns i varje databas:  
📌 *Databas → Security → Roles → Database Roles*

Standardroller:
| Database Role         | Vad den får göra                       |
| --------------------- | -------------------------------------- |
| **db_owner**          | Full kontroll i databasen              |
| **db_datareader**     | Läs alla tabeller                      |
| **db_datawriter**     | Skriv till alla tabeller               |
| **db_ddladmin**       | Skapa/ändra schemaobjekt               |
| **db_backupoperator** | Ta backup av databasen                 |
| **public**            | Grundläggande rättigheter som alla har |

Du kan också skapa **egna roller** (best practice i företag).

En user kan vara medlem i flera database roles.

## 6️⃣ GRANT, DENY, REVOKE, WITH GRANT OPTION

Dessa är *objektnivå-rättigheter* (tabeller, vyer, stored procedures, schema mm.)

### ✔ GRANT

Ger en rättighet:
```SQL
GRANT SELECT ON dbo.Customer TO UserA;
```

UserA får SELECT.

### ✔ DENY

Tar bort och blockerar rättigheten:
```SQL
DENY SELECT ON dbo.Customer TO UserA;
```

**DENY övertrumfar GRANT** (utom för sysadmin).

### ✔ REVOKE

Tar bort GRANT *eller* DENY → återgår till default.

### ✔ GRANT ... WITH GRANT OPTION

Ger rättighet + rätt att ge den vidare:
```SQL
GRANT SELECT ON dbo.Customer TO UserA WITH GRANT OPTION;
```

UserA kan nu:
```SQL
GRANT SELECT ON dbo.Customer TO UserB;
```

💥 **Cascade revoke:**  
Om du tar bort UserA:s rättighet försvinner även de UserA gav vidare.

## ⭐ Hur allt hänger ihop (superviktigt)

Detta är SQL Servers säkerhetsmodell i ett nötskal:

**Login**
– användare på servernivå, kan ansluta till instansen

**User (per databas)**
– loginet “översätts” till en user i en databas

**Database roles**
– grupper av rättigheter inom databasen

**Server roles**
– rättigheter på servernivå

**GRANT / DENY / REVOKE**
– finmaskiga rättigheter på objektnivå

Och:

**DENY vinner alltid över GRANT**
(utom för sysadmin och vissa ownership-chaining-scenarier)

## ⭐ Kort exempel som knyter ihop allt
**1. Skapa login**
```SQL
CREATE LOGIN MyLogin WITH PASSWORD = 'StrongPass!';
```

**2. Mappa login till user i en databas**
```SQL
USE MyDB;
CREATE USER MyUser FOR LOGIN MyLogin;
```

**3. Lägg i database role**
```SQL
ALTER ROLE db_datareader ADD MEMBER MyUser;
```

**4. Ge extra rättigheter**
```SQL
GRANT UPDATE ON dbo.Customer TO MyUser;
```

Nu kan:
- login MyLogin ansluta till SQL-instansen
- user MyUser läsa allt (db_datareader)
- user MyUser uppdatera dbo.Customer

## ⭐ Sammanfattning
| Begrepp               | Nivå    | Funktion                                       |
| --------------------- | ------- | ---------------------------------------------- |
| **Login**             | Server  | Kan ansluta                                    |
| **User**              | Databas | Representerar login i databasen                |
| **Server Roles**      | Server  | Globala rättigheter                            |
| **Database Roles**    | Databas | Paket av rättigheter i DB                      |
| **GRANT / DENY**      | Objekt  | Rättigheter på specifika tabeller/vyer/stored procedures |
| **WITH GRANT OPTION** | Objekt  | Ge rättigheter vidare                          |
