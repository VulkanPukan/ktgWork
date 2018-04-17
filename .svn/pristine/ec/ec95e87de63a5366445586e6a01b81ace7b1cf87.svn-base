/****** Object:  Table [dbo].[UserDataVisibility]    Script Date: 23/11/2017 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserDataVisibility](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ResponsibilityType] [int] NOT NULL,
	[SportID] [int] NOT NULL,
	[TeamID] [int] NOT NULL,
 CONSTRAINT [PK_UserDataVisibility] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserDataVisibility]  WITH CHECK ADD  CONSTRAINT [FK_UserDataVisibility_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO