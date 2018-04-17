ALTER TABLE [dbo].[RolePermission] DROP CONSTRAINT [FK_RolePermission_Permission]
Go
/****** Object:  Table [dbo].[Permission]    Script Date: 12/20/2017 8:11:45 PM ******/
DROP TABLE [dbo].[Permission]
GO

/****** Object:  Table [dbo].[Permission]    Script Date: 12/20/2017 8:10:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[PermissionID] [int] IDENTITY(1,1) NOT NULL,
	[PermissionName] [nvarchar](150) NOT NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
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

INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (1, N'Manage Programs', 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (2, N'Manage Exercises', 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (3, N'Manage Customers', 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (4, N'Manage Locations', 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (5, N'Manage Athletes', 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (6, N'Manage Roles', 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (7, N'Manage Users', 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (8, N'Admin Dashboard', 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (9, N'Coach Dashboard', 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (10, N'Athlete Dashboard', 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (11, N'Manage Team', 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (12, N'Manage Session Data', 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Permission] ([PermissionID], [PermissionName], [IsActive], [IsDeleted], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (13, N'Manage Assessment', 1, 1, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Permission] OFF
ALTER TABLE [dbo].[Permission] ADD  CONSTRAINT [DF_Permission_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Permission] ADD  CONSTRAINT [DF_Permission_IsDeleted]  DEFAULT ((1)) FOR [IsDeleted]
GO
