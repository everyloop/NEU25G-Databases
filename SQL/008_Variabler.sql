

declare @name nvarchar(MAX) = 'Fredrik';
declare @email nvarchar(MAX);

print @name;

set @name = 'Kalle';

print @name;

set @name = (select top 1 FirstName from users order by FirstName);

print @name;

-- Glöm inte top 1, annars hämtar den alla raderer och skriver @name och @email så många gånger.
select top 1 @name = FirstName, @email = Email from users;

print concat(@name, ' - ', @email);

-- ... eventuellt kan man använda += som ett trick för att konkatenera alla.
select @name += FirstName from Users;

print @name;

set @name = 'My'
select @email = email from Users where FirstName = @name;

print '----'
print @email;

-- Globala variabler: börjar på @@ -----------

select @@SPID

select * from Users where FirstName like 'a%';
select @@ROWCOUNT

