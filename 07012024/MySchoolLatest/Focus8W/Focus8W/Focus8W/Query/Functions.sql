use school
create Function [dbo].[IntToDate] 
( 
 @iDate int 
) 
returns Datetime 
Begin 
 Declare @Date Datetime 
 if @iDate <> 0 
 Begin 
 Set @Date = Cast((CAST((@iDate & 0xfff0000)/65536 as nvarchar(4)) + '/' + Cast((@iDate & 0xff00)/256 as
nvarchar(2)) + '/' + Cast ((@iDate & 0xff) as nvarchar(2))) as DATETIME) 
 End 
 else 
 Begin 
 Set @Date = GetDate() 
 End 
 return @Date 
End

select dbo.IntToDate(132581377)

CREATE function dbo.GetDatePart(@p varchar(4), @iDate int) returns int 
 begin 
set @p = substring(LOWER(@p), 1, 1) 
if @p = 'd' 
begin 
 return @iDate & 0xff 
end 
if @p = 'm' 
begin 
 return (@iDate & 0xff00)/256 
end 
if @p = 'y' 
begin 
 return (@iDate & 0xfff0000)/65535 
end 
if @p = 'q' 
begin 
 if (@iDate & 0xff00)/256 between 1 and 3 
 begin 
return 1 
 end 
 if (@iDate & 0xff00)/256 between 4 and 6 
 begin 
return 2 
 end 
 if (@iDate & 0xff00)/256 between 7 and 9 
 begin 
return 3 
 end 
 if (@iDate & 0xff00)/256 between 10 and 12 
 begin 
return 4 
 end 
end 
return 0 
 end

SELECT iStdId,iClass, iDate, iStatus FROM tbl_StudentAttandence where dbo.GETDATEPART('M',iDate) = 10 and
dbo.GETDATEPART('Y',132581899) = 2023
--select dbo.inttodate(132581899)
--SELECT dbo.GETDATEPART('d',132581899)
--SELECT dbo.GETDATEPART('M',132581899)
--SELECT dbo.GETDATEPART('Y',132581899)