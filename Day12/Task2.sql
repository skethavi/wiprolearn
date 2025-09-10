
--1
SELECT * 
FROM dbo.tblEmployees
WHERE LEN(Name) - LEN(REPLACE(Name, ' ', '')) = 0;
--2
SELECT * 
FROM dbo.tblEmployees
WHERE LEN(Name) - LEN(REPLACE(Name, ' ', '')) = 2;

--3
SELECT * 
FROM dbo.tblEmployees
WHERE 
    TRIM(Name) IN ('Ram') OR
    TRIM(SUBSTRING(Name, CHARINDEX(' ', Name) + 1, LEN(Name))) = 'Ram';
--4 bitwise operators
SELECT 
    65 | 11 AS OR_Result,
    65 ^ 11 AS XOR_Result,
    65 & 11 AS AND_Result,
    (12 & 4) | (13 & 1) AS Expr1,
    127 | 64 AS OR2,
    127 ^ 64 AS XOR2,
    127 ^ 128 AS XOR3,
    127 & 64 AS AND2,
    127 & 128 AS AND3;

-- 5
SELECT * FROM dbo.tblCentreMaster;
--6
select distinct emp.EmployeeType
from dbo.tblEmployees emp;
-- 7a
SELECT Name, FatherName, DOB 
FROM dbo.tblEmployees
WHERE PresentBasic > 3000;

-- 7b
SELECT Name, FatherName, DOB 
FROM dbo.tblEmployees
WHERE PresentBasic < 3000;

-- 7c
SELECT Name, FatherName, DOB 
FROM dbo.tblEmployees
WHERE PresentBasic BETWEEN 3000 AND 5000;
--8a
SELECT *
FROM tblEmployees
WHERE Name LIKE '%KHAN';
--8b
SELECT *
FROM tblEmployees
WHERE Name LIKE 'CHANDRA%';
--8c
SELECT *
FROM tblEmployees
WHERE Name LIKE '[A-T].RAMESH';

--excercise 2
--1
SELECT D.Description AS DepartmentName, SUM(E.PresentBasic) AS TotalBasic
FROM tblEmployees E
JOIN tblDesignations D ON E.DesignationCode = D.DesignationCode
GROUP BY D.Description
HAVING SUM(E.PresentBasic) > 30000
ORDER BY D.Description;

--2
SELECT 
    E.CentreCode,
    ST.Description AS ServiceTypeName,
    SS.Description AS StatusName,
    MAX(E.Age) AS MaxAge,
    MIN(E.Age) AS MinAge,
    AVG(CAST(E.Age AS FLOAT)) AS AvgAge
FROM tblEmployees E
JOIN tblServiceTypes ST ON E.ServiceType = ST.ServiceType
JOIN tblServiceStatus SS ON E.ServiceStatus = SS.ServiceStatus
GROUP BY E.CentreCode, ST.Description, SS.Description;

--3
SELECT 
    E.CentreCode,
    ST.Description AS ServiceTypeName,
    SS.Description AS StatusName,
    MAX(DATEDIFF(YEAR, E.DOJ, GETDATE())) AS MaxServiceYears,
    MIN(DATEDIFF(YEAR, E.DOJ, GETDATE())) AS MinServiceYears,
    AVG(DATEDIFF(MONTH, E.DOJ, GETDATE()) / 12.0) AS AvgServiceYears
FROM tblEmployees E
JOIN tblServiceTypes ST ON E.ServiceType = ST.ServiceType
JOIN tblServiceStatus SS ON E.ServiceStatus = SS.ServiceStatus
GROUP BY E.CentreCode, ST.Description, SS.Description;

--4
SELECT D.Description AS Department
FROM tblPayEmployees P
JOIN tblDesignations D ON P.DesignationCode = D.DesignationCode
GROUP BY D.Description
HAVING SUM(P.GrossPay) > 3 * AVG(P.GrossPay);

--5
SELECT D.Description AS Department
FROM tblPayEmployees P
JOIN tblDesignations D ON P.DesignationCode = D.DesignationCode
GROUP BY D.Description
HAVING 
    SUM(P.GrossPay) > 2 * AVG(P.GrossPay)
    AND MAX(P.GrossPay) >= 3 * MIN(P.GrossPay);

--6
SELECT CentreCode
FROM tblEmployees
GROUP BY CentreCode
HAVING MAX(LEN(LTRIM(RTRIM(Name)))) >= 2 * MIN(LEN(LTRIM(RTRIM(Name))));

--7
SELECT 
    E.CentreCode,
    ST.Description AS ServiceType,
    SS.Description AS ServiceStatus,
    MAX(DATEDIFF_BIG(MILLISECOND, E.DOJ, GETDATE())) AS MaxServiceMS,
    MIN(DATEDIFF_BIG(MILLISECOND, E.DOJ, GETDATE())) AS MinServiceMS,
    AVG(1.0 * DATEDIFF_BIG(MILLISECOND, E.DOJ, GETDATE())) AS AvgServiceMS
FROM tblEmployees E
JOIN tblServiceTypes ST ON E.ServiceType = ST.ServiceType
JOIN tblServiceStatus SS ON E.ServiceStatus = SS.ServiceStatus
GROUP BY E.CentreCode, ST.Description, SS.Description;

--8
SELECT *
FROM tblEmployees
WHERE Name <> LTRIM(RTRIM(Name));

--9
SELECT *
FROM tblEmployees
WHERE Name LIKE '%  %'; -- double spaces

--10
SELECT 
    EmployeeNumber,
    Name AS OriginalName,
    REPLACE(
        LTRIM(RTRIM(REPLACE(REPLACE(Name, '.', ''), '  ', ' '))),
        '  ', ' '
    ) AS CleanedName
FROM tblEmployees;

--11
SELECT MAX(LEN(LTRIM(RTRIM(Name))) - LEN(REPLACE(LTRIM(RTRIM(Name)), ' ', '')) + 1) AS MaxWords
FROM tblEmployees;

--12
SELECT *
FROM tblEmployees
WHERE LEFT(LTRIM(Name), 1) = RIGHT(RTRIM(Name), 1);

--13
SELECT *
FROM tblEmployees
WHERE LEN(Name) - LEN(REPLACE(Name, ' ', '')) >= 1
  AND LEFT(Name, 1) = SUBSTRING(Name, CHARINDEX(' ', Name) + 1, 1);

--14
SELECT *
FROM tblEmployees
WHERE NOT EXISTS (
    SELECT 1
    FROM STRING_SPLIT(REPLACE(Name, '.', ''), ' ') AS parts
    WHERE LEFT(parts.value, 1) COLLATE Latin1_General_CI_AI 
          <> LEFT(Name, 1) COLLATE Latin1_General_CI_AI
);

--15
SELECT *
FROM tblEmployees
WHERE EXISTS (
    SELECT 1
    FROM STRING_SPLIT(REPLACE(Name, '.', ''), ' ') AS parts
    WHERE LEN(parts.value) > 1 AND LEFT(parts.value, 1) = RIGHT(parts.value, 1)
);

--16
SELECT * FROM tblPayEmployees
WHERE ROUND(GrossPay, -2) = GrossPay;
SELECT * FROM tblPayEmployees
WHERE FLOOR(GrossPay / 100.0) * 100 = GrossPay;
SELECT * FROM tblPayEmployees
WHERE GrossPay % 100 = 0;
SELECT * FROM tblPayEmployees
WHERE CEILING(GrossPay / 100.0) * 100 = GrossPay;

--17
SELECT DesignationCode
FROM tblPayEmployees
GROUP BY DesignationCode
HAVING COUNT(*) = SUM(CASE WHEN GrossPay % 100 = 0 THEN 1 ELSE 0 END);

--18
SELECT DesignationCode
FROM tblPayEmployees
GROUP BY DesignationCode
HAVING SUM(CASE WHEN GrossPay % 100 = 0 THEN 1 ELSE 0 END) = 0;

--19
SELECT 
    EmployeeNumber,
    DOJ,
    DOB,
    DATEADD(DAY, 15, DATEADD(MONTH, 3, DATEADD(YEAR, 1, DOJ))) AS EligibilityDate,
    DATEADD(MONTH, 1, DATEADD(DAY, 15, DATEADD(MONTH, 3, DATEADD(YEAR, 1, DOJ)))) AS BonusDate,
    DATEDIFF(YEAR, DOB, DATEADD(MONTH, 1, DATEADD(DAY, 15, DATEADD(MONTH, 3, DATEADD(YEAR, 1, DOJ))))) AS AgeOnBonusDate,
    DATENAME(WEEKDAY, DATEADD(MONTH, 1, DATEADD(DAY, 15, DATEADD(MONTH, 3, DATEADD(YEAR, 1, DOJ))))) AS WeekDay,
    DATEPART(WEEK, DATEADD(MONTH, 1, DATEADD(DAY, 15, DATEADD(MONTH, 3, DATEADD(YEAR, 1, DOJ))))) AS WeekOfYear,
    DATEPART(DAYOFYEAR, DATEADD(MONTH, 1, DATEADD(DAY, 15, DATEADD(MONTH, 3, DATEADD(YEAR, 1, DOJ))))) AS DayOfYear
FROM tblEmployees;

--20
SELECT emp.DepartmentCode,COUNT(emp.PresentBasic),
     COUNT(CASE
        WHEN ROUND(emp.PresentBasic,-2)=emp.PresentBasic AND emp.PresentBasic<>0 THEN emp.PresentBasic
     END)
FROM dbo.tblEmployees emp
GROUP BY emp.DepartmentCode
HAVING COUNT(emp.PresentBasic)=COUNT(CASE
        WHEN ROUND(emp.PresentBasic,-2)=emp.PresentBasic AND emp.PresentBasic<>0 THEN emp.PresentBasic
     END)
--21
SELECT 
    CONVERT(VARCHAR, GETDATE(), 1) AS [MM/DD/YY],
    CONVERT(VARCHAR, GETDATE(), 3) AS [DD/MM/YY],
    CONVERT(VARCHAR, GETDATE(), 105) AS [DD-MM-YYYY],
    CONVERT(VARCHAR, GETDATE(), 113) AS [DD Mon YYYY HH:MI:SS:MMM],
    CONVERT(VARCHAR, GETDATE(), 120) AS [YYYY-MM-DD HH:MI:SS];

--22
SELECT 
    D.EmployeeNumber,
    D.ParamCode,
    D.ActualAmount AS ExpectedBasic,
    P.NetPay,
    D.EffectiveFrom,
    D.NoteNumber
FROM tblPayEmployeeparamDetails D
JOIN tblPayEmployees P 
    ON D.EmployeeNumber = P.EmployeeNumber
WHERE 
    D.ParamCode = 'BASIC'
    AND P.NetPay < D.ActualAmount;
