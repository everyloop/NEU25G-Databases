

--insert into Students (id, lastName) values(10, 'Johansson');
--insert into Students values(11, 'Emil', 'Johansson');

--select * from Students;

--select 
--	id, 
--	firstName + ' ' + lastName as 'Name' 
----into 
----	newStudents3 
--from 
--	Students 
--where 
--	id < 7;

select * from newStudents;

insert into newStudents (id, name)
select id, firstName from Students where id > 8;
