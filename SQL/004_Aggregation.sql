select * from Elements where Mass is not null;

select 
	count(*) as 'Total number of rows', 
	count(Mass) as 'Number of values in Mass', 
	count(*) - count(Mass) as 'Number of NULL values in Mass',
	count(StableIsotopes) as 'Number of values in StableIsotopes'
from 
	Elements;

select * from Elements order by Boilingpoint;

select
	sum(Mass) as 'Sum of Mass',
	avg(BoilingPoint) as 'Average Boiling Point',
	min(BoilingPoint) as 'Min Boiling Point',
	max(BoilingPoint) as 'Max Boiling Point',
	string_agg(Symbol, ', ') as 'Symbols'
from
	Elements
where
	Number <= 10

select count(distinct Period) from Elements


select * from Elements where Period = 2


select 
	Period,
	--Valenceel,
	--[Group],
	count(*) as 'Number of elements',
	string_agg(Symbol, ', ') as 'List of symbols',
	avg(MeltingPoint) as 'Average Meltingpoint',
	avg(cast(Radius as float)) as 'Average Radius',
	sum(cast(Radius as float)) / count(*) as 'Average Radius, counting null as zero'
from 
	Elements 
where 
	len(symbol) = 2
group by 
	Period --, Valenceel
having 
	count(*) < 10


--order by
--	'Average Meltingpoint'

select * from Elements order by [Group];







