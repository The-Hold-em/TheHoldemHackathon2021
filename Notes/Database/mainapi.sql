USE [master]
GO
/****** Object:  Database [THH_MainApi]    Script Date: 12/29/2021 1:24:47 AM ******/
CREATE DATABASE [THH_MainApi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'THH_MainApi', FILENAME = N'C:\Users\recep\THH_MainApi.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'THH_MainApi_log', FILENAME = N'C:\Users\recep\THH_MainApi_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [THH_MainApi] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [THH_MainApi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [THH_MainApi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [THH_MainApi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [THH_MainApi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [THH_MainApi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [THH_MainApi] SET ARITHABORT OFF 
GO
ALTER DATABASE [THH_MainApi] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [THH_MainApi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [THH_MainApi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [THH_MainApi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [THH_MainApi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [THH_MainApi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [THH_MainApi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [THH_MainApi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [THH_MainApi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [THH_MainApi] SET  ENABLE_BROKER 
GO
ALTER DATABASE [THH_MainApi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [THH_MainApi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [THH_MainApi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [THH_MainApi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [THH_MainApi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [THH_MainApi] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [THH_MainApi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [THH_MainApi] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [THH_MainApi] SET  MULTI_USER 
GO
ALTER DATABASE [THH_MainApi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [THH_MainApi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [THH_MainApi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [THH_MainApi] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [THH_MainApi] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [THH_MainApi] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [THH_MainApi] SET QUERY_STORE = OFF
GO
USE [THH_MainApi]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12/29/2021 1:24:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 12/29/2021 1:24:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Plate] [int] NOT NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
	[UpdatedTime] [datetime2](7) NULL,
	[CreatedUserId] [uniqueidentifier] NOT NULL,
	[UpdatedUserId] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Districts]    Script Date: 12/29/2021 1:24:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Districts](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[CityId] [uniqueidentifier] NOT NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
	[UpdatedTime] [datetime2](7) NULL,
	[CreatedUserId] [uniqueidentifier] NOT NULL,
	[UpdatedUserId] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Districts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Elections]    Script Date: 12/29/2021 1:24:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Elections](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
	[UpdatedTime] [datetime2](7) NULL,
	[CreatedUserId] [uniqueidentifier] NOT NULL,
	[UpdatedUserId] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Elections] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nodes]    Script Date: 12/29/2021 1:24:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nodes](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[CityId] [uniqueidentifier] NOT NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
	[UpdatedTime] [datetime2](7) NULL,
	[CreatedUserId] [uniqueidentifier] NOT NULL,
	[UpdatedUserId] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Nodes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PollingStations]    Script Date: 12/29/2021 1:24:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PollingStations](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[CityId] [uniqueidentifier] NOT NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
	[UpdatedTime] [datetime2](7) NULL,
	[CreatedUserId] [uniqueidentifier] NOT NULL,
	[UpdatedUserId] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_PollingStations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Districts_CityId]    Script Date: 12/29/2021 1:24:48 AM ******/
CREATE NONCLUSTERED INDEX [IX_Districts_CityId] ON [dbo].[Districts]
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Nodes_CityId]    Script Date: 12/29/2021 1:24:48 AM ******/
CREATE NONCLUSTERED INDEX [IX_Nodes_CityId] ON [dbo].[Nodes]
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PollingStations_CityId]    Script Date: 12/29/2021 1:24:48 AM ******/
CREATE NONCLUSTERED INDEX [IX_PollingStations_CityId] ON [dbo].[PollingStations]
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Districts]  WITH CHECK ADD  CONSTRAINT [FK_Districts_Cities_CityId] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Districts] CHECK CONSTRAINT [FK_Districts_Cities_CityId]
GO
ALTER TABLE [dbo].[Nodes]  WITH CHECK ADD  CONSTRAINT [FK_Nodes_Cities_CityId] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Nodes] CHECK CONSTRAINT [FK_Nodes_Cities_CityId]
GO
ALTER TABLE [dbo].[PollingStations]  WITH CHECK ADD  CONSTRAINT [FK_PollingStations_Cities_CityId] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PollingStations] CHECK CONSTRAINT [FK_PollingStations_Cities_CityId]
GO
USE [master]
GO
ALTER DATABASE [THH_MainApi] SET  READ_WRITE 
GO
