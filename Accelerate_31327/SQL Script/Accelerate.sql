-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sapna Goyal
-- Create date: 13-Oct-2016
-- Description:	This store procedure use for get employee list.
-- =============================================
IF (OBJECT_ID('GetEmployeeList') IS NOT NULL)
  DROP PROCEDURE GetEmployeeList
GO
CREATE PROCEDURE GetEmployeeList 
	-- Add the parameters for the stored procedure here
	@SearchText VARCHAR(200)=''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   CREATE TABLE #EmployeeDetails(
	[Emp_ID] [int] NOT NULL,
	[Emp_FirstName] [varchar](255) NOT NULL,
	[Emp_LastName] [varchar](255) NOT NULL,
	[Emp_Email] [nvarchar](255) NOT NULL,
	[Emp_Designation] [varchar](255) NOT NULL,
	[Technologies] [varchar](255) NOT NULL,
    )
    -- Insert statements for procedure here
	     INSERT INTO #EmployeeDetails SELECT Emp_ID,Emp_FirstName,Emp_LastName,Emp_Email,Emp_Designation,STUFF((SELECT distinct ',' + tech.Technology_Name
         FROM  Technology_Details tech Join Employee_Skill_Details ESK on tech.Technology_ID=ESK.Technology_ID
         WHERE emp.Emp_ID = ESK.Emp_ID
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)')
        ,1,1,'') As Technologies FROM Employee_Details emp 

		SELECT Emp_ID,Emp_FirstName,Emp_LastName,Emp_Email,Emp_Designation,Technologies FROM #EmployeeDetails tempemp 
		where tempemp.Emp_ID like'%'+@SearchText+'%' OR tempemp.Emp_FirstName like '%'+@SearchText+'%'
		OR tempemp.Emp_LastName like '%'+@SearchText+'%' OR tempemp.Emp_Email like '%'+@SearchText+'%' 
		OR tempemp.Emp_Designation like '%'+@SearchText+'%' OR tempemp.Technologies like '%'+@SearchText+'%'
		DROP TABLE #EmployeeDetails 
END
GO