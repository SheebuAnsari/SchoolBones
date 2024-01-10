--Teacher
use school
--========================================================================================
--drop table Student
--drop table tbl_ProgressReport 
--========================================================================================
--AdmissionDetails
--drop table tbl_AdmissionDetails
CREATE TABLE tbl_AdmissionDetails(
	iEnrollmentNo int IDENTITY(1,1) PRIMARY KEY,    
    sStdName nvarchar(255),
    sFirstName nvarchar(255),
	sLastName nVarchar(255),
    sAddress nvarchar(255),
    sCity nvarchar(255),
	iContact bigint,
	iAdmissionInClass int,
	iAdmissionDate bigint,
	iDOB bigint,
	bIsActive bit
);
--drop table tbl_StudentClasswiseInfo
CREATE TABLE tbl_StudentClasswiseInfo(
	iStdId int IDENTITY(1,1) PRIMARY KEY,   
	iEnrollmentNo int Foreign key references tbl_AdmissionDetails,
	iCurrentClass int, 
	iCurrentYear int, 
	bIsActive bit, 
	bResultPrepared bit
);

--drop table tbl_StudentAttendance
CREATE TABLE tbl_StudentAttendance (
	iStdId int FOREIGN KEY REFERENCES tbl_StudentClasswiseInfo,
	iEnrollmentNo int Foreign key references tbl_AdmissionDetails,
	iClass int,
	iDate bigint,
	iStatus int --0: Absent 1: Present 3: onLeave
);
--========================================================================================
--drop table tbl_MarkSheet
CREATE TABLE tbl_MarkSheet (
	--iEnrollmentNo int Foreign key references tbl_AdmissionDetails,
	iStdId int FOREIGN KEY REFERENCES tbl_StudentClasswiseInfo,
    iClass int,
	iYear int,
	iPhysics int,
	iChemistry int,
	iMathmatics int,
	bResultPrepared bit
);

--drop table tbl_MarkSheet
select *from tbl_MarkSheet
--drop table tbl_MarkSheet
CREATE TABLE tbl_MarkSheet (
	iMarkSheetId int Primary key identity(1,1),
	iStdId int FOREIGN KEY REFERENCES tbl_StudentClasswiseInfo,
	iClass int,
	iYear int,
	--Physics int,
	--Chemistry int,
	--Mathematics int,
	bResultPrepared bit,
);

--drop table tbl_MarksOfSubjects
CREATE TABLE tbl_MarksOfSubjects (
--iMarksOfCustomizeSubjectId int Primary key identity(1,1),
iMarkSheetId int FOREIGN KEY REFERENCES tbl_MarkSheet
);
select *from tbl_MarksOfSubjects 

--drop table tbl_Subjects
select *from tbl_Subjects
CREATE TABLE tbl_Subjects (
	iSubjectId int Primary key identity(1,1),   --as Class
	iClass int,
    sSubjectName nvarchar(100) unique,
	bIsDefaultSubject bit
);
insert into tbl_MarkSheet (iStdId,iYear,sClass,iObtainMark,sDivision,iPhysics,iChemistry,iMathmatics) values (1,2000,'4th',425,'1', 48,48,41)
insert into tbl_MarkSheet (iStdId,iYear,sClass,iObtainMark,sDivision,iPhysics,iChemistry,iMathmatics) values (1,2001,'5th',425,'1', 48,48,41)
insert into tbl_MarkSheet (iStdId,iYear,sClass,iObtainMark,sDivision,iPhysics,iChemistry,iMathmatics) values (1,2002,'6th',425,'1', 48,48,41)

select * from tbl_MarkSheet
--========================================================================================


















select m.iMarkSheetId, m.iStdId, a.sStdName, m.iClass,m.iYear from tbl_MarkSheet m
                                        join tbl_StudentClasswiseInfo i on i.iStdId=m.iStdId
                                        JOIN tbl_AdmissionDetails a ON a.iEnrollmentNo = i.iEnrollmentNo
                                        where m.iClass = 4 and m.iYear = 0

SELECT English,Math FROM tbl_MarksOfSubjects WHERE iMarkSheetId = 2

select m.iMarkSheetId, m.iStdId, a.sStdName, m.iClass,m.iYear from tbl_MarkSheet m
                                        join tbl_StudentClasswiseInfo i on i.iStdId=m.iStdId
                                        JOIN tbl_AdmissionDetails a ON a.iEnrollmentNo = i.iEnrollmentNo
                                        where m.iClass = 4 and m.iYear = 2023

select *from tbl_AdmissionDetails
SELECT b.iStdId, a.sStdName, b.iCurrentClass
FROM tbl_AdmissionDetails a
JOIN tbl_StudentClasswiseInfo b ON a.iEnrollmentNo = b.iEnrollmentNo
WHERE b.bIsActive = 1 and b.iCurrentClass =4
ORDER BY 1

--drop table tbl_MarkSheet
select *from tbl_MarkSheet
CREATE TABLE tbl_MarkSheet (
iMarkSheetId int Primary key identity(1,1),
iStdId int FOREIGN KEY REFERENCES tbl_StudentClasswiseInfo,
iClass int,
iYear int,
--Physics int,
--Chemistry int,
--Mathematics int,
);

--drop table tbl_MarksOfSubjects
CREATE TABLE tbl_MarksOfSubjects (
--iMarksOfCustomizeSubjectId int Primary key identity(1,1),
iMarkSheetId int FOREIGN KEY REFERENCES tbl_MarkSheet,
);
select *from tbl_MarksOfSubjects


select *from tbl_MarkSheet
select *from tbl_MarksOfSubjects
select *from tbl_Subjects
select *from tbl_MarksOfSubjects
--alter table tbl_MarksOfSubjects add Hindi int
--alter table tbl_MarkSheet add bResultPrepared bit

insert into tbl_MarkSheet (iStdId,iClass,iYear) values (2,4,2023)
insert into tbl_MarksOfSubjects (iMarkSheetId, Hindi) values (1,50)

select name [ColumnName] from sys.columns where object_id in (select object_id from sys.tables where name ='tbl_MarksOfSubjects')

select m.iStdId,m.iClass,m.iYear,s.Hindi from tbl_MarkSheet m
join tbl_MarksOfSubjects s on m.iMarkSheetId = s.iMarkSheetId
where m.iStdId=2

select *from tbl_StudentClasswiseInfo



select * from tbl_Subjects
select * from tbl_MarkSheet
insert into tbl_MarkSheet(iStdId,iClass,iYear,Physics,Chemistry,Mathematics) values (5,4,2023,25,26,27)

select iSubjectId , iClass, sSubjectName, bIsDefaultSubject from tbl_Subjects
where iClass=4
update tbl_Subjects set sSubjectName=''
where iSubjectId=4
delete from tbl_Subjects
Insert into tbl_Subjects (iClass, sSubjectName) values (4, 'sdsds')

--drop table tbl_MarkSheet
CREATE TABLE tbl_MarkSheet (
	iMarkSheetId int Primary key identity(1,1),
	iStdId int FOREIGN KEY REFERENCES tbl_StudentClasswiseInfo,
	iClass int,
	iYear int,
	--Physics int,
	--Chemistry int,
	--Mathematics int,
	bResultPrepared bit,
);

--drop table tbl_MarksOfSubjects
CREATE TABLE tbl_MarksOfSubjects (
--iMarksOfCustomizeSubjectId int Primary key identity(1,1),
iMarkSheetId int FOREIGN KEY REFERENCES tbl_MarkSheet
);

select * from tbl_MarkSheet
select * from tbl_Subjects
select *from tbl_MarksOfSubjects

delete from tbl_Subjects
delete from tbl_MarkSheet
alter table tbl_MarksOfCustomizeSubject add [P.T.] int
alter table tbl_MarksOfCustomizeSubject drop column Arabi_1

INSERT INTO tbl_MarkSheet (iStdId, iClass, iYear, bResultPrepared)
VALUES (1, 4, 0, 1)  Select @@IDENTITY [id]

select *from sys.tables
select name [ColumnName] from sys.columns where object_id in (select object_id from sys.tables where name ='tbl_MarksOfSubjects')  
select name [sSubjectColumnName] from sys.columns 
where object_id in (select object_id from sys.tables where name ='tbl_MarksOfSubjects')
and name <> 'iMarkSheetId'

--drop table tbl_Subjects
select *from tbl_Subjects
CREATE TABLE tbl_Subjects (
	iSubjectId int Primary key identity(1,1),   --as Class
	iClass int,
    sSubjectName nvarchar(100) unique,
	bIsDefaultSubject bit
);
select iSubjectId , iClass, sSubjectName, ISNULL(bIsDefaultSubject, 0)[bIsDefaultSubject] from tbl_Subjects Where iClass = 4


select * from tbl_MarkSheet
select * from tbl_Subjects
select *from tbl_MarksOfSubjects
select *from  tbl_AdmissionDetails


SELECT  b.iStdId, a.sStdName, b.iCurrentClass, b.iCurrentYear
FROM tbl_AdmissionDetails a
JOIN tbl_StudentClasswiseInfo b ON a.iEnrollmentNo = b.iEnrollmentNo
WHERE b.bIsActive = 1 AND b.bResultPrepared = 1
AND b.iCurrentClass = 4 AND dbo.GETDATEPART('Y',b.iCurrentYear) = 2023
ORDER BY 1
                                     
select m.iStdId, a.sStdName, m.iClass,m.iYear,s.English,s.Math from tbl_MarkSheet m
join tbl_StudentClasswiseInfo i on i.iStdId=m.iStdId
JOIN tbl_AdmissionDetails a ON a.iEnrollmentNo = i.iEnrollmentNo
join tbl_MarksOfSubjects s on m.iMarkSheetId = s.iMarkSheetId
where m.iClass=4 and m.iYear=0 and i.bResultPrepared = 1

   
select m.iMarkSheetId, m.iStdId, a.sStdName, m.iClass,m.iYear from tbl_MarkSheet m
join tbl_StudentClasswiseInfo i on i.iStdId=m.iStdId
JOIN tbl_AdmissionDetails a ON a.iEnrollmentNo = i.iEnrollmentNo
where m.iClass=4 and m.iYear=0

select *from tbl_StudentClasswiseInfo 
select *from tbl_MarksOfSubjects where iMarkSheetId = 4