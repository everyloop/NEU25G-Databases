
select 
	SubQuery.Namn, 
	upper(Namn) as 'uNamn', 
	lower(Namn) as' lNamn'
from
(
	select 
		Id as 'Personnummer',
		concat(FirstName, ' ', LastName) as 'Namn'
	from 
		Users
	where
		ID > '800101'
) SubQuery
where
	SubQuery.Personnummer < '900101'


select * from company.products;

SELECT
	cc.City,
	COUNT(*) AS 'total orders',
	COUNT(DISTINCT cp.Id) AS 'Unique orders',
	FORMAT((CAST(COUNT(DISTINCT cp.Id) AS float) / (select count(*) from company.products)), 'P1') AS 'Percent of total products'
FROM
	company.customers cc
	JOIN company.orders co ON cc.Id = co.CustomerId
	JOIN company.order_details cod ON co.Id = cod.OrderId
	JOIN company.products cp ON cod.ProductId = cp.Id
WHERE
	CC.City = 'London'
GROUP BY
	cc.City;

-------------

select * from music.artists
select * from music.albums


select
	AlbumId,
	Title,
	albums.artistid,
	(select top 1 Name from music.artists where artists.ArtistId = albums.ArtistId) as 'Artist'
from 
	music.albums

