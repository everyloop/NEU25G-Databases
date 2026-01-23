create table productsWithJson
(
	Id int primary key identity(1, 1),
	Name nvarchar(50),
	Price decimal,
	OptionalInfo nvarchar(max) check (OptionalInfo is null or IsJson(OptionalInfo) = 1)
)

declare @json nvarchar(max) = json_object('color':'Black', 'NumberOfKeys': 108);
insert into productsWithJson values ('Keyboard108', 199, @json);

set @json = json_object('color':'Black');
insert into productsWithJson values ('iPhone 13 black', 9876, @json);

set @json = json_object('color':'White');
insert into productsWithJson values ('iPhone 13 white', 9876, @json);

select * from productsWithJson where json_value(OptionalInfo, '$.NumberOfKeys') > 100;

-- Se även json_modify för att uppdatera json-värden.

