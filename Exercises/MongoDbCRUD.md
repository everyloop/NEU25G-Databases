# Övningsuppgifter med CRUD-operationer i MongoDB

Gör gärna övningsuppgifterna "var och en" i grupp, och jämför era resultat med varandra och diskutera vad ni gjort.

## Anslut mot en lokal MongoDB server:
1. Använd en databas med namn myDB.

2. Skapa ett dokument med innehåll FirstName: ”Selma”, LastName: Lagerlöf
och sätt in det i en kollektion med namn ”authors”.
3. Lägg till ytterligare ett dokument i ”authors” med FirstName: ”August”,
LastName: ”Bondeson”, Birth: 1854
4. Uppdatera dokumentet för August Bondeson och lägg till Death: 1906 
5. Lägg till ytterligare författare i ”authors” genom att Ladda ner [addAuthors.js](./addAuthors.js) och kör kommandot load(”addAuthors.js”) från mongo Shell. Alternativt öppna den filen och copy/pasta innehållet i mongosh.
6. Räkna hur många dokument det finns totalt i ”authors”.
7. Räkna hur många författare som heter August i förnamn.
8. Lägg till Birth: 1858 och Death: 1940 för Selma Lagerlöf
9. Lägg till en array ”Books” för Selma Lagerlöf med följande böcker:
”Gösta Berlings saga”, ”En herrgårdssägen”,
”Nils Holgerssons underbara resa genom Sverige”
10. Lägg till boken ”Vi på Saltkråkan” bland Astrid Lindgrens böcker.
11. Ta bort boken ”Bröderna Lejonhjärta” från Astrid Lindgrens böcker.
12. Visa dokument för de författare som dog år 2000 eller senare.
13. Räkna hur många författare som dog på 1940-talet.
14. Visa endast attributen FirstName, LastName och Death för de författare
som dog på 1940-talet.
15. Lägg till Gender: ”Male” i dokument med FirstName = ”Hjalmar”.
16. Lägg till Gender: ”Female” i dokument där FirstName är Astrid, Karin eller
Selma.
17. Ta bort arrayen Books från dokumentet med August Strindberg.
18. Ta bort dokumenten där FirstName = ”August”. 