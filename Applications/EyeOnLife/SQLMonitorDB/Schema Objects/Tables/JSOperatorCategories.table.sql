CREATE TABLE [dbo].[JSOperatorCategories]
(
	[ID] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [JobServer_ID] UNIQUEIDENTIFIER NULL, 

	[Name_JSOperatorCategory] VARCHAR(256) NULL,

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_JSOperatorCategories] PRIMARY KEY NONCLUSTERED ([ID]),
	CONSTRAINT [FK_JSOperatorCategories_JobServers] FOREIGN KEY ([JobServer_ID]) REFERENCES [dbo].[JobServers] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_JSOperatorCategories_JobServer_ID] ON [dbo].[JSOperatorCategories] ([JobServer_ID])