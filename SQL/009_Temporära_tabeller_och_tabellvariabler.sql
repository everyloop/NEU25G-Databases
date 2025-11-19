--- Temporära tabeller ----------------

select * from #tempUsers;

select 
	id, 
	UserName, 
	concat(firstname, ' ', lastname) as name
into
	#tempUsers
from 
	Users 
where 
	FirstName like 'a%'

select * from #tempUsers;

--- Tabell-variabel ---------------

declare @people as table
(
	name nvarchar(MAX),
	age int
);

insert into @people values('Anna', 29);
insert into @people values('Frida', 24);
insert into @people values('Erik', 35);

select * from @people;

