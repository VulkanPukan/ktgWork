/****** Object:  Table [dbo].[AthleteAssessmentInfo]    Script Date: 12/18/2017 11:22:21 PM ******/
DROP TABLE [dbo].[AthleteAssessmentInfo]
GO

/****** Object:  Table [dbo].[AthleteAssessmentInfo]    Script Date: 12/18/2017 11:22:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AthleteAssessmentInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AssessmentMasterID] [int] NOT NULL,
	[AssessmentExericseID] [int] NOT NULL,
	[AssessmentValue] [decimal](18, 2) NULL
) ON [PRIMARY]

GO


