if exists(select * from sysobjects where name='prcEmployOut') 
Drop Proc prcEmployOut
GO

Create proc prcEmployOut
				@empno INT,
				@name varchar(30) OUTPUT,
				@gender varchar(30) OUTPUT,
				@dept varchar(30) OUTPUT,
				@desig varchar(30) OUTPUT,
				@basic numeric(9,2) OUTPUT
AS
BEGIN
	if exists(select * from Employ where empno = @empno) 
	BEGIN
		select @name=name, @gender = Gender, @dept = Dept,
				@desig = Desig, @basic = Basic 
		from Employ WHERE Empno = @empno
		RETURN 1
	END
	RETURN 0
END

if exists(select * from sysobjects where name='prcEmployOutExec') 
Drop Proc prcEmployOutExec
GO

Create Proc prcEmployOutExec
					@empno INT
AS
BEGIN
	declare 
		@ret INT,
		@name varchar(30),
		@gender varchar(10),
		@dept varchar(30),
		@desig varchar(30),
		@basic numeric(9,2)
		Exec @ret = prcEmployOut @empno, @name OUTPUT, @Gender OUTPUT, @Dept OUTPUT, @Desig OUTPUT,
				@BAsic OUTPUT 
		if @ret = 1
		BEGIN
			print 'Name ' +@name 
			Print 'Gender ' +@gender
			Print 'Department ' +@dept
			Print 'Designation ' +@desig 
			Print 'Basic ' +convert(varchar,@basic)
		END
		ELSE 
		BEGIN
			Print 'Employ Record Not Found...'
		END
END
GO
Exec prcEmployOutExec 1 
GO

--prcoutexamplenew

if exists(select * from sysobjects where name='prcEmployAutoGen') 
Drop Proc prcEmployAutoGen
GO

create  procedure prcEmployAutoGen
			@empno INT OUTPUT
AS
BEGIN
	select @empno = 
		case when max(empno) IS NULL THEN 1 else max(empno)+1 END from Employ 

END
GO

if exists(select * from sysobjects where name='PrcEmployInsert') 
Drop Proc PrcEmployInsert
GO

Create proc PrcEmployInsert
			@name varchar(30),
			@gender varchar(10),
			@dept varchar(30),
			@desig varchar(30),
			@basic numeric(9,2)
AS
BEGIN
	Declare
		@empno INT 
	BEGIN
		Exec prcEmployAutoGen @empno OUTPUT 
		Insert into Employ(Empno,Name,Gender,Dept,Desig,Basic) values(@empno,@name,@gender,
				@dept,@desig,@basic)
		Print 'Employ Record Inserted...'
	END
END
GO

Exec PrcEmployInsert 'Vasim','MALE','Dotnet','Manager',88233 
GO

select * from Employ 
GO