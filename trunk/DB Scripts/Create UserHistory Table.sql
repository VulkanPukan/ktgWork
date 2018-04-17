/****** Object:  Table [dbo].[UserHistory]    Script Date: 07.12.2017 21:52:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ByUserId] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL,
	[Field] [nvarchar](150) NOT NULL,
	[CoachID] [int] NOT NULL,
	[TeamID] [int] NOT NULL,
 CONSTRAINT [PK_UserHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/*INSERT INTO UserHistory(UserId, ByUserId, StartDate, EndDate, Field, CoachID, TeamID )
      SELECT             UserID,       63,  EnablementDate, NULL, 'Team', 0, TeamID
	  FROM [ktg-dev].[dbo].[User]
	  where UserType = 3 AND TeamID IS not null

INSERT INTO UserHistory(UserId, ByUserId, StartDate, EndDate, Field, CoachID, TeamID )
      SELECT             UserID,       63,  EnablementDate, NULL, 'Coach', CoachID, 0
	  FROM [ktg-dev].[dbo].[User]
	  where UserType = 3 AND CoachID IS not null*/