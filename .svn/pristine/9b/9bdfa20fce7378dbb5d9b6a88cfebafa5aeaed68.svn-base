/****** Object:  Table [dbo].[CustomerLocation]    Script Date: 1/19/2017 9:34:41 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomerLocation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[LocationName] [nvarchar](500) NOT NULL,
	[LocationShortName] [nvarchar](250) NOT NULL,
	[Website] [nvarchar](500) NULL,
	[LocationPhone] [nvarchar](20) NOT NULL,
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
	[SameAddAsAbove] [bit] NOT NULL,
 CONSTRAINT [PK_CustomerLocation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO