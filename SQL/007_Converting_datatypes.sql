
-- Standard ISO-SQL, funkar med alla dialekter, alla DBMS.
select 'Value: ' + cast(5 as nvarchar)
select 5 + cast('6' as int)

-- T-SQL, funkar bara i MSSQLServer, 
-- har möjlighet att använda formateringskod som tredje parameter
select 'Value: ' + convert(nvarchar, 5)

-- Använda endast för att konverta TILL strängar,
-- Tar en formateringssträng, och har i princip ersat convert + formateringskod
select 'Value: ' + format(5, '0')



