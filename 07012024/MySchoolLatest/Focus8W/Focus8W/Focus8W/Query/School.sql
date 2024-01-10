--create database School
--use School
--========================================================================================
--drop table tbl_Login
CREATE TABLE tbl_Login (
	iLoginId int IDENTITY(1,1) PRIMARY KEY,    
	sLoginId nvarchar (200) Unique,
	sPassword nvarchar (200),
	iType int,    
);

insert into tbl_Login (sLoginId, sPassword, iType) values ('su','su',1)
select *from tbl_Login where itype=1 and sloginid='su'

CREATE TABLE tbl_SchoolDetails (
	iSchoolId int IDENTITY(1,1) PRIMARY KEY,    
	--sPrincipal varchar(255),
    --sEstablish varchar(255),
	iRegistrationDate bigint,
    --sAddress varchar(255),
	iContact bigint,
);

insert into tbl_SchoolDetails (sRegistration, iContact) values (2020,86360214)
select *from tbl_SchoolDetails

--========================================================================================
--drop table tbl_TeachersFinance
CREATE TABLE tbl_TeachersFinance(
	iTeacherFinanceId int Primary key identity(1,1),
	iTeacherId int Foreign key references tbl_teachers,
	iYear int ,
	iMonth int, 
	iCrSalary int,
);

insert into tbl_TeachersFinance values (1, 2000,1,20000)
insert into tbl_TeachersFinance values (1, 2000,2,20000)
insert into tbl_TeachersFinance values (1, 2000,3,20000)

insert into tbl_TeachersFinance values (2, 2000,1,15000)
insert into tbl_TeachersFinance values (2, 2000,2,15000)
insert into tbl_TeachersFinance values (2, 2000,3,15000)

insert into tbl_TeachersFinance values (3, 2001,1,15000)
insert into tbl_TeachersFinance values (3, 2001,2,15000)
insert into tbl_TeachersFinance values (3, 2001,3,15000)
insert into tbl_TeachersFinance values (3, 2001,4,15000)


select *from tbl_Teachers 
select *from tbl_TeachersFinance 
SELECT iSchoolId, sRegistration, iContact from tbl_SchoolDetails

--========================================================================================
--========================================================================================
--========================================================================================
--========================================================================================
--========================================================================================
--========================================================================================
