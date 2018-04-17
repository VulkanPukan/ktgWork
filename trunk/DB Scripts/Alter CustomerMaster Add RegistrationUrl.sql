/*
   26 грудня 2017 р.23:10:04
   User: 
   Server: (localdb)\v12.0
   Database: TKGMaster
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
ALTER TABLE dbo.CustomerMaster ADD
	RegistrationUrl nvarchar(2000) NULL
GO
ALTER TABLE dbo.CustomerMaster SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CustomerMaster', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CustomerMaster', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CustomerMaster', 'Object', 'CONTROL') as Contr_Per 