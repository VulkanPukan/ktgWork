BEGIN TRANSACTION
GO
ALTER TABLE dbo.[User] ADD
	OtherGrade nvarchar(100) NULL
GO
COMMIT
