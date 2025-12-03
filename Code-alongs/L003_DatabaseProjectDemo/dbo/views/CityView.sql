CREATE VIEW [dbo].[CityView] AS 
select 
	[ci].[Id], 
	[ci].[Name] as 'City', 
	[co].[Name] as 'Country'
from
	Cities ci
	join Countries co on co.Id = ci.CountryID