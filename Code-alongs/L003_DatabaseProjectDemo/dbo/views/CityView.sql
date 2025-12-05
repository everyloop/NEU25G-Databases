
CREATE VIEW [dbo].[CityView] AS 
select 
	[ci].[CityId], 
	[ci].[Name] as 'City', 
	[co].[Name] as 'Country'
from
	Cities ci
	join Countries co on co.[CountryId] = ci.CountryID