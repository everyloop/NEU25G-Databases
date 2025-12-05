
create database IndexDemo;

use IndexDemo;

drop table DemoData;

-- Heap table
create table DemoData
(
	a int,
	b int,
	c int
);

insert into DemoData select value, 1000001 - value, abs(checksum(newid())) % 10 from generate_series(1, 1000000);

-- PK = Clustered Index
create table DemoData_pk
(
	a int primary key,
	b int,
	c int
);

insert into DemoData_pk select value, 1000001 - value, abs(checksum(newid())) % 10 from generate_series(1, 1000000);

-- a = Clustered Index, b = Nonclustered index
create table DemoData_pk_indexB
(
	a int primary key,
	b int,
	c int,
	index IX_DemoData_B nonclustered (b) 
);

insert into DemoData_pk_indexB select value, 1000001 - value, abs(checksum(newid())) % 10 from generate_series(1, 1000000);

-- a = Clustered Index, b = Nonclustered index (include c)
create table DemoData_pk_indexB_includeC
(
	a int primary key,
	b int,
	c int,
	index IX_DemoData_B_includeC nonclustered (b) include (c)
);

insert into DemoData_pk_indexB_includeC select value, 1000001 - value, abs(checksum(newid())) % 10 from generate_series(1, 1000000);

select count(*) from DemoData_pk_indexB_includeC;

select * from DemoData where b = 435634;


select * from DemoData_pk where b = 435634;
select c from DemoData_pk_indexB where b = 435634;
select c from DemoData_pk_indexB_includeC where b = 435634;



