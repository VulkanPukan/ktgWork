/*
   1 грудня 2017 р.2:59:25
   User: 
   Server: (localdb)\v12.0
   Database: ktg-dev
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Team ADD
	Description nvarchar(1000) NULL,
	Gender int NULL,
	Month int NULL,
	Year int NULL
GO
ALTER TABLE dbo.Team SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Team', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Team', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Team', 'Object', 'CONTROL') as Contr_Per 