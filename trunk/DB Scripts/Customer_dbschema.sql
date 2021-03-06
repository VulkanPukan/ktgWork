
GO
/****** Object:  Table [dbo].[ApplicationServer]    Script Date: 12/28/2016 11:13:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationServer](
	[ApplicationServerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
 CONSTRAINT [PK_ApplicationServer] PRIMARY KEY CLUSTERED 
(
	[ApplicationServerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer]    Script Date: 12/28/2016 11:13:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationName] [nvarchar](500) NOT NULL,
	[Website] [nvarchar](500) NULL,
	[CustomerPhone] [nvarchar](20) NOT NULL,
	[AlternatePhone] [nvarchar](20) NULL,
	[Address] [nvarchar](500) NOT NULL,
	[State] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[ZIPCode] [nvarchar](10) NULL,
	[ContactFirstName] [nvarchar](150) NULL,
	[ContactLastName] [nvarchar](150) NULL,
	[ContactEmail] [nvarchar](150) NULL,
	[ContactPhone] [nvarchar](20) NULL,
	[BillingAddress] [nvarchar](500) NULL,
	[BillingState] [nvarchar](100) NULL,
	[BillingCity] [nvarchar](100) NULL,
	[BillingZIPCode] [nvarchar](10) NULL,
	[FreeTrialStartDate] [datetime] NULL,
	[FreeTrialEndDate] [datetime] NULL,
	[ApplicationServerId] [int] NOT NULL,
	[DatabaseServerId] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomerContact]    Script Date: 12/28/2016 11:13:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerContact](
	[CustomerContactId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[FirstName] [nvarchar](150) NOT NULL,
	[LastName] [nvarchar](150) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[ContactType] [int] NOT NULL,
 CONSTRAINT [PK_CustomerContact] PRIMARY KEY CLUSTERED 
(
	[CustomerContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomerPricing]    Script Date: 12/28/2016 11:13:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerPricing](
	[CustomerPricingId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[TypeOfCustomer] [int] NOT NULL,
	[BillingFrequency] [nvarchar](10) NULL,
	[BillingAmount] [decimal](18, 2) NULL,
	[PaymentOption] [int] NOT NULL,
	[EnrollmentMin] [int] NULL,
	[EnrollmentMax] [int] NULL,
	[NumberOfActiveAthletes] [int] NULL,
	[MaxNumberOfAthletes] [int] NULL,
	[PricePerActiveAthelete] [decimal](18, 2) NULL,
	[MaxPricePerAthelete] [decimal](18, 2) NULL,
	[PricePerAdditionalAthlete] [decimal](18, 2) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
 CONSTRAINT [PK_CustomerPricing] PRIMARY KEY CLUSTERED 
(
	[CustomerPricingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DatabaseServer]    Script Date: 12/28/2016 11:13:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DatabaseServer](
	[DatabaseServerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
 CONSTRAINT [PK_DatabaseServer] PRIMARY KEY CLUSTERED 
(
	[DatabaseServerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[ApplicationServer] ON 

GO
INSERT [dbo].[ApplicationServer] ([ApplicationServerId], [Name]) VALUES (1, N'Test app server')
GO
SET IDENTITY_INSERT [dbo].[ApplicationServer] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

GO
INSERT [dbo].[Customer] ([CustomerId], [OrganizationName], [Website], [CustomerPhone], [AlternatePhone], [Address], [State], [City], [ZIPCode], [ContactFirstName], [ContactLastName], [ContactEmail], [ContactPhone], [BillingAddress], [BillingState], [BillingCity], [BillingZIPCode], [FreeTrialStartDate], [FreeTrialEndDate], [ApplicationServerId], [DatabaseServerId], [IsDeleted], [IsActive]) VALUES (5, N'24 hour fitness', N'www.test.com', N'123456', N'145678', N'Test address', NULL, NULL, NULL, N'contact first  name', N'contact last name', N'contactemail@email.com', NULL, N'billing address', NULL, NULL, NULL, CAST(0x0000A6DE010328E7 AS DateTime), CAST(0x0000A6F2010328E7 AS DateTime), 1, 1, 0, 1)
GO
INSERT [dbo].[Customer] ([CustomerId], [OrganizationName], [Website], [CustomerPhone], [AlternatePhone], [Address], [State], [City], [ZIPCode], [ContactFirstName], [ContactLastName], [ContactEmail], [ContactPhone], [BillingAddress], [BillingState], [BillingCity], [BillingZIPCode], [FreeTrialStartDate], [FreeTrialEndDate], [ApplicationServerId], [DatabaseServerId], [IsDeleted], [IsActive]) VALUES (8, N'Time fitness', NULL, N'123456', NULL, N'test', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A6DE010328E7 AS DateTime), CAST(0x0000A6F2010328E7 AS DateTime), 1, 1, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[CustomerContact] ON 

GO
INSERT [dbo].[CustomerContact] ([CustomerContactId], [CustomerId], [FirstName], [LastName], [Phone], [Email], [ContactType]) VALUES (1, 5, N'John', N'Erick', N'(502) 885 25
', N'client@to.com', 1)
GO
INSERT [dbo].[CustomerContact] ([CustomerContactId], [CustomerId], [FirstName], [LastName], [Phone], [Email], [ContactType]) VALUES (2, 8, N'David', N'Ray', N'(502) 885 00
', N'dr@testctc.com', 2)
GO
SET IDENTITY_INSERT [dbo].[CustomerContact] OFF
GO
SET IDENTITY_INSERT [dbo].[CustomerPricing] ON 

GO
INSERT [dbo].[CustomerPricing] ([CustomerPricingId], [CustomerId], [TypeOfCustomer], [BillingFrequency], [BillingAmount], [PaymentOption], [EnrollmentMin], [EnrollmentMax], [NumberOfActiveAthletes], [MaxNumberOfAthletes], [PricePerActiveAthelete], [MaxPricePerAthelete], [PricePerAdditionalAthlete], [StartDate], [EndDate]) VALUES (1, 5, 1, N'Monthly', CAST(100.00 AS Decimal(18, 2)), 1, 10, 100, 100, 150, CAST(10.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(0x0000A6E800F92D81 AS DateTime), NULL)
GO
INSERT [dbo].[CustomerPricing] ([CustomerPricingId], [CustomerId], [TypeOfCustomer], [BillingFrequency], [BillingAmount], [PaymentOption], [EnrollmentMin], [EnrollmentMax], [NumberOfActiveAthletes], [MaxNumberOfAthletes], [PricePerActiveAthelete], [MaxPricePerAthelete], [PricePerAdditionalAthlete], [StartDate], [EndDate]) VALUES (2, 8, 2, N'Yearly', CAST(200.00 AS Decimal(18, 2)), 2, 15, 80, 70, 200, CAST(12.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), CAST(0x0000A6E800F92D81 AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[CustomerPricing] OFF
GO
SET IDENTITY_INSERT [dbo].[DatabaseServer] ON 

GO
INSERT [dbo].[DatabaseServer] ([DatabaseServerId], [Name]) VALUES (1, N'test database server')
GO
SET IDENTITY_INSERT [dbo].[DatabaseServer] OFF
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_ApplicationServer] FOREIGN KEY([ApplicationServerId])
REFERENCES [dbo].[ApplicationServer] ([ApplicationServerId])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_ApplicationServer]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_DatabaseServer] FOREIGN KEY([DatabaseServerId])
REFERENCES [dbo].[DatabaseServer] ([DatabaseServerId])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_DatabaseServer]
GO
ALTER TABLE [dbo].[CustomerContact]  WITH CHECK ADD  CONSTRAINT [FK_CustomerContact_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[CustomerContact] CHECK CONSTRAINT [FK_CustomerContact_Customer]
GO
ALTER TABLE [dbo].[CustomerPricing]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPricing_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[CustomerPricing] CHECK CONSTRAINT [FK_CustomerPricing_Customer]
GO
