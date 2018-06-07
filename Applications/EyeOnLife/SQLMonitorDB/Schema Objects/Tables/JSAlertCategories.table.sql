CREATE TABLE [dbo].[JSAlertCategories]
(
	[ID] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [JobServer_ID] UNIQUEIDENTIFIER NULL, 

    [Name_JSAlertCategory] VARCHAR(256) NULL, 

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_JSAlertCategories] PRIMARY KEY NONCLUSTERED ([ID]),
	CONSTRAINT [FK_JSAlertCategories_JobServers] FOREIGN KEY ([JobServer_ID]) REFERENCES [dbo].[JobServers] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_JSAlertCategories_JobServer_ID] ON [dbo].[JSAlertCategories] ([JobServer_ID])
