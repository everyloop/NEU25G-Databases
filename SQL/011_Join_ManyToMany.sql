
drop table Students;
drop table Courses;

create table Students
(
	Id int,
	Name nvarchar(max)
)

insert into Students values(1, 'Anders');
insert into Students values(2, 'Johan');
insert into Students values(3, 'Emma');
insert into Students values(4, 'Johanna');
insert into Students values(5, 'Erik');
insert into Students values(6, 'Stefan');
insert into Students values(7, 'Maria');

create table Courses
(
	Id int,
	Name nvarchar(max)
);

insert into Courses values(1, 'C#');
insert into Courses values(2, 'Databaser');
insert into Courses values(3, 'Webutveckling');
insert into Courses values(4, 'Javscript');
insert into Courses values(5, 'Java');
insert into Courses values(6, 'SQL');

create table StudentsCourses
(
	StudentId int,
	CourseId int,
)

insert into StudentsCourses values(1, 1);
insert into StudentsCourses values(1, 3);
insert into StudentsCourses values(1, 5);
insert into StudentsCourses values(2, 1);
insert into StudentsCourses values(3, 2);
insert into StudentsCourses values(3, 3);
insert into StudentsCourses values(4, 1);
insert into StudentsCourses values(4, 2);
insert into StudentsCourses values(4, 3);
insert into StudentsCourses values(4, 4);
insert into StudentsCourses values(5, 2);
insert into StudentsCourses values(5, 4);
insert into StudentsCourses values(6, 4);
insert into StudentsCourses values(6, 5);


select * from Students;
select * from Courses;
select * from StudentsCourses

