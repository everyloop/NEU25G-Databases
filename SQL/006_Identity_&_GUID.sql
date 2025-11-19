

select * from Products

drop table Products;

create table Products
(
	--id int primary key identity(100, 5),
	id uniqueidentifier primary key,
	name nvarchar(MAX) not null,
	price float
)

insert into Products values(newid(), 'Iphone', 24324);

select * from Products;
delete from Products;

truncate table Products;


select newid()

select top 1 * from users order by newid();

