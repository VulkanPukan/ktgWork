USE [ktg-dev]
GO

IF  EXISTS (SELECT *  FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS WHERE CONSTRAINT_NAME ='FK_RolePermission_Role')
BEGIN

ALTER TABLE [dbo].[RolePermission] DROP CONSTRAINT [FK_RolePermission_Role]
END

GO


IF  EXISTS (SELECT *  FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS WHERE CONSTRAINT_NAME ='FK_RolePermission_Permission')
BEGIN

ALTER TABLE [dbo].[RolePermission] DROP CONSTRAINT [FK_RolePermission_Permission]

END

IF  EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[Permission]') AND type in (N'U'))
BEGIN
DROP TABLE [dbo].[Permission]
END

GO
/****** Object:  Table [dbo].[Permission]    Script Date: 02-02-2017 20:47:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Permission](
	[PermissionID] [int] IDENTITY(1,1) NOT NULL,
	[PermissionName] [nvarchar](150) NOT NULL,
	[IsActive] [bit] NULL CONSTRAINT [DF_Permission_IsActive]  DEFAULT ((1)),
	[IsDeleted] [bit] NULL CONSTRAINT [DF_Permission_IsDeleted]  DEFAULT ((1)),
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Permission] ON 

GO
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Manage Programs', 1, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Manage Exercises', 1, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Manage Customers', 1, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, N'Manage Users', 1, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (5, N'Audit Log', 1, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (6, N'Perform Evaluation', 1, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (7, N'Manage Schedule', 1, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (8, N'Dashboard panel1', 1, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (9, N'Dashboard panel2', 1, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (10, N'Dashboard panel3', 1, 1, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Permission] OFF
GO

IF EXISTS(SELECT * FROM sys.columns  WHERE Name = N'RoleD' AND Object_ID = Object_ID(N'RolePermission'))
BEGIN
   EXEC  sp_RENAME 'RolePermission.[RoleD]' , 'RoleID', 'COLUMN'
END

GO

IF   EXISTS (SELECT *  FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS WHERE CONSTRAINT_NAME ='FK_RolePermission_Permission')
BEGIN
ALTER TABLE [dbo].[RolePermission] DROP CONSTRAINT [FK_RolePermission_Permission]
END
GO

ALTER TABLE [dbo].[RolePermission]  WITH CHECK ADD  CONSTRAINT [FK_RolePermission_Permission] FOREIGN KEY([PermissionID])
REFERENCES [dbo].[Permission] ([PermissionID])
GO

ALTER TABLE [dbo].[RolePermission] CHECK CONSTRAINT [FK_RolePermission_Permission]
GO

IF   EXISTS (SELECT *  FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS WHERE CONSTRAINT_NAME ='FK_RolePermission_Role')
BEGIN
ALTER TABLE [dbo].[RolePermission] DROP CONSTRAINT [FK_RolePermission_Role]
END
GO

ALTER TABLE [dbo].[RolePermission]  WITH CHECK ADD  CONSTRAINT [FK_RolePermission_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO

ALTER TABLE [dbo].[RolePermission] CHECK CONSTRAINT [FK_RolePermission_Role]
GO
