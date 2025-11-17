-- Radkommentar

/*
	Blockkommentar
*/

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
select 
	id, 
	upper(firstName) as 'FirstName', 
	id + 100, 
	--*, 
	'Fredrik' as 'Fredriks namn', 
	'Hej ' + firstName as 'Greeting',
	firstName + ' ' + lastName as 'FullName'

from 
	Students;

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

