Alter Table [dbo].[ProgramExercise] Drop Constraint [DF_ProgramExercise_RestBetweenSets]
Go
Alter table [dbo].[ProgramExercise]
ALTER COLUMN [RestBetweenSets] decimal (18, 2) NOT NULL
Go
ALTER TABLE [dbo].[ProgramExercise] ADD  CONSTRAINT [DF_ProgramExercise_RestBetweenSets]  DEFAULT ((1.0)) FOR [RestBetweenSets]
GO