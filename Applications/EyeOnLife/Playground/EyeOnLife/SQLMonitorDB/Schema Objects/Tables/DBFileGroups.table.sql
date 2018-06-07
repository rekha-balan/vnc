CREATE TABLE [dbo].[DBFileGroups]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [Database_ID] UNIQUEIDENTIFIER NULL, 
    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    [Name_FileGroup] VARCHAR(256) NULL, 
	[Size] FLOAT NULL, 
    CONSTRAINT [PK_FileGroups] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_DBFileGroups_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID])
)
