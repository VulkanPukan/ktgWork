/****** Object:  Table [dbo].[Team]    Script Date: 10/4/2017 7:50:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Team](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[CoachID] [int] NOT NULL,
	[SportID] [int] NOT NULL,
	[IsSystemCreated] [bit] NOT NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Team] ADD  CONSTRAINT [DF_Team_IsSystemCreated]  DEFAULT ((0)) FOR [IsSystemCreated]
GO


