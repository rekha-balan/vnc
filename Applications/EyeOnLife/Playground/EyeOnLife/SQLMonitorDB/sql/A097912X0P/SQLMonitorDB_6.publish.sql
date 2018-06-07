﻿/*
Deployment script for SQLMonitorDB

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "SQLMonitorDB"
:setvar DefaultFilePrefix "SQLMonitorDB"
:setvar DefaultDataPath "D:\Program Files\Microsoft SQL Server\MSSQL10_50.REL02\MSSQL\DATA\"
:setvar DefaultLogPath "D:\Program Files\Microsoft SQL Server\MSSQL10_50.REL02\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
GO

GO
PRINT N'Rename refactoring operation with key 1b561362-680c-4c05-81d8-2c46b5d460e6 is skipped, element [dbo].[LKUP_AreasAndTeams].[ADDomainName] (SqlSimpleColumn) will not be renamed to AreaName';


GO
PRINT N'Altering [dbo].[LKUP_ADDomains]...';


GO
ALTER TABLE [dbo].[LKUP_ADDomains]
    ADD [DC1_HostName]  VARCHAR (50) NULL,
        [DC1_IPAddress] VARCHAR (20) NULL,
        [DC2_HostName]  VARCHAR (50) NULL,
        [DC2_IPAddress] VARCHAR (20) NULL;


GO
PRINT N'Creating [dbo].[LKUP_Teams]...';


GO
CREATE TABLE [dbo].[LKUP_Teams] (
    [ID]       INT          NOT NULL,
    [TeamName] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '1b561362-680c-4c05-81d8-2c46b5d460e6')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('1b561362-680c-4c05-81d8-2c46b5d460e6')

GO

GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
GO

GO
PRINT N'Update complete.'
GO
