-- String Functions

/* charindex() : Used to display the first occurence of the given character  */

select charindex('l','hello')
go
select nam ,charindex('a', nam) from emp
go
/* Reverse() : Used to display string in reverse order */

select Reverse('kethai')
go
select Nam,Reverse(Nam) from Emp 
GO

/* Left() : Displays no.of left-side chars */

select left('kethavi',4) 
GO

/* Right() : Displays no.of right-side chars */ 

select right('kethavi',4) 
GO

/* Upper() : Dispalys string in Upper Case */ 

select nam, upper(nam) from Emp
GO

/* Lower() : Displays string in Lower Case */ 

select nam, Lower(nam) from Emp 
GO

/* Substring() : Used to display part of the string */ 

select SUBSTRING('welcome to sql',5,5) 
GO

/* Replace() : used to replace old value/string with new value/string in all occurrences */ 

SELECT REPLACE('Dotnet Training','Dotnet','Java') 
GO

SELECT dept,REPLACE(dept,'dotnet','Java train') from  emp
GO