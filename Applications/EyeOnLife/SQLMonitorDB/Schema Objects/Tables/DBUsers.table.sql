CREATE TABLE [dbo].[DBUsers]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [Database_ID] UNIQUEIDENTIFIER NULL, 

	[Name_Database] VARCHAR(256) NULL,
    [Name_User] VARCHAR(256) NULL, 
    [CreateDate]       DATETIME         NULL,
    [DateLastModified] DATETIME         NULL,
    [HasDBAccess] BIT NULL, 
    [IsSystemObject]   BIT     NULL,
    [Login] VARCHAR(256) NULL, 
    [LoginType] VARCHAR(50) NULL,
	 
    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    [NotFound] BIT NULL DEFAULT 0, 

    CONSTRAINT [PK_DBUsers] PRIMARY KEY NONCLUSTERED ([ID]),
    CONSTRAINT [FK_DBUsers_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_DBUsers_Database_ID] ON [dbo].[DBUsers] ([Database_ID])
