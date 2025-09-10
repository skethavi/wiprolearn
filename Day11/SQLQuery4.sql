-- Date Functions 

-- GetDate() : used to display today's date 

select GETDATE() 
GO

select convert(varchar,GETDATE(),1) 
Go

select convert(varchar,GETDATE(),2) 
Go

select convert(varchar,GETDATE(),101) 
Go

select convert(varchar,GETDATE(),103) 
Go

/* DatePart() : used to display the specific portion of the given date */

select datepart(dd,getdate())
select datepart(mm,getdate())
select datepart(yy,getdate())
select datepart(hh,getdate())
select datepart(mi,getdate())
select datepart(ss,getdate())
select datepart(ms,getdate())
select datepart(dw,getdate())
select datepart(qq,getdate())

-- DateName() : Displays date part in engligh words 

select datename(dw,getdate());


select convert(varchar,DATEPART(dd,getdate())) + '/' + 
convert(varchar,datepart(mm,getdate())) + '/' + 
convert(varchar,DATEPART(yy,getdate()))
GO

select DATENAME(mm, getdate())
GO

/* DateAdd() : Used to add no.of days or months or years to the particular date */

select DATEADD(dd,3,getdate())

select dateadd(mm,3,getdate())

select DATEADD(yy,3,getdate())

/* DateDiff() : used to Display difference between Two Dates */ 

select DATEDIFF(dd,'03/09/1980',getdate())
select DATEDIFF(yy,'03/09/1980',getdate())



-- Aggregate Functions 

-- sum() : used to perform sum operation 

select sum(basic) from Emp 
GO

-- Avg() : Displays avg operation 

select avg(basic) from Emp 
GO

-- Max() : Display max value 

select max(basic) from Emp 
GO

-- Min() : Displays Min. Value

select min(basic) from Emp 
GO

-- count(*) : Displays total no.of records 

select count(*) from Emp 
GO

-- count(column_name) : displays count for that column not null values 

select count(basic) from Emp
Go
 
 -- Group By : Display summary result w.r.t field specified 

select dept, sum(basic) from Emp 
group by dept;

select gender,count(*)
from Agent  group by gender;

select dept, avg(basic) from Emp 
Group by Dept; 

select dept, sum(basic) 'Total',avg(basic) 'Average',
max(basic) 'Max Basic', min(basic) 'Min Basic', count(*) 'Total Records'
from Emp 
group by Dept; 

-- Having clause : Used to display aggregate conditions 

select dept, sum(basic) 'Total',avg(basic) 'Average',
max(basic) 'Max Basic', min(basic) 'Min Basic', count(*) 'Total Records'
from Emp 
group by Dept having count(*) > 1; 

select dept, sum(basic) from Emp 
group by dept 
having sum(basic) > 50000;