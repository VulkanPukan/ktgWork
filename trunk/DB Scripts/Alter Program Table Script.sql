Alter Table [dbo].[Program] Add [IsDeleted] [bit] NOT NULL DEFAULT 0 WITH VALUES
Go
Alter Table [dbo].[Program] Add [UpdateDate] [datetime] NOT NULL DEFAULT getdate() WITH VALUES
Go
Alter Table [dbo].[Program] Add [SportID] [int] NOT NULL DEFAULT 1 WITH VALUES
Go
Alter Table [dbo].[Program] Add [PositionID] [int] NOT NULL DEFAULT 1 WITH VALUES
Go
Alter Table [dbo].[Program] Add [TrainingSeasonID] [int] NOT NULL DEFAULT 1 WITH VALUES
Go
Alter Table [dbo].[Program] Add [Objective] [nvarchar](100) NULL
Go
Alter Table [dbo].[Program] Add [TrainingPhase] [nvarchar](50) NULL