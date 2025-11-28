
drop table demoData;

create table demoData
(
	id int primary key identity(1, 1),
	timestamp datetime2 default(getdate())
)

GO

insert into demoData default values;

GO 100000

truncate table demoData;

GO

declare @i int = 0;

while @i < 100000
begin
	insert into demoData default values;
	set @i += 1;
end

GO

insert into demoData(timestamp)
select getdate() from generate_series(1, 100000);


GO

select * from demoData;


/*
SELECT name, compatibility_level
FROM sys.databases

ALTER DATABASE Everyloop
SET COMPATIBILITY_LEVEL = 160;
*/