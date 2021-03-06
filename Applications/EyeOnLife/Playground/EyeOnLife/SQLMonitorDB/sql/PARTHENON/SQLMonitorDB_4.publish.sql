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
:setvar DefaultDataPath "C:\SQL Data\"
:setvar DefaultLogPath "C:\SQL Data\"

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
PRINT N'Dropping DF__tmp_ms_xx_Da__ID__45BE5BA9...';


GO
ALTER TABLE [dbo].[Databases] DROP CONSTRAINT [DF__tmp_ms_xx_Da__ID__45BE5BA9];


GO
PRINT N'Dropping DF__tmp_ms_xx__IsMon__46B27FE2...';


GO
ALTER TABLE [dbo].[Databases] DROP CONSTRAINT [DF__tmp_ms_xx__IsMon__46B27FE2];


GO
PRINT N'Dropping DF__tmp_ms_xx__Expan__47A6A41B...';


GO
ALTER TABLE [dbo].[Databases] DROP CONSTRAINT [DF__tmp_ms_xx__Expan__47A6A41B];


GO
PRINT N'Dropping DF__tmp_ms_xx__Expan__489AC854...';


GO
ALTER TABLE [dbo].[Databases] DROP CONSTRAINT [DF__tmp_ms_xx__Expan__489AC854];


GO
PRINT N'Dropping DF__tmp_ms_xx__Expan__498EEC8D...';


GO
ALTER TABLE [dbo].[Databases] DROP CONSTRAINT [DF__tmp_ms_xx__Expan__498EEC8D];


GO
PRINT N'Dropping DF__tmp_ms_xx__Expan__4A8310C6...';


GO
ALTER TABLE [dbo].[Databases] DROP CONSTRAINT [DF__tmp_ms_xx__Expan__4A8310C6];


GO
PRINT N'Dropping DF__tmp_ms_xx__Expan__4B7734FF...';


GO
ALTER TABLE [dbo].[Databases] DROP CONSTRAINT [DF__tmp_ms_xx__Expan__4B7734FF];


GO
PRINT N'Dropping DF__tmp_ms_xx__Expan__4C6B5938...';


GO
ALTER TABLE [dbo].[Databases] DROP CONSTRAINT [DF__tmp_ms_xx__Expan__4C6B5938];


GO
PRINT N'Dropping DF__tmp_ms_xx__Expan__4D5F7D71...';


GO
ALTER TABLE [dbo].[Databases] DROP CONSTRAINT [DF__tmp_ms_xx__Expan__4D5F7D71];


GO
PRINT N'Dropping DF__tmp_ms_xx__Expan__4E53A1AA...';


GO
ALTER TABLE [dbo].[Databases] DROP CONSTRAINT [DF__tmp_ms_xx__Expan__4E53A1AA];


GO
PRINT N'Dropping DF__tmp_ms_xx__Expan__4F47C5E3...';


GO
ALTER TABLE [dbo].[Databases] DROP CONSTRAINT [DF__tmp_ms_xx__Expan__4F47C5E3];


GO
PRINT N'Dropping DF__tmp_ms_xx__Expan__503BEA1C...';


GO
ALTER TABLE [dbo].[Databases] DROP CONSTRAINT [DF__tmp_ms_xx__Expan__503BEA1C];


GO
PRINT N'Dropping FK_DBDdlTriggers_Databases...';


GO
ALTER TABLE [dbo].[DBDdlTriggers] DROP CONSTRAINT [FK_DBDdlTriggers_Databases];


GO
PRINT N'Dropping FK_DBFileGroups_Databases...';


GO
ALTER TABLE [dbo].[DBFileGroups] DROP CONSTRAINT [FK_DBFileGroups_Databases];


GO
PRINT N'Dropping FK_DBRoles_Databases...';


GO
ALTER TABLE [dbo].[DBRoles] DROP CONSTRAINT [FK_DBRoles_Databases];


GO
PRINT N'Dropping FK_DBStoredProcedures_Databases...';


GO
ALTER TABLE [dbo].[DBStoredProcedures] DROP CONSTRAINT [FK_DBStoredProcedures_Databases];


GO
PRINT N'Dropping FK_DBTables_Databases...';


GO
ALTER TABLE [dbo].[DBTables] DROP CONSTRAINT [FK_DBTables_Databases];


GO
PRINT N'Dropping FK_DBUserDefinedFunctions_Databases...';


GO
ALTER TABLE [dbo].[DBUserDefinedFunctions] DROP CONSTRAINT [FK_DBUserDefinedFunctions_Databases];


GO
PRINT N'Dropping FK_DBUsers_Databases...';


GO
ALTER TABLE [dbo].[DBUsers] DROP CONSTRAINT [FK_DBUsers_Databases];


GO
PRINT N'Dropping FK_DBViews_Databases...';


GO
ALTER TABLE [dbo].[DBViews] DROP CONSTRAINT [FK_DBViews_Databases];


GO
PRINT N'Dropping FK_DBLogFiles_Databases...';


GO
ALTER TABLE [dbo].[DBLogFiles] DROP CONSTRAINT [FK_DBLogFiles_Databases];


GO
PRINT N'Dropping FK_Databases_Instances...';


GO
ALTER TABLE [dbo].[Databases] DROP CONSTRAINT [FK_Databases_Instances];


GO
PRINT N'Dropping FK_DatbaseInfo_Databases...';


GO
ALTER TABLE [dbo].[DatabaseInfo] DROP CONSTRAINT [FK_DatbaseInfo_Databases];


GO
PRINT N'Starting rebuilding table [dbo].[Databases]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Databases] (
    [ID]                         UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Instance_ID]                UNIQUEIDENTIFIER NULL,
    [Name_Instance]              VARCHAR (256)    NULL,
    [SnapShotDate]               DATETIME         NULL,
    [SnapShotError]              VARCHAR (256)    NULL,
    [Name_Database]              VARCHAR (256)    NULL,
    [CreateDate]                 DATETIME         NULL,
    [DataBaseGuid]               VARCHAR (50)     NULL,
    [DataSpaceUsage]             FLOAT (53)       NULL,
    [DefaultFileGroup]           VARCHAR (256)    NULL,
    [ID_DB]                      INT              NULL,
    [IndexSpaceUsage]            FLOAT (53)       NULL,
    [LastBackupDate]             DATETIME         NULL,
    [LastDifferentialBackupDate] DATETIME         NULL,
    [LastLogBackupDate]          DATETIME         NULL,
    [Owner]                      VARCHAR (50)     NULL,
    [RecoveryModel]              VARCHAR (50)     NULL,
    [Size]                       FLOAT (53)       NULL,
    [SpaceAvailable]             FLOAT (53)       NULL,
    [EP_Area]                    VARCHAR (50)     NULL,
    [EP_Team]                    VARCHAR (50)     NULL,
    [EP_PrimaryDBContact]        VARCHAR (50)     NULL,
    [EP_DBApprover]              VARCHAR (50)     NULL,
    [EP_DRTier]                  VARCHAR (50)     NULL,
    [IsMonitored]                BIT              DEFAULT 0 NULL,
    [ExpandDataFiles]            BIT              DEFAULT 0 NULL,
    [ExpandFileGroups]           BIT              DEFAULT 0 NULL,
    [ExpandLogFiles]             BIT              DEFAULT 0 NULL,
    [ExpandRoles]                BIT              DEFAULT 0 NULL,
    [ExpandStoredProcedures]     BIT              DEFAULT 0 NULL,
    [ExpandTables]               BIT              DEFAULT 0 NULL,
    [ExpandTriggers]             BIT              DEFAULT 0 NULL,
    [ExpandUserDefinedFunctions] BIT              DEFAULT 0 NULL,
    [ExpandUsers]                BIT              DEFAULT 0 NULL,
    [ExpandViews]                BIT              DEFAULT 0 NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Databases] PRIMARY KEY CLUSTERED ([ID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Databases])
    BEGIN
        
        INSERT INTO [dbo].[tmp_ms_xx_Databases] ([ID], [Instance_ID], [SnapShotDate], [SnapShotError], [Name_Database], [CreateDate], [DataBaseGuid], [DataSpaceUsage], [DefaultFileGroup], [ID_DB], [IndexSpaceUsage], [LastBackupDate], [LastDifferentialBackupDate], [LastLogBackupDate], [Owner], [RecoveryModel], [Size], [SpaceAvailable], [EP_Area], [EP_Team], [EP_PrimaryDBContact], [EP_DBApprover], [EP_DRTier], [IsMonitored], [ExpandDataFiles], [ExpandFileGroups], [ExpandLogFiles], [ExpandRoles], [ExpandStoredProcedures], [ExpandTables], [ExpandTriggers], [ExpandUserDefinedFunctions], [ExpandUsers], [ExpandViews])
        SELECT   [ID],
                 [Instance_ID],
                 [SnapShotDate],
                 [SnapShotError],
                 [Name_Database],
                 [CreateDate],
                 [DataBaseGuid],
                 [DataSpaceUsage],
                 [DefaultFileGroup],
                 [ID_DB],
                 [IndexSpaceUsage],
                 [LastBackupDate],
                 [LastDifferentialBackupDate],
                 [LastLogBackupDate],
                 [Owner],
                 [RecoveryModel],
                 [Size],
                 [SpaceAvailable],
                 [EP_Area],
                 [EP_Team],
                 [EP_PrimaryDBContact],
                 [EP_DBApprover],
                 [EP_DRTier],
                 [IsMonitored],
                 [ExpandDataFiles],
                 [ExpandFileGroups],
                 [ExpandLogFiles],
                 [ExpandRoles],
                 [ExpandStoredProcedures],
                 [ExpandTables],
                 [ExpandTriggers],
                 [ExpandUserDefinedFunctions],
                 [ExpandUsers],
                 [ExpandViews]
        FROM     [dbo].[Databases]
        ORDER BY [ID] ASC;
        
    END

DROP TABLE [dbo].[Databases];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Databases]', N'Databases';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Databases]', N'PK_Databases', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[DBUsers]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_DBUsers] (
    [ID]               UNIQUEIDENTIFIER NOT NULL,
    [SnapShotDate]     DATETIME         NULL,
    [SnapShotError]    VARCHAR (256)    NULL,
    [Database_ID]      UNIQUEIDENTIFIER NULL,
    [Name_Database]    VARCHAR (256)    NULL,
    [CreateDate]       DATETIME         NULL,
    [DateLastModified] DATETIME         NULL,
    [HasDBAccess]      BIT              NULL,
    [IsSystemObject]   BIT              NULL,
    [LoginType]        VARCHAR (50)     NULL,
    [Login]            VARCHAR (256)    NULL,
    [Name_User]        VARCHAR (256)    NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_DBUsers] PRIMARY KEY CLUSTERED ([ID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[DBUsers])
    BEGIN
        
        INSERT INTO [dbo].[tmp_ms_xx_DBUsers] ([ID], [SnapShotDate], [SnapShotError], [Database_ID], [CreateDate], [DateLastModified], [HasDBAccess], [IsSystemObject], [LoginType], [Login], [Name_User])
        SELECT   [ID],
                 [SnapShotDate],
                 [SnapShotError],
                 [Database_ID],
                 [CreateDate],
                 [DateLastModified],
                 [HasDBAccess],
                 [IsSystemObject],
                 [LoginType],
                 [Login],
                 [Name_User]
        FROM     [dbo].[DBUsers]
        ORDER BY [ID] ASC;
        
    END

DROP TABLE [dbo].[DBUsers];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_DBUsers]', N'DBUsers';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_DBUsers]', N'PK_DBUsers', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating FK_DBDdlTriggers_Databases...';


GO
ALTER TABLE [dbo].[DBDdlTriggers] WITH NOCHECK
    ADD CONSTRAINT [FK_DBDdlTriggers_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]);


GO
PRINT N'Creating FK_DBFileGroups_Databases...';


GO
ALTER TABLE [dbo].[DBFileGroups] WITH NOCHECK
    ADD CONSTRAINT [FK_DBFileGroups_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]);


GO
PRINT N'Creating FK_DBRoles_Databases...';


GO
ALTER TABLE [dbo].[DBRoles] WITH NOCHECK
    ADD CONSTRAINT [FK_DBRoles_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]);


GO
PRINT N'Creating FK_DBStoredProcedures_Databases...';


GO
ALTER TABLE [dbo].[DBStoredProcedures] WITH NOCHECK
    ADD CONSTRAINT [FK_DBStoredProcedures_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]);


GO
PRINT N'Creating FK_DBTables_Databases...';


GO
ALTER TABLE [dbo].[DBTables] WITH NOCHECK
    ADD CONSTRAINT [FK_DBTables_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]);


GO
PRINT N'Creating FK_DBUserDefinedFunctions_Databases...';


GO
ALTER TABLE [dbo].[DBUserDefinedFunctions] WITH NOCHECK
    ADD CONSTRAINT [FK_DBUserDefinedFunctions_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]);


GO
PRINT N'Creating FK_DBUsers_Databases...';


GO
ALTER TABLE [dbo].[DBUsers] WITH NOCHECK
    ADD CONSTRAINT [FK_DBUsers_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]);


GO
PRINT N'Creating FK_DBViews_Databases...';


GO
ALTER TABLE [dbo].[DBViews] WITH NOCHECK
    ADD CONSTRAINT [FK_DBViews_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]);


GO
PRINT N'Creating FK_DBLogFiles_Databases...';


GO
ALTER TABLE [dbo].[DBLogFiles] WITH NOCHECK
    ADD CONSTRAINT [FK_DBLogFiles_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]);


GO
PRINT N'Creating FK_Databases_Instances...';


GO
ALTER TABLE [dbo].[Databases] WITH NOCHECK
    ADD CONSTRAINT [FK_Databases_Instances] FOREIGN KEY ([Instance_ID]) REFERENCES [dbo].[Instances] ([ID]);


GO
PRINT N'Creating FK_DatbaseInfo_Databases...';


GO
ALTER TABLE [dbo].[DatabaseInfo] WITH NOCHECK
    ADD CONSTRAINT [FK_DatbaseInfo_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]);


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
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[DBDdlTriggers] WITH CHECK CHECK CONSTRAINT [FK_DBDdlTriggers_Databases];

ALTER TABLE [dbo].[DBFileGroups] WITH CHECK CHECK CONSTRAINT [FK_DBFileGroups_Databases];

ALTER TABLE [dbo].[DBRoles] WITH CHECK CHECK CONSTRAINT [FK_DBRoles_Databases];

ALTER TABLE [dbo].[DBStoredProcedures] WITH CHECK CHECK CONSTRAINT [FK_DBStoredProcedures_Databases];

ALTER TABLE [dbo].[DBTables] WITH CHECK CHECK CONSTRAINT [FK_DBTables_Databases];

ALTER TABLE [dbo].[DBUserDefinedFunctions] WITH CHECK CHECK CONSTRAINT [FK_DBUserDefinedFunctions_Databases];

ALTER TABLE [dbo].[DBUsers] WITH CHECK CHECK CONSTRAINT [FK_DBUsers_Databases];

ALTER TABLE [dbo].[DBViews] WITH CHECK CHECK CONSTRAINT [FK_DBViews_Databases];

ALTER TABLE [dbo].[DBLogFiles] WITH CHECK CHECK CONSTRAINT [FK_DBLogFiles_Databases];

ALTER TABLE [dbo].[Databases] WITH CHECK CHECK CONSTRAINT [FK_Databases_Instances];

ALTER TABLE [dbo].[DatabaseInfo] WITH CHECK CHECK CONSTRAINT [FK_DatbaseInfo_Databases];


GO
PRINT N'Update complete.'
GO
