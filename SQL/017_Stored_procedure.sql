
alter procedure myProcedure @name nvarchar(20), @timestamp datetime2 as
begin
	print concat('Hello ', @name, '!');

	select * from Countries;

	return len(@name);
end

GO

declare @length int;
exec @length = myProcedure 'Fredrik', '2024-10-12';
print @length;


-- drop procedure myProcedure;
