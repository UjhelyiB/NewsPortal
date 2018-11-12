﻿/****** Login ******/
USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [NewsPortalUser]    Script Date: 2018.11.03. 22:04:56 ******/
CREATE LOGIN [NewsPortalUser] WITH PASSWORD='NewsPortalUser123', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO

USE [master]
GO
/****** Object:  Database [NewsPortal]    Script Date: 2018.11.03. 22:07:59 ******/
CREATE DATABASE [NewsPortal]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NewsPortal', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\NewsPortal.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'NewsPortal_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\NewsPortal_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [NewsPortal] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NewsPortal].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NewsPortal] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NewsPortal] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NewsPortal] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NewsPortal] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NewsPortal] SET ARITHABORT OFF 
GO
ALTER DATABASE [NewsPortal] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NewsPortal] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NewsPortal] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NewsPortal] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NewsPortal] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NewsPortal] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NewsPortal] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NewsPortal] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NewsPortal] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NewsPortal] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NewsPortal] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NewsPortal] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NewsPortal] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NewsPortal] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NewsPortal] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NewsPortal] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NewsPortal] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NewsPortal] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NewsPortal] SET  MULTI_USER 
GO
ALTER DATABASE [NewsPortal] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NewsPortal] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NewsPortal] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NewsPortal] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [NewsPortal] SET DELAYED_DURABILITY = DISABLED 
GO
USE [NewsPortal]
GO
/****** Object:  User [NewsPortalUser]    Script Date: 2018.11.03. 22:07:59 ******/
CREATE USER [NewsPortalUser] FOR LOGIN [NewsPortalUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [NewsPortalUser]
GO
/****** Object:  Table [dbo].[News]    Script Date: 2018.11.03. 22:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [ntext] NOT NULL,
	[Context] [ntext] NOT NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[News] ON 
GO
INSERT [dbo].[News] ([Id], [Title], [Context]) VALUES (1, N'Test title', N'Test context')
GO
SET IDENTITY_INSERT [dbo].[News] OFF
GO
USE [master]
GO
ALTER DATABASE [NewsPortal] SET  READ_WRITE 
GO