declare @id nvarchar(100);
declare @name varchar(100);

declare myCursor CURSOR FAST_FORWARD for select id, FirstName from users;

open myCursor; 

fetch next from myCursor into @id, @name;

while @@FETCH_STATUS = 0
Begin
	print convert(varchar, @id) + ' = ' + @name; 
	fetch next from myCursor into @id, @name; 
end;

close myCursor; 
deallocate myCursor;