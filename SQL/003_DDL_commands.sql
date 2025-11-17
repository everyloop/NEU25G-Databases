--DDL is a set of SQL commands used to create, modify, and delete database structures but not data. 
--These commands are normally not used by a general user, who should be accessing the database via an application.


create table Products(
	id int primary key,
	name nvarchar(40),
	price float
)

insert into Products values(1, 'Iphone', 15444);
insert into Products values(2, 'Iphone2', 19444);

select * from Products;

drop table Products;

