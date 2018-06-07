CREATE TABLE [dbo].[DBFileGroupInfo]
(
	[ID] int NOT NULL, 
    [DBFileGroup_ID] UNIQUEIDENTIFIER NULL, 
	[Database_ID] UNIQUEIDENTIFIER NULL,

	[Size] FLOAT NULL, 

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_FileGroupInfo] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_DBFileGroupInfo_DBFileGroups] FOREIGN KEY ([DBFileGroup_ID]) REFERENCES [dbo].[DBFileGroups] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_DBFileGroupInfo_DBFileGroup_ID] ON [dbo].[DBFileGroupInfo] ([DBFileGroup_ID])
