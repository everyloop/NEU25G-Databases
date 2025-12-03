
declare @playlist varchar(max) = 'Heavy Metal Classic';

select 
	g.Name as 'Genre',
	ar.Name as 'Artist',
	al.Title as 'Album',
	t.Name as 'Track',
	right('00' + cast(Milliseconds / 60000 as varchar(2)), 2) + ':' + right('00' + cast((Milliseconds % 60000) / 1000 as varchar(2)), 2) as 'Length',
	format(Milliseconds / 60000, '00') + ':' + format(Milliseconds / 1000 % 60, '00') as 'Length 2',
	format(dateadd(millisecond, milliseconds, 0), 'mm:ss') as 'Length 3', 
	concat(format(Bytes / POWER(1024.0, 2), 'N1'), ' MiB') as 'Size',
	Composer
from 
	music.tracks t
	join music.albums al on al.AlbumId = t.AlbumId
	join music.artists ar on ar.ArtistId = al.ArtistId
	join music.genres g on g.GenreId = t.GenreId
	join music.playlist_track pt on pt.TrackId = t.TrackId
	join music.playlists p on p.PlaylistId = pt.PlaylistId
where
	p.Name = @playlist






