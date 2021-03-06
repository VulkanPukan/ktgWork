USE [ktg-dev]
GO
/****** Object:  Table [dbo].[DWChangedExerciseDetails]    Script Date: 9/27/2017 7:28:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DWChangedExerciseDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DWChangedExerciseDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ProgramID] [int] NOT NULL,
	[TrackID] [int] NOT NULL,
	[SessionID] [int] NOT NULL,
	[ExerciseStepID] [int] NOT NULL,
	[ExerciseGroupID] [int] NOT NULL,
	[SetNumber] [int] NOT NULL,
	[OriginalExerciseID] [int] NOT NULL,
	[OriginalTargetLoad] [int] NOT NULL,
	[OriginalTargetReps] [int] NOT NULL,
	[ModifiedExerciseID] [int] NOT NULL,
	[ModifiedTargetLoad] [int] NOT NULL,
	[ModifiedTargetReps] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL
) ON [PRIMARY]
END
GO
