IF EXISTS (SELECT 1 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'dbo.FK_UserImage_User')
   AND parent_object_id = OBJECT_ID(N'dbo.UserImage')
)

ALTER TABLE [dbo].[UserImage] DROP CONSTRAINT [FK_UserImage_User]
GO

IF EXISTS (SELECT 1 FROM sys.tables where object_id=OBJECT_ID(N'dbo.UserImage'))
DROP TABLE [dbo].[UserImage]
GO

/****** Object:  Table [dbo].[UserImage]    Script Date: 28-Dec-16 7:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserImage](
	[ImageID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[ImagePath] [varchar](max) NULL,
 CONSTRAINT [PK_UserImage] PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[UserImage] ON 

GO
INSERT [dbo].[UserImage] ([ImageID], [UserID], [ImagePath]) VALUES (1, 3, N'../images/user/02.jpg')
GO
INSERT [dbo].[UserImage] ([ImageID], [UserID], [ImagePath]) VALUES (2, 4, N'../images/user/03.jpg')
GO
INSERT [dbo].[UserImage] ([ImageID], [UserID], [ImagePath]) VALUES (3, 5, N'../images/user/05.jpg')
GO
INSERT [dbo].[UserImage] ([ImageID], [UserID], [ImagePath]) VALUES (4, 6, N'../images/user/10.jpg')
GO
INSERT [dbo].[UserImage] ([ImageID], [UserID], [ImagePath]) VALUES (5, 7, N'../images/user/09.jpg')
GO
INSERT [dbo].[UserImage] ([ImageID], [UserID], [ImagePath]) VALUES (6, 8, N'../images/user/08.jpg')
GO
INSERT [dbo].[UserImage] ([ImageID], [UserID], [ImagePath]) VALUES (7, 9, N'../images/user/02.jpg')
GO
INSERT [dbo].[UserImage] ([ImageID], [UserID], [ImagePath]) VALUES (8, 10, N'../images/user/03.jpg')
GO
INSERT [dbo].[UserImage] ([ImageID], [UserID], [ImagePath]) VALUES (9, 11, N'../images/user/05.jpg')
GO
INSERT [dbo].[UserImage] ([ImageID], [UserID], [ImagePath]) VALUES (10, 12, N'../images/user/10.jpg')
GO
SET IDENTITY_INSERT [dbo].[UserImage] OFF
GO
ALTER TABLE [dbo].[UserImage]  WITH CHECK ADD  CONSTRAINT [FK_UserImage_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[UserImage] CHECK CONSTRAINT [FK_UserImage_User]
GO
