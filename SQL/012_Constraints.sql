
drop table Users;


create table Users
(
	Id int,
	Username nvarchar(20) check(len(Username) >= 3) not null,
	CreatedAt datetime2 default(getdate())
	--constraint PK_Users primary key (Id, Username)
)



insert into Users (Id, CreatedAt) values (2, getdate());

insert into Users values (1, 'h', SYSDATETIME());

update users set Username = 'u' where id = 2

insert into Users (Id, Username) values (3, 'fredrik');
select *  from Users;




