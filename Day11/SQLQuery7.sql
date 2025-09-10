---Display Last Occurrence of given char in a string

---Ex: 'Keerthi' char 'e' exists 2 times last time at position 3

DECLARE @str NVARCHAR(100) ='Keerthi';
DECLARE @char CHAR(1) = 'e';
SELECT
	LEN (@str) -CHARINDEX(@char, REVERSE (@str)) + 1 AS LastOccurPosition, 
	LEN(@str) -LEN(REPLACE(@str, @char,'')) AS Occurrences;

---Take FullName as 'Venkata Narayana and split them into firstName and LastName

DECLARE @FullName NVARCHAR (100)='Venkata Narayana';

SELECT 
	LEFT(@FullName, CHARINDEX(' ',@FullName) - 1) AS FirstName,
    RIGHT(@FullName, LEN(@FullName) - CHARINDEX(' ',@FullName)) AS LastName;

--In Word 'misissipi' count no.of 'i'

DECLARE @word NVARCHAR(100) ='misissipi';

SELECT 
	LEN (@word)-LEN(REPLACE(@word, 'i','')) AS CountOfI;

---Display the last day of next month

SELECT EOMONTH(DATEADD(MONTH, 1, GETDATE())) AS LastDayOfNextMonth;

--Display First Day of Previous Month

SELECT DATEFROMPARTS(
	YEAR (DATEADD(MONTH,-1, GETDATE())),
	MONTH (DATEADD(MONTH,-1, GETDATE())),1) AS FirstDayOfPreviousMonth;

--Display all Fridays of current month!!
WITH Dates AS (
    SELECT CAST(DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1) AS DATE) AS DateValue
    UNION ALL
    SELECT DATEADD(DAY, 1, DateValue)
    FROM Dates
    WHERE MONTH(DateValue) = MONTH(GETDATE())
)
SELECT DateValue
FROM Dates
WHERE DATENAME(WEEKDAY, DateValue) = 'Friday'
OPTION (MAXRECURSION 100);