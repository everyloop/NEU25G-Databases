
-- Aktuellt datum och tid
select getdate() as 'Serverns datum och tid som datetime'  ,sysdatetime() 'Serverns datum och tid som datetime2'

-- Beräkna skillnad i dagar (eller annan datepart t.ex year, minute etc.) mellan två datum 
select datediff(day, '1981-02-04', SYSDATETIME())

-- Addera (även negativ) x antal dagar/year/timmar etc till en datetime
select dateadd(hour, -5, getdate())

-- SET DATEFIRST 1; -- Konfigurera om SQL server så måndag är dag 1 i veckan.

select datepart(day, getdate())
select datepart(week, getdate())
select datepart(WEEKDAY, getdate())
select datepart(DAYOFYEAR, getdate())
