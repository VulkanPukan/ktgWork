SET IDENTITY_INSERT [dbo].[Team] ON 

INSERT [dbo].[Team] ([ID], [Name], [CoachID], [SportID], [IsSystemCreated]) VALUES (1, N'No Team', 0, 1, 1)
INSERT [dbo].[Team] ([ID], [Name], [CoachID], [SportID], [IsSystemCreated]) VALUES (2, N'Sophomore', 27, 2, 0)
INSERT [dbo].[Team] ([ID], [Name], [CoachID], [SportID], [IsSystemCreated]) VALUES (3, N'Juniors', 1, 1, 0)
INSERT [dbo].[Team] ([ID], [Name], [CoachID], [SportID], [IsSystemCreated]) VALUES (4, N'Seniors', 1, 1, 0)
INSERT [dbo].[Team] ([ID], [Name], [CoachID], [SportID], [IsSystemCreated]) VALUES (5, N'Junior High', 64, 3, 0)
SET IDENTITY_INSERT [dbo].[Team] OFF
