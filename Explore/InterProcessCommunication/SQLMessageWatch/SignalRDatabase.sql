USE [master]
GO
/****** Object:  Database [SignalRDatabase]    Script Date: 19-09-2015 21:08:51 ******/
CREATE DATABASE [SignalRDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SignalRDatabase', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\SignalRDatabase.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SignalRDatabase_log', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\SignalRDatabase_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SignalRDatabase] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SignalRDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SignalRDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SignalRDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SignalRDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SignalRDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SignalRDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [SignalRDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SignalRDatabase] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SignalRDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SignalRDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SignalRDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SignalRDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SignalRDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SignalRDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SignalRDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SignalRDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SignalRDatabase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SignalRDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SignalRDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SignalRDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SignalRDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SignalRDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SignalRDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SignalRDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SignalRDatabase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SignalRDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [SignalRDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SignalRDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SignalRDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SignalRDatabase] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [SignalRDatabase]
GO
/****** Object:  StoredProcedure [dbo].[spSelectLog]    Script Date: 19-09-2015 21:08:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[spSelectLog]
as
select A.Name,ML.EventID,LL.Name as LogLevelName,OC.Name as OperationCodeName,ML.ServerName,ML.ComponentName,ML.SubComponentName,ML.Message,ML.StackTrace,ML.CreatedOn  from MessageLog ML 
inner join Application A on ML.ApplicationID = A.ID 
inner join LogLevel LL on ML.LogLevelID = LL.ID
inner join OperationCode OC on ML.OperationCodeID = OC.ID order by ML.ID desc
GO
/****** Object:  Table [dbo].[Application]    Script Date: 19-09-2015 21:08:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Application](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_.] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LogLevel]    Script Date: 19-09-2015 21:08:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogLevel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_LogLevel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MessageLog]    Script Date: 19-09-2015 21:08:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NULL,
	[EventID] [int] NULL,
	[LogLevelID] [int] NULL,
	[OperationCodeID] [int] NULL,
	[CorelationID] [int] NULL,
	[ErrorCode] [nvarchar](50) NULL,
	[ServerName] [nvarchar](50) NULL,
	[ComponentName] [nvarchar](50) NULL,
	[SubComponentName] [nvarchar](50) NOT NULL,
	[UserID] [int] NULL,
	[Message] [nvarchar](50) NULL,
	[StackTrace] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_MessageLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OperationCode]    Script Date: 19-09-2015 21:08:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperationCode](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_OperationCode] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Application] ON 

INSERT [dbo].[Application] ([ID], [Name], [Description], [CreatedOn]) VALUES (1, N'ABC', N'ABC', CAST(0x0000A50300C3A2F0 AS DateTime))
INSERT [dbo].[Application] ([ID], [Name], [Description], [CreatedOn]) VALUES (2, N'XYZ', N'XYZ', CAST(0x0000A50300C3B0D7 AS DateTime))
SET IDENTITY_INSERT [dbo].[Application] OFF
SET IDENTITY_INSERT [dbo].[LogLevel] ON 

INSERT [dbo].[LogLevel] ([ID], [Name], [Description], [CreatedOn]) VALUES (1, N'Debug', N'Its for Debug', CAST(0x0000A50300000000 AS DateTime))
INSERT [dbo].[LogLevel] ([ID], [Name], [Description], [CreatedOn]) VALUES (2, N'Trace', N'Its for Trace', CAST(0x0000A50300000000 AS DateTime))
INSERT [dbo].[LogLevel] ([ID], [Name], [Description], [CreatedOn]) VALUES (3, N'Information', N'Its for Information', CAST(0x0000A50300000000 AS DateTime))
INSERT [dbo].[LogLevel] ([ID], [Name], [Description], [CreatedOn]) VALUES (4, N'Warning', N'Its for warning', CAST(0x0000A50300000000 AS DateTime))
INSERT [dbo].[LogLevel] ([ID], [Name], [Description], [CreatedOn]) VALUES (5, N'Fatal', N'Fatal', CAST(0x0000A50300000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[LogLevel] OFF
SET IDENTITY_INSERT [dbo].[MessageLog] ON 

INSERT [dbo].[MessageLog] ([ID], [ApplicationID], [EventID], [LogLevelID], [OperationCodeID], [CorelationID], [ErrorCode], [ServerName], [ComponentName], [SubComponentName], [UserID], [Message], [StackTrace], [CreatedOn]) VALUES (1, 1, 345, 4, 3, 12, N'3453', N'Test143', N'Test.dll', N'GetData()', 600001, N'User is unable to get data', N'Sample Stack Trace', CAST(0x0000A50300C769A7 AS DateTime))
INSERT [dbo].[MessageLog] ([ID], [ApplicationID], [EventID], [LogLevelID], [OperationCodeID], [CorelationID], [ErrorCode], [ServerName], [ComponentName], [SubComponentName], [UserID], [Message], [StackTrace], [CreatedOn]) VALUES (5, 2, 100, 4, 3, 12, N'3453', N'Test143', N'Test.dll', N'GetData()', 600001, N'User is unable to get data', N'Sample Stack Trace', CAST(0x0000A50300D13402 AS DateTime))
INSERT [dbo].[MessageLog] ([ID], [ApplicationID], [EventID], [LogLevelID], [OperationCodeID], [CorelationID], [ErrorCode], [ServerName], [ComponentName], [SubComponentName], [UserID], [Message], [StackTrace], [CreatedOn]) VALUES (17, 3, 100, 4, 3, 12, N'3453', N'Test143', N'Test.dll', N'GetData()', 600001, N'User is unable to get data', N'Sample Stack Trace', CAST(0x0000A503012F11B9 AS DateTime))
INSERT [dbo].[MessageLog] ([ID], [ApplicationID], [EventID], [LogLevelID], [OperationCodeID], [CorelationID], [ErrorCode], [ServerName], [ComponentName], [SubComponentName], [UserID], [Message], [StackTrace], [CreatedOn]) VALUES (18, 5, 100, 4, 3, 12, N'3453', N'Test143', N'Test.dll', N'GetData()', 600001, N'User is unable to get data', N'Sample Stack Trace', CAST(0x0000A503012F5167 AS DateTime))
INSERT [dbo].[MessageLog] ([ID], [ApplicationID], [EventID], [LogLevelID], [OperationCodeID], [CorelationID], [ErrorCode], [ServerName], [ComponentName], [SubComponentName], [UserID], [Message], [StackTrace], [CreatedOn]) VALUES (19, 6, 100, 4, 3, 12, N'3453', N'Test143', N'Test.dll', N'GetData()', 600001, N'User is unable to get data', N'Sample Stack Trace', CAST(0x0000A5030130756E AS DateTime))
INSERT [dbo].[MessageLog] ([ID], [ApplicationID], [EventID], [LogLevelID], [OperationCodeID], [CorelationID], [ErrorCode], [ServerName], [ComponentName], [SubComponentName], [UserID], [Message], [StackTrace], [CreatedOn]) VALUES (20, 6, 100, 4, 3, 12, N'3453', N'Test143', N'Test.dll', N'GetData()', 600001, N'User is unable to get data', N'Sample Stack Trace', CAST(0x0000A503013147B7 AS DateTime))
INSERT [dbo].[MessageLog] ([ID], [ApplicationID], [EventID], [LogLevelID], [OperationCodeID], [CorelationID], [ErrorCode], [ServerName], [ComponentName], [SubComponentName], [UserID], [Message], [StackTrace], [CreatedOn]) VALUES (21, 6, 100, 4, 3, 12, N'3453', N'Test143', N'Test.dll', N'GetData()', 600001, N'User is unable to get data', N'Sample Stack Trace', CAST(0x0000A503013A96A0 AS DateTime))
INSERT [dbo].[MessageLog] ([ID], [ApplicationID], [EventID], [LogLevelID], [OperationCodeID], [CorelationID], [ErrorCode], [ServerName], [ComponentName], [SubComponentName], [UserID], [Message], [StackTrace], [CreatedOn]) VALUES (22, 6, 100, 4, 3, 12, N'3453', N'Test143', N'Test.dll', N'GetData()', 600001, N'User is unable to get data', N'Sample Stack Trace', CAST(0x0000A503013C4DFF AS DateTime))
INSERT [dbo].[MessageLog] ([ID], [ApplicationID], [EventID], [LogLevelID], [OperationCodeID], [CorelationID], [ErrorCode], [ServerName], [ComponentName], [SubComponentName], [UserID], [Message], [StackTrace], [CreatedOn]) VALUES (23, 6, 100, 4, 3, 12, N'3453', N'Test143', N'Test.dll', N'GetData()', 600001, N'User is unable to get data', N'Sample Stack Trace', CAST(0x0000A503013D89B2 AS DateTime))
INSERT [dbo].[MessageLog] ([ID], [ApplicationID], [EventID], [LogLevelID], [OperationCodeID], [CorelationID], [ErrorCode], [ServerName], [ComponentName], [SubComponentName], [UserID], [Message], [StackTrace], [CreatedOn]) VALUES (24, 6, 100, 2, 2, 12, N'3453', N'Test143', N'Test.dll', N'GetData()', 600001, N'User is unable to get data', N'Sample Stack Trace', CAST(0x0000A503013ED579 AS DateTime))
INSERT [dbo].[MessageLog] ([ID], [ApplicationID], [EventID], [LogLevelID], [OperationCodeID], [CorelationID], [ErrorCode], [ServerName], [ComponentName], [SubComponentName], [UserID], [Message], [StackTrace], [CreatedOn]) VALUES (25, 6, 100, 2, 1, 12, N'3453', N'Test143', N'Test.dll', N'GetData()', 600001, N'User is unable to get data', N'Sample Stack Trace', CAST(0x0000A503013F4CF8 AS DateTime))
INSERT [dbo].[MessageLog] ([ID], [ApplicationID], [EventID], [LogLevelID], [OperationCodeID], [CorelationID], [ErrorCode], [ServerName], [ComponentName], [SubComponentName], [UserID], [Message], [StackTrace], [CreatedOn]) VALUES (26, 6, 100, 5, 1, 12, N'3453', N'Test143', N'Test.dll', N'GetData()', 600001, N'User is unable to get data', N'Sample Stack Trace', CAST(0x0000A5030147C0B0 AS DateTime))
INSERT [dbo].[MessageLog] ([ID], [ApplicationID], [EventID], [LogLevelID], [OperationCodeID], [CorelationID], [ErrorCode], [ServerName], [ComponentName], [SubComponentName], [UserID], [Message], [StackTrace], [CreatedOn]) VALUES (27, 6, 100, 5, 3, 12, N'3453', N'Test143', N'Test.dll', N'GetData()', 600001, N'User is unable to get data', N'Sample Stack Trace', CAST(0x0000A504009E3C7E AS DateTime))
INSERT [dbo].[MessageLog] ([ID], [ApplicationID], [EventID], [LogLevelID], [OperationCodeID], [CorelationID], [ErrorCode], [ServerName], [ComponentName], [SubComponentName], [UserID], [Message], [StackTrace], [CreatedOn]) VALUES (28, 4, 102, 3, 2, 13, N'3467', N'Test143', N'Test.dll', N'GetData()', 345, N'User is unable to get data', N'Sample Stack Trace', CAST(0x0000A5180158B506 AS DateTime))
SET IDENTITY_INSERT [dbo].[MessageLog] OFF
SET IDENTITY_INSERT [dbo].[OperationCode] ON 

INSERT [dbo].[OperationCode] ([ID], [Name], [Description], [CreatedOn]) VALUES (1, N'Info', N'Info', CAST(0x0000A50300C49BB7 AS DateTime))
INSERT [dbo].[OperationCode] ([ID], [Name], [Description], [CreatedOn]) VALUES (2, N'Start', N'Start', CAST(0x0000A50300C4AF63 AS DateTime))
INSERT [dbo].[OperationCode] ([ID], [Name], [Description], [CreatedOn]) VALUES (3, N'Stop', N'Stop', CAST(0x0000A50300C4B64B AS DateTime))
INSERT [dbo].[OperationCode] ([ID], [Name], [Description], [CreatedOn]) VALUES (4, N'Data Collection Start', N'Data Collection Start', CAST(0x0000A50300C4CF32 AS DateTime))
INSERT [dbo].[OperationCode] ([ID], [Name], [Description], [CreatedOn]) VALUES (5, N'Data Collection Stop', N'Data Collection Stop', CAST(0x0000A50300C4E2CD AS DateTime))
INSERT [dbo].[OperationCode] ([ID], [Name], [Description], [CreatedOn]) VALUES (6, N'Reply', N'Reply', CAST(0x0000A50300C4F638 AS DateTime))
INSERT [dbo].[OperationCode] ([ID], [Name], [Description], [CreatedOn]) VALUES (7, N'Resume', N'Resume', CAST(0x0000A50300C504A9 AS DateTime))
SET IDENTITY_INSERT [dbo].[OperationCode] OFF
ALTER TABLE [dbo].[Application] ADD  CONSTRAINT [DF_._CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[MessageLog] ADD  CONSTRAINT [DF_MessageLog_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[OperationCode] ADD  CONSTRAINT [DF_OperationCode_CreatedOn]  DEFAULT (getdate()) FOR [CreatedOn]
GO
USE [master]
GO
ALTER DATABASE [SignalRDatabase] SET  READ_WRITE 
GO
