CREATE TABLE [dbo].[JSJobCategories]
(
	[ID] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [JobServer_ID] UNIQUEIDENTIFIER NULL, 

	[Name_JSJobCategory] VARCHAR(256) NULL,

	[CategoryType] VARCHAR(256) NULL,

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    CONSTRAINT [PK_JSJobCategories] PRIMARY KEY NONCLUSTERED ([ID]),
	CONSTRAINT [FK_JSJobCategories_JobServers] FOREIGN KEY ([JobServer_ID]) REFERENCES [dbo].[JobServers] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_JSJobCategories_JobServer_ID] ON [dbo].[JSJobCategories] ([JobServer_ID])