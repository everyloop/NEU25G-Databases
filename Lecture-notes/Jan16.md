# Januari 16

## Vad är Git?
- Git är ett **versionshanteringssystem**
- Skapat av Linus Torvalds
- Används för att:
  - spara historik
  - samarbeta
  - kunna ångra ändringar
- Git är **lokalt** → allt finns på din dator

## Vad är GitHub?
- En **molntjänst** som hostar Git-repon
- Gör det möjligt att:
  - samarbeta
  - göra code reviews
  - hantera issues
- GitHub ≠ Git  
  (GitHub är bara en plattform ovanpå Git)

## Repository (repo)
- En mapp som innehåller:
  - dina filer
  - en dold `.git`-mapp

- `.git` innehåller:
  - historik
  - brancher
  - metadata


## Commit
- En **snapshot** av projektet
- Innehåller:
  - alla filer
  - metadata:
    - author
    - committer
    - tid
    - meddelande
    - parents



## Grundläggande arbetsflöde

1. Ändra filer  
2. Stage (`git add`)  
3. Commit (`git commit`)  
4. Push (`git push`)  
<br>


## Viktiga begrepp

### Working directory
- Dina aktuella filer
- Ändringar som **inte är sparade i Git**

### Staging area
- Vad som ska med i nästa commit

### Local repository
- Alla dina commits lokalt

### Remote repository
- T.ex GitHub


## Vanliga kommandon (konceptuellt)

| Handling | Kommando |
|------------|------------|
| Skapa repo | `git init` |
| Se status | `git status` |
| Stage filer | `git add` |
| Commit | `git commit` |
| Push | `git push` |
| Fetch | `git fetch` |
| Pull | `git pull` |
| Byt branch | `git switch` |

*(I Visual Studio / VS Code görs detta via UI)*



## HEAD

- HEAD = **var du står just nu**
- Pekar:
  - oftast på en branch
  - ibland direkt på en commit (detached HEAD)
- HEAD pekar **alltid på en commit**
- Ocommittade ändringar är **utanför Git**


## Branches

### Vad är en branch?
- En **pekare** till en commit
- När du skapar en branch:
  - ingen ny commit
  - bara en ny label

### Varför branches?
- Jobba parallellt
- Testa nya funktioner
- Skydda main


### Best practice

> ❌ Jobba inte direkt i main  
> ✔️ Skapa branch per funktion

### Exempel på branchnamn

````text
feature/login
bugfix/nullref
hotfix/crash
````

## Merge

- Slår ihop två brancher
- Skapar ofta:
  - en ny commit
  - med två parents

### Konflikter
- Uppstår när:
  - samma kod ändrats
- Flöde:
  1. fixa i fil
  2. stage filen
  3. commit merge


## Rebase

- Flyttar commits
- Skriver om historik
- Används:
  - lokalt
  - inte på pushad kod


## Pull request (PR)

- Finns på GitHub (inte i Git)
- Ett förslag att:
  - merge:a branch → main

### Typiskt flöde

1. Skapa branch  
2. Jobba & commit  
3. Push branch  
4. Skapa PR  
5. Review  
6. Merge  
7. Ta bort branch  


### Branch protection

- Förhindrar:
  - push direkt till main
- Kräver:
  - PR
  - review
  - tester



## Reset vs Revert

### Reset
- Flyttar branchen bakåt
- Skriver om historik
- Bara lokalt
- Används när:
  - inte pushat

### Revert
- Skapar ny commit
- Ångrar tidigare commit
- Säkert även efter push


## Reflog

- Logg över:
  - var HEAD varit
- Räddar:
  - borttappade commits
- Lokalt bara


## Stash

- Tillfällig förvaring
- Används när:
  - vill byta branch
  - men inte klar

## .gitignore

- Fil som säger:
  - vad Git ska ignorera
- Exempel:

````text
*.dat
bin/
obj/
````

- Ignorerade filer:
  - syns inte som untracked
  - kan inte committas


## Detached HEAD

- När du:
  - checkar ut en commit
- Bra för:
  - läsa gammal kod
  - testa
- Inte bra för:
  - riktigt arbete


## Viktiga tumregler

- main ska alltid vara stabil  
- Jobba i branch  
- Commit ofta  
- Skriv bra commit messages    
- Push är att dela – commit är lokalt  

# Sammanfattning

Git:
- versionshantering
- lokalt

GitHub:
- samarbete
- PR
- reviews

Brancher:
- isolerat arbete

Merge:
- slå ihop

Revert:
- ångra säkert

Reset:
- ångra lokalt

<br>

> *Git är historiken.  
> GitHub är samarbetet.*
