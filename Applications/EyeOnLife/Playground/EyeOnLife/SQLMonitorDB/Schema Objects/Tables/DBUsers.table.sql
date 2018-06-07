CREATE TABLE [dbo].[DBUsers]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    [Database_ID] UNIQUEIDENTIFIER NULL, 
	[Name_Database] VARCHAR(256) NULL, 
    [CreateDate]       DATETIME         NULL,
    [DateLastModified] DATETIME         NULL,
    [HasDBAccess] BIT NULL, 
    [IsSystemObject]   BIT     NULL,
    [LoginType] VARCHAR(50) NULL, 
    [Login] VARCHAR(256) NULL, 
    [Name_User] VARCHAR(256) NULL, 
    CONSTRAINT [PK_DBUsers] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_DBUsers_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID])

)
