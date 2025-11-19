--Ta ut data (select) från tabellen GameOfThrones på sådant sätt att ni 
--får ut en kolumn ’Title’ med titeln samt en kolumn ’Episode’ som visar 
--episoder och säsonger i formatet ”S01E01”, ”S01E02”, osv. 
--Tips: kolla upp funktionen format()

select * from GameOfThrones;

select
	Title,
	'S' + format(Season, '00') + 'E' + format(EpisodeInSeason, '00') as 'Episode',
	concat('S', format(Season, '00'), 'E', format(EpisodeInSeason, '00')) as 'Episode'
from 
	GameOfThrones


-- select format(123.45, '0.###')

-- select format(getdate(), 'yyyy-MM-dd HH:mm:ss', 'sv')

-- DateTime.Now().tostring("yyyy-MM-dd HH:mm:ss");

select 'hej' + cast(5 as nvarchar)

