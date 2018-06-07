CREATE TABLE [dbo].[JSTargetServerGroups]
(
	[ID] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [JobServer_ID] UNIQUEIDENTIFIER NULL, 

	[Name_JSTargetServerGroups] VARCHAR(256) NULL,

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_JSTargetServerGroups] PRIMARY KEY NONCLUSTERED ([ID]),
	CONSTRAINT [FK_JSTargetServerGroups_JobServers] FOREIGN KEY ([JobServer_ID]) REFERENCES [dbo].[JobServers] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_JSTargetServerGroups_JobServer_ID] ON [dbo].[JSTargetServerGroups] ([JobServer_ID])