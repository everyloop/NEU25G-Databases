-- Radkommentar

/*
	Blockkommentar
*/

-- Välj vilken databas som är aktiv.
use ITHS;

-- SQL är case-insensitive, d.v.s gör ingen skillnad på små och stora bokstäver,
-- gäller data såväl som kolumnnamn, variabler etc.

-- Index börjar på 1.

-- Databas-termer:
-- Selection - Vilka rader vi filtrerar ut (görs med keyword Where)
-- Projection - Vilka kolumner vi efterfrågar (görs mellan keyword Select och From)

-- Ge oss alla kolumner (Våran selection = *), och alla rader.
--select * from students;

-- Använd en Where för att filtrera vilka rader vi tar ut, med ett (where-)vilkor.
--select * from students where lastName = 'johansson' and id < 4;


-- Projection exempel:
--select 
--	id, 
--	upper(firstName) as 'FirstName', 
--	id + 100, 
--	--*, 
--	'Fredrik' as 'Fredriks namn', 
--	'Hej ' + firstName as 'Greeting',
--	firstName + ' ' + lastName as 'FullName'

--from 
--	Students;

/*
	string literals använder 'single quotes', inte "double quotes" som i C#

	= lika med (istället för == som i C#)
	<> inte lika med (ISO-standard, men i T-SQL även != )
	< mindre än
	> större än
	<= mindre eller lika
	>= större eller lika

	and (motsvarar c# &&)
	or (motsvarar c# ||)
	not (motsvarar c# !)
*/


use everyloop;

-- Keyword top begränsar antal rader man får ut.
--select top 5 * from users;

-- In
-- select * from users where FirstName not in ('Ulla', 'Milla', 'Johanna');

-- Between
--select * from users where FirstName between 'b' and 'Caroline';
--select * from users where FirstName >= 'b' and FirstName <= 'Caroline';

-- Like
-- select * from users where FirstName like '[a-c]%'

-- Order by
--select * from users order by FirstName asc, LastName desc
-- select firstname from users order by len(firstname) desc;

-- Case when
--select 
--	firstname, 
--	case
--		when len(firstname) < 5 then 'Short'
--		when len(firstname) > 8 then 'Long'
--		else 'Medium'
--	end as 'LengthOfName'
--from 
--	users
--where
--	FirstName like '[adfk]%'
--order by 
--	len(firstname)

