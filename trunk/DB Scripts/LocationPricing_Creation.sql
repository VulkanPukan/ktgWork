/****** Object:  Table [dbo].[LocationPricing]    Script Date: 1/19/2017 10:26:37 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LocationPricing](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LocationID] [int] NOT NULL,
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
 CONSTRAINT [PK_LocationPricing] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


