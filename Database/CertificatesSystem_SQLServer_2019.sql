USE [master]
GO
/****** Object:  Database [CertificatesSystem]    Script Date: 5/27/2022 4:14:34 PM ******/
CREATE DATABASE [CertificatesSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CertificatesSystem', FILENAME = N'D:\Archivos de programa\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CertificatesSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CertificatesSystem_log', FILENAME = N'D:\Archivos de programa\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\CertificatesSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CertificatesSystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CertificatesSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CertificatesSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CertificatesSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CertificatesSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CertificatesSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CertificatesSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [CertificatesSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CertificatesSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CertificatesSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CertificatesSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CertificatesSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CertificatesSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CertificatesSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CertificatesSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CertificatesSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CertificatesSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CertificatesSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CertificatesSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CertificatesSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CertificatesSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CertificatesSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CertificatesSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CertificatesSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CertificatesSystem] SET RECOVERY FULL 
GO
ALTER DATABASE [CertificatesSystem] SET  MULTI_USER 
GO
ALTER DATABASE [CertificatesSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CertificatesSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CertificatesSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CertificatesSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CertificatesSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CertificatesSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'CertificatesSystem', N'ON'
GO
ALTER DATABASE [CertificatesSystem] SET QUERY_STORE = OFF
GO
USE [CertificatesSystem]
GO
/****** Object:  Table [dbo].[Grades]    Script Date: 5/27/2022 4:14:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grades](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Grades] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Managers]    Script Date: 5/27/2022 4:14:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Managers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Surname] [varchar](50) NOT NULL,
	[Role] [varchar](50) NOT NULL,
	[Gender] [char](1) NOT NULL,
 CONSTRAINT [PK_Managers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sections]    Script Date: 5/27/2022 4:14:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sections](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [char](1) NOT NULL,
 CONSTRAINT [PK_Sections] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentGrades]    Script Date: 5/27/2022 4:14:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentGrades](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NIE] [int] NOT NULL,
	[GradeId] [int] NOT NULL,
	[SectionId] [int] NOT NULL,
	[Year] [int] NOT NULL,
 CONSTRAINT [PK_StudentGrades] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 5/27/2022 4:14:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[NIE] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Surname] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[NIE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StudentGrades]  WITH CHECK ADD  CONSTRAINT [FK_Grades] FOREIGN KEY([GradeId])
REFERENCES [dbo].[Grades] ([Id])
GO
ALTER TABLE [dbo].[StudentGrades] CHECK CONSTRAINT [FK_Grades]
GO
ALTER TABLE [dbo].[StudentGrades]  WITH CHECK ADD  CONSTRAINT [FK_Sections] FOREIGN KEY([SectionId])
REFERENCES [dbo].[Sections] ([Id])
GO
ALTER TABLE [dbo].[StudentGrades] CHECK CONSTRAINT [FK_Sections]
GO
ALTER TABLE [dbo].[StudentGrades]  WITH CHECK ADD  CONSTRAINT [FK_Students] FOREIGN KEY([NIE])
REFERENCES [dbo].[Students] ([NIE])
GO
ALTER TABLE [dbo].[StudentGrades] CHECK CONSTRAINT [FK_Students]
GO
USE [master]
GO
ALTER DATABASE [CertificatesSystem] SET  READ_WRITE 
GO
