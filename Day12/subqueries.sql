SELECT TOP (1000) [Empno]
      ,[Name]
      ,[Gender]
      ,[Dept]
      ,[Desig]
      ,[Basic]
  FROM [wiprojuly].[dbo].[Employ]

  select max(basic) from Employ;

  select name from Employ where basic = (select max(basic) from Employ) 
  GO

  -- Dispaly second max. salary 

  select max(basic) from Employ where basic < 
  (select Max(basic) from Employ)

  -- Display Name of employ who is getting 2nd max. salary

  select Name from Employ where basic = (
    select max(basic) from Employ where basic < 
  (select Max(basic) from Employ))
  GO

  select * from Policy;


  select PolicyId, AppNumber, ModalPremium, AnnualPremium,
  ROW_NUMBER() OVER(Order By AnnualPremium desc) 'Rno'
  from Policy
  GO

  select PolicyId, AppNumber, ModalPremium, AnnualPremium,
  RANK() OVER(Order By AnnualPremium desc) 'Rno'
  from Policy
  GO

  select PolicyId, AppNumber, ModalPremium, AnnualPremium,
  DENSE_RANK() OVER(Order By AnnualPremium desc) 'Rno'
  from Policy
  GO

  select * from Policy
  GO

  select max(annualpremium) from Policy 
  GO

  -- Display PolicyID of max. annualpremium 

  select PolicyId from Policy WHERE AnnualPremium = 
  (select MAX(annualpremium) from  Policy)

  -- Display 2nd max AnnualPremium 

  select max(annualpremium) from Policy WHERE AnnualPremium < 
  (select max(annualpremium) from Policy)

  --

  -- Display matching records from Employ and LeaveHistory table 

select * from Employ where Empno = ANY(select Empno from LeaveHistory)
GO

-- Display matching records from Agent and AgentPolicy Tables 

select * from Agent WHERE AgentId = ANY(select AgentId from AgentPolicy) 

-- Display Matching records from Policy and AgentPolicy Tables 

select * from Policy WHERE PolicyId = ANY (select PolicyId from AgentPolicy) 
GO

-- Display Employ Details who are not taken Leave (Means records which are in Employ table, but not in
-- LeaveHistory Table)

select * from Employ WHERE Empno <> ALL(select Empno from LeaveHistory) 
GO

-- Display Idle Agents (AgentId Exists in Agent Table, but not in AgentPolicy Table) 

select * from Agent where AgentId <> ALL(select AgentId from AgentPolicy) 
GO

-- Display Idle Policies (PolicyId exists in Policy Table, but not in AgentPolicy Table) 

select * from Policy WHERE PolicyId <> ALL(select PolicyId from AgentPolicy) 
GO