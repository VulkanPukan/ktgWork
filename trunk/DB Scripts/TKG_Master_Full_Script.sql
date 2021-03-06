USE [master]
GO
/****** Object:  Database [TKGMaster]    Script Date: 5/4/2017 12:01:20 AM ******/
CREATE DATABASE [TKGMaster]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TKGMaster', FILENAME = N'G:\Work\My Programmer\DB\TKGMaster.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TKGMaster_log', FILENAME = N'G:\Work\My Programmer\DB\TKGMaster_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TKGMaster] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TKGMaster].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TKGMaster] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TKGMaster] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TKGMaster] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TKGMaster] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TKGMaster] SET ARITHABORT OFF 
GO
ALTER DATABASE [TKGMaster] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TKGMaster] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [TKGMaster] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TKGMaster] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TKGMaster] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TKGMaster] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TKGMaster] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TKGMaster] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TKGMaster] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TKGMaster] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TKGMaster] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TKGMaster] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TKGMaster] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TKGMaster] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TKGMaster] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TKGMaster] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TKGMaster] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TKGMaster] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TKGMaster] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TKGMaster] SET  MULTI_USER 
GO
ALTER DATABASE [TKGMaster] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TKGMaster] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TKGMaster] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TKGMaster] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [TKGMaster]
GO
/****** Object:  Table [dbo].[CustomerMaster]    Script Date: 5/4/2017 12:01:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMaster](
	[CustomerID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[CustomerName] [nvarchar](50) NOT NULL,
	[CustomerDisplayName] [nvarchar](100) NULL,
	[CustomerAttachmentPath] [varchar](200) NOT NULL,
	[CustomerConnStr] [nvarchar](400) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsSuperAdmin] [bit] NOT NULL,
	[CustomerIDinTKG] [int] NOT NULL,
 CONSTRAINT [PK_CustomerMaster] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CustomerMaster] ON 

INSERT [dbo].[CustomerMaster] ([CustomerID], [CustomerName], [CustomerDisplayName], [CustomerAttachmentPath], [CustomerConnStr], [IsActive], [IsSuperAdmin], [CustomerIDinTKG]) VALUES (CAST(1 AS Numeric(18, 0)), N'tkg', N'TKG', N'D:', N'server=rohitlaptop\sqlexpress;database=ktg-dev;uid=sa;password=Password123;', 1, 1, 0)
INSERT [dbo].[CustomerMaster] ([CustomerID], [CustomerName], [CustomerDisplayName], [CustomerAttachmentPath], [CustomerConnStr], [IsActive], [IsSuperAdmin], [CustomerIDinTKG]) VALUES (CAST(2 AS Numeric(18, 0)), N'og1', N'OG1', N'D:', N'server=rohitlaptop\sqlexpress;database=og1-dev;uid=sa;password=Password123;', 1, 0, 9)
INSERT [dbo].[CustomerMaster] ([CustomerID], [CustomerName], [CustomerDisplayName], [CustomerAttachmentPath], [CustomerConnStr], [IsActive], [IsSuperAdmin], [CustomerIDinTKG]) VALUES (CAST(3 AS Numeric(18, 0)), N'democustomer', N'DemoCustomer', N'D:', N'server=rohitlaptop\sqlexpress;database=democust;uid=sa;password=Password123;', 1, 0, 17)
SET IDENTITY_INSERT [dbo].[CustomerMaster] OFF
ALTER TABLE [dbo].[CustomerMaster] ADD  CONSTRAINT [DF_CustomerMaster_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[CustomerMaster] ADD  CONSTRAINT [DF_CustomerMaster_IsSuperAdmin]  DEFAULT ((0)) FOR [IsSuperAdmin]
GO
ALTER TABLE [dbo].[CustomerMaster] ADD  CONSTRAINT [DF_CustomerMaster_CustomerIDinTKG]  DEFAULT ((0)) FOR [CustomerIDinTKG]
GO
USE [master]
GO
ALTER DATABASE [TKGMaster] SET  READ_WRITE 
GO
