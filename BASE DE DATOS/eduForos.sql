USE [master]
GO
/****** Object:  Database [eduForosDB]    Script Date: 8/04/2024 3:17:21 p. m. ******/
CREATE DATABASE [eduForosDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'eduForosDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\eduForosDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'eduForosDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\eduForosDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [eduForosDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [eduForosDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [eduForosDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [eduForosDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [eduForosDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [eduForosDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [eduForosDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [eduForosDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [eduForosDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [eduForosDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [eduForosDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [eduForosDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [eduForosDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [eduForosDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [eduForosDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [eduForosDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [eduForosDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [eduForosDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [eduForosDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [eduForosDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [eduForosDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [eduForosDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [eduForosDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [eduForosDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [eduForosDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [eduForosDB] SET  MULTI_USER 
GO
ALTER DATABASE [eduForosDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [eduForosDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [eduForosDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [eduForosDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [eduForosDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [eduForosDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [eduForosDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [eduForosDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [eduForosDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/04/2024 3:17:22 p. m. ******/
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
/****** Object:  Table [dbo].[answer]    Script Date: 8/04/2024 3:17:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[answer](
	[idAnswer] [int] IDENTITY(1,1) NOT NULL,
	[message] [varchar](500) NOT NULL,
	[route] [varchar](200) NOT NULL,
	[date] [date] NULL,
	[idMessage] [int] NULL,
	[idUser] [uniqueidentifier] NULL,
 CONSTRAINT [PK_answer] PRIMARY KEY CLUSTERED 
(
	[idAnswer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[course]    Script Date: 8/04/2024 3:17:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[course](
	[idCourse] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [varchar](500) NULL,
	[create_at] [date] NULL,
	[update_at] [date] NULL,
 CONSTRAINT [PK_course] PRIMARY KEY CLUSTERED 
(
	[idCourse] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[courseSubject]    Script Date: 8/04/2024 3:17:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[courseSubject](
	[idCourseSubject] [int] IDENTITY(1,1) NOT NULL,
	[idCourse] [int] NOT NULL,
	[idSubject] [int] NOT NULL,
 CONSTRAINT [PK_courseSubject] PRIMARY KEY CLUSTERED 
(
	[idCourseSubject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[forum]    Script Date: 8/04/2024 3:17:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[forum](
	[idForum] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [varchar](500) NULL,
	[startDate] [date] NOT NULL,
	[endDate] [date] NOT NULL,
	[urlPhoto] [varchar](250) NULL,
	[assetId] [varchar](250) NULL,
	[idCourse] [int] NOT NULL,
	[idUser] [uniqueidentifier] NULL,
	[idSubject] [int] NOT NULL,
	[create_at] [date] NULL,
	[update_at] [date] NULL,
 CONSTRAINT [PK_forum] PRIMARY KEY CLUSTERED 
(
	[idForum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[message]    Script Date: 8/04/2024 3:17:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[message](
	[idMessage] [int] IDENTITY(1,1) NOT NULL,
	[message] [varchar](500) NULL,
	[assetId] [varchar](250) NULL,
	[urlFile] [varchar](250) NULL,
	[date] [date] NOT NULL,
	[calification] [int] NULL,
	[idForum] [int] NOT NULL,
	[idUser] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_message] PRIMARY KEY CLUSTERED 
(
	[idMessage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[subject]    Script Date: 8/04/2024 3:17:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subject](
	[idSubject] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[create_at] [date] NULL,
	[update_at] [date] NULL,
 CONSTRAINT [PK_subject] PRIMARY KEY CLUSTERED 
(
	[idSubject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 8/04/2024 3:17:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[idUser] [uniqueidentifier] NOT NULL,
	[userDocumentType] [varchar](50) NOT NULL,
	[userDocument] [varchar](50) NOT NULL,
	[UrlPhoto] [varchar](250) NULL,
	[firtsName] [varchar](100) NOT NULL,
	[surName] [varchar](100) NOT NULL,
	[age] [varchar](50) NOT NULL,
	[AssetId] [varchar](250) NULL,
	[telephone] [varchar](50) NOT NULL,
	[institutionalEmail] [varchar](50) NOT NULL,
	[personalEmail] [varchar](50) NULL,
	[password] [varchar](50) NOT NULL,
	[userType] [varchar](50) NOT NULL,
	[profession] [varchar](100) NOT NULL,
	[create_at] [date] NULL,
	[update_at] [date] NULL,
 CONSTRAINT [PK_user_1] PRIMARY KEY CLUSTERED 
(
	[idUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userCourse]    Script Date: 8/04/2024 3:17:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userCourse](
	[idUserCourse] [int] IDENTITY(1,1) NOT NULL,
	[idUser] [uniqueidentifier] NOT NULL,
	[idCourse] [int] NOT NULL,
 CONSTRAINT [PK_userCourse] PRIMARY KEY CLUSTERED 
(
	[idUserCourse] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userSubject]    Script Date: 8/04/2024 3:17:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userSubject](
	[idUserSubject] [int] IDENTITY(1,1) NOT NULL,
	[idUser] [uniqueidentifier] NOT NULL,
	[idSubject] [int] NOT NULL,
 CONSTRAINT [PK_userSubject] PRIMARY KEY CLUSTERED 
(
	[idUserSubject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[videoConference]    Script Date: 8/04/2024 3:17:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[videoConference](
	[idVideoConference] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [varchar](250) NULL,
	[idCourse] [int] NOT NULL,
	[idSubject] [int] NOT NULL,
	[link] [varchar](100) NOT NULL,
	[startDate] [date] NOT NULL,
	[endDate] [date] NOT NULL,
	[create_at] [date] NULL,
	[update_at] [date] NULL,
 CONSTRAINT [PK_videoConference] PRIMARY KEY CLUSTERED 
(
	[idVideoConference] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[answer]  WITH CHECK ADD  CONSTRAINT [FK_answer_message] FOREIGN KEY([idMessage])
REFERENCES [dbo].[message] ([idMessage])
GO
ALTER TABLE [dbo].[answer] CHECK CONSTRAINT [FK_answer_message]
GO
ALTER TABLE [dbo].[answer]  WITH CHECK ADD  CONSTRAINT [FK_answer_user] FOREIGN KEY([idUser])
REFERENCES [dbo].[user] ([idUser])
GO
ALTER TABLE [dbo].[answer] CHECK CONSTRAINT [FK_answer_user]
GO
ALTER TABLE [dbo].[courseSubject]  WITH CHECK ADD  CONSTRAINT [FK_courseSubject_course] FOREIGN KEY([idCourse])
REFERENCES [dbo].[course] ([idCourse])
GO
ALTER TABLE [dbo].[courseSubject] CHECK CONSTRAINT [FK_courseSubject_course]
GO
ALTER TABLE [dbo].[courseSubject]  WITH CHECK ADD  CONSTRAINT [FK_courseSubject_subject] FOREIGN KEY([idSubject])
REFERENCES [dbo].[subject] ([idSubject])
GO
ALTER TABLE [dbo].[courseSubject] CHECK CONSTRAINT [FK_courseSubject_subject]
GO
ALTER TABLE [dbo].[forum]  WITH CHECK ADD  CONSTRAINT [FK_forum_course] FOREIGN KEY([idCourse])
REFERENCES [dbo].[course] ([idCourse])
GO
ALTER TABLE [dbo].[forum] CHECK CONSTRAINT [FK_forum_course]
GO
ALTER TABLE [dbo].[forum]  WITH CHECK ADD  CONSTRAINT [FK_forum_subject] FOREIGN KEY([idSubject])
REFERENCES [dbo].[subject] ([idSubject])
GO
ALTER TABLE [dbo].[forum] CHECK CONSTRAINT [FK_forum_subject]
GO
ALTER TABLE [dbo].[forum]  WITH CHECK ADD  CONSTRAINT [FK_forum_user] FOREIGN KEY([idUser])
REFERENCES [dbo].[user] ([idUser])
GO
ALTER TABLE [dbo].[forum] CHECK CONSTRAINT [FK_forum_user]
GO
ALTER TABLE [dbo].[message]  WITH CHECK ADD  CONSTRAINT [FK_message_forum] FOREIGN KEY([idForum])
REFERENCES [dbo].[forum] ([idForum])
GO
ALTER TABLE [dbo].[message] CHECK CONSTRAINT [FK_message_forum]
GO
ALTER TABLE [dbo].[message]  WITH CHECK ADD  CONSTRAINT [FK_message_user] FOREIGN KEY([idUser])
REFERENCES [dbo].[user] ([idUser])
GO
ALTER TABLE [dbo].[message] CHECK CONSTRAINT [FK_message_user]
GO
ALTER TABLE [dbo].[userCourse]  WITH CHECK ADD  CONSTRAINT [FK_userCourse_course] FOREIGN KEY([idCourse])
REFERENCES [dbo].[course] ([idCourse])
GO
ALTER TABLE [dbo].[userCourse] CHECK CONSTRAINT [FK_userCourse_course]
GO
ALTER TABLE [dbo].[userCourse]  WITH CHECK ADD  CONSTRAINT [FK_userCourse_user] FOREIGN KEY([idUser])
REFERENCES [dbo].[user] ([idUser])
GO
ALTER TABLE [dbo].[userCourse] CHECK CONSTRAINT [FK_userCourse_user]
GO
ALTER TABLE [dbo].[userSubject]  WITH CHECK ADD  CONSTRAINT [FK_userSubject_subject] FOREIGN KEY([idSubject])
REFERENCES [dbo].[subject] ([idSubject])
GO
ALTER TABLE [dbo].[userSubject] CHECK CONSTRAINT [FK_userSubject_subject]
GO
ALTER TABLE [dbo].[userSubject]  WITH CHECK ADD  CONSTRAINT [FK_userSubject_user] FOREIGN KEY([idUser])
REFERENCES [dbo].[user] ([idUser])
GO
ALTER TABLE [dbo].[userSubject] CHECK CONSTRAINT [FK_userSubject_user]
GO
ALTER TABLE [dbo].[videoConference]  WITH CHECK ADD  CONSTRAINT [FK_videoConference_course] FOREIGN KEY([idCourse])
REFERENCES [dbo].[course] ([idCourse])
GO
ALTER TABLE [dbo].[videoConference] CHECK CONSTRAINT [FK_videoConference_course]
GO
ALTER TABLE [dbo].[videoConference]  WITH CHECK ADD  CONSTRAINT [FK_videoConference_subject] FOREIGN KEY([idSubject])
REFERENCES [dbo].[subject] ([idSubject])
GO
ALTER TABLE [dbo].[videoConference] CHECK CONSTRAINT [FK_videoConference_subject]
GO
USE [master]
GO
ALTER DATABASE [eduForosDB] SET  READ_WRITE 
GO
