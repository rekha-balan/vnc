CREATE TABLE [dbo].[DBFileGroups]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [Database_ID] UNIQUEIDENTIFIER NULL, 

    [Name_FileGroup] VARCHAR(256) NULL, 
	[Size] FLOAT NULL, 

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_FileGroups] PRIMARY KEY NONCLUSTERED ([ID]),
    CONSTRAINT [FK_DBFileGroups_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_DBFileGroups_Database_ID] ON [dbo].[DBFileGroups] ([Database_ID])
