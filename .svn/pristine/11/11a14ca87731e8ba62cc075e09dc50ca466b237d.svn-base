IF NOT EXISTS (SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Contact]') AND name = 'AlternatePhoneNumber'
)
BEGIN
  ALTER TABLE [dbo].[Contact] 
  ADD AlternatePhoneNumber nvarchar(15) NULL
END;

IF NOT EXISTS (SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Contact]') AND name = 'SecondaryEmail'
)
BEGIN
  ALTER TABLE [dbo].[Contact] 
  ADD SecondaryEmail nvarchar(150) NULL
END;