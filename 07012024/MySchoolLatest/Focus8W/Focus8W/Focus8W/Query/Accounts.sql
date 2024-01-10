use school
--drop table tbl_Fees
CREATE TABLE tbl_Fees (
	iEnrollmentNo int Foreign key references tbl_AdmissionDetails,
	iStdId int FOREIGN KEY REFERENCES tbl_StudentClasswiseInfo,
	iClass int,
    iForMonth int,
	iYear int,
	iDate bigint,  --submission date
	iFee int,
	iPending int,
	iYearlyFee int, --it will come in Fee structure table
	iConcession int,
);

select *from tbl_MarkSheet
select *from tbl_AdmissionDetails
select *from tbl_StudentClasswiseInfo
select iEnrollmentNo from tbl_StudentClasswiseInfo
where iStdId=1005
insert into tbl_StudentClasswiseInfo (iEnrollmentNo,iCurrentClass,iCurrentYear,bIsActive,bResultPrepared)
values (1005,3,132581633,1,0)
select * from tbl_Fees
insert into tbl_Fees (iEnrollmentNo, iStdId, iClass, iForMonth, iYear, iDate, iFee, iPending, iYearlyFee, iConcession) 
values (2, 2, 4, 2, 2023, 0, 200, 0, 0, 0)
INSERT INTO tbl_Fees (iEnrollmentNo, iStdId, iClass, iForMonth, iYear, iDate, iFee, iPending, iYearlyFee, iConcession) 
values (4, 4, 4, 1, 2023, 132581633, 0, 0, 0, 0)

select a.iEnrollmentNo, b.iStdId, a.sStdName, b.iCurrentClass, dbo.GETDATEPART('y' ,b.iCurrentYear)[iYear], c.iForMonth,c.iFee
                                        from tbl_AdmissionDetails a
                                        join tbl_StudentClasswiseInfo b on a.iEnrollmentNo=b.iEnrollmentNo
                                        join tbl_Fees c on c.iStdId=b.istdId
                                        where a.iEnrollmentNo = 1
                                        or b.iStdId = 0

select a.iEnrollmentNo, b.iStdId, a.sStdName, b.iCurrentYear
from tbl_AdmissionDetails a
join tbl_StudentClasswiseInfo b on a.iEnrollmentNo=b.iEnrollmentNo
--join tbl_Fees c on c.iStdId=b.iStdId
where b.iCurrentClass=4
and dbo.GETDATEPART('y' ,b.iCurrentYear)=2023


select c.iFee,c.iMonth from tbl_Fees c 
where c.iStdId=1

select (b.iFee+b.iPending) FeeStatus , * from tbl_Student a
join tbl_Fees b on a.iStdId=b.iStdId
where (b.iFee+b.iPending) < 0 and b.iYear=2000 and b.iMonth=3

select * from tbl_Student a
join tbl_Fees b on a.iStdId=b.iStdId
where b.iPending < 0 and b.iYear=2000 and b.iMonth=2
group by a.sFirstName


SELECT b.iStdId, a.sStdName, b.iCurrentClass, b.iCurrentYear
                                        FROM tbl_AdmissionDetails a 
                                        JOIN tbl_StudentClasswiseInfo b ON a.iEnrollmentNo = b.iEnrollmentNo
                                        WHERE b.bIsActive = 1 AND b.bResultPrepared = 0
        		                        AND b.iCurrentClass = 4 
										AND dbo.GETDATEPART('Y',b.iCurrentYear) = 2024 or b.iCurrentYear = 2024
                                        ORDER BY 1