use school
--Admin
--==============================
--drop table tbl_AdmissionDetails
CREATE TABLE tbl_Teachers(
	iTeacherId int IDENTITY(1,1) PRIMARY KEY,    
	bIntMobile bigint,
	sEmail nvarchar(100),
	sPassword nvarchar(200),
    sName nvarchar(255),
    sQualification nvarchar(255),
	iJoiningDate bigint,
    iPackage bigint,
    iMonthlySalary int,
	iExperiance int,
	bActive bit
);
select *from tbl_Teachers
INSERT INTO tbl_Teachers(bIntMobile, sEmail, sPassword, sName,sQualification,iJoiningDate,iPackage,iMonthlySalary,iExperiance,bActive) 
VALUES (8686360214,'ansari.sheebu786@gmail.com','123','Anas','MCA',2020,20000,20000,4,0)

SELECT iTeacherId, ISNULL(bIntMobile,0)bIntMobile, ISNULL(sEmail, 0) sEmail,ISNULL(sPassword,0)sPassword, sName, sQualification,iJoiningDate,iPackage,iMonthlySalary,iExperiance,ISNULL(bActive,0)[bActive] from tbl_Teachers
SELECT iTeacherId, ISNULL(bIntMobile,0)bIntMobile, ISNULL(sEmail, 0) sEmail,ISNULL(sPassword,0)sPassword, sName, sQualification, iJoiningDate,iPackage,iMonthlySalary,iExperiance,ISNULL(bActive,0)[bActive] from tbl_Teachers
where bActive<>1

--drop table tbl_TeacherAttandence
CREATE TABLE tbl_TeacherAttandence (
	iTeacherId int FOREIGN KEY REFERENCES tbl_Teachers,
	iDate bigint,
	iStatus int --0: Absent 1: Present 3: onLeave
);
select *from tbl_TeacherAttandence
select dbo.getdatepart('m',132581633)

select a.iTeacherId, c.sName, a.iDate, a.iStatus
from tbl_TeacherAttandence a
join tbl_Teachers c on c.iTeacherId=c.iTeacherId
where dbo.getdatepart('m',a.iDate) = 9
and dbo.getdatepart('y',a.iDate) = 2023
and c.bActive=1

select *from tbl_TeacherAttandence