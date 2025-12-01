
alter procedure random @rows int = 1, @total float output as
begin
	declare @i int = 1;

	declare @t table
	(
		n int,
		r float
	);

	while @i <= @rows
	begin
		insert into @t values(@i, rand());
		set @i += 1;
	end

	select @total = sum(r) from @t;

	select * from @t;
end


declare @result float;

exec random 12, @total = @result output;

print @result;
