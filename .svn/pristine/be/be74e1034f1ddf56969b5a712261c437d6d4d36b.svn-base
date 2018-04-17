/****** Object:  Table [dbo].[AssessmentMasterInfo]    Script Date: 12/18/2017 11:21:41 PM ******/
DROP TABLE [dbo].[AssessmentMasterInfo]
GO

/****** Object:  Table [dbo].[AssessmentMasterInfo]    Script Date: 12/18/2017 11:21:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AssessmentMasterInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[TeamID] [int] NOT NULL,
	[CoachID] [int] NOT NULL,
	[SportID] [int] NOT NULL,
	[PositionID] [int] NOT NULL,
	[ProgramID] [int] NOT NULL,
	[AssessmentNumber] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[AssessmentMasterInfo] ADD  CONSTRAINT [DF_AssessmentMasterInfo_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[AssessmentMasterInfo] ADD  CONSTRAINT [DF_AssessmentMasterInfo_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO


