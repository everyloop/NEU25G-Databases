
drop table Countries;
drop table Cities;

create table Countries
(
	Id int,
	Name nvarchar(max)
);

insert into Countries values(1, 'Sweden');
insert into Countries values(2, 'Norway');
insert into Countries values(3, 'Denmark');
insert into Countries values(4, 'Finland');

create table Cities
(
	Id int,
	Name nvarchar(max),
	CountryId int
)

insert into Cities values(1, 'Stockholm', 1);
insert into Cities values(2, 'Gothenburg', 1);
insert into Cities values(3, 'Oslo', 2);
insert into Cities values(4, 'Bergen', 2);
insert into Cities values(5, 'Stavanger', 2);
insert into Cities values(6, 'Copenhagen', 3);
insert into Cities values(7, 'London', 5);

/*
select * from Countries;
select * from Cities;
*/

select * from Cities inner join Countries on Cities.CountryId = Countries.Id