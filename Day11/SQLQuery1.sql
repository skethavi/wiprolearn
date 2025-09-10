use sqlpractice 
Go
--Display List of tables avaialble in current database 
select * from INFORMATION_SCHEMA.TABLES
GO
-- Display information about Emp Table 
sp_help Emp
GO
--Dispaly all records from Emp table 
select * from Emp
Go
--Display Empno, Name, Basic from Emp table 
select Empno, nam, basic from Emp
Go
select Empno 'Employ No', nam 'Employ Name', basic 'Salary' from Emp 
GO
-- Display All records whose Basic > 50000 
select * from Emp
where basic > 50000
Go
--select * from Emp
--where salary > 50000
--Go
-- Display All records whose Dept is 'DOTNET'
select * from Emp 
where Dept ='Dotnet'
Go
-- Display all records whose name is 'Swetha'
select * from Emp 
where nam ='Swetha'
Go

-- Between...AND : Display records from specific Range (works for numbers and date only) 

select * from Emp
Where basic between 50000 and 90000
Go
select * from EMP 
WHERE Basic NOT Between 50000 and 90000
GO

-- IN Clause : Used to check for multiple values of particular column 

-- Display all records whose Dept is Java or Dotnet or Sql 

select *from Emp
where Dept in('java', 'sql', 'Dotnet')
Go
select *from Emp
where Dept not in('java', 'sql', 'Dotnet')
Go
select * from Emp 
where nam IN('Manu','Satish','Swapna','Smitha','Rushi')
GO
-- LIKE operator : Used to display data w.r.t. wild cards

-- Display all records whose name starts with 'S'

select * from Emp where nam LIKE 's%'
Go
-- Display all records whose name ends with 'A' 
select * from Emp where nam LIKE '%A'
GO

-- Distinct : Eliminate duplicate entries at the time of display 

select Dept from Emp
GO
select distinct Dept from Emp 
GO

-- Order By : Used to display values w.r.t. Particular field(s) in ascending or descending order 

select nam from Emp  
GO
select * from Emp order by nam 
GO
select * from Emp order by Basic DESC
GO
select * from Emp order by Dept, Nam 
GO
select * from Emp order by  Nam, DEpt 
GO
select * from Emp order by Dept, Nam DESC
GO