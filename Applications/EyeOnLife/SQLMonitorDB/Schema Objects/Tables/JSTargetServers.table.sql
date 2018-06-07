CREATE TABLE [dbo].[JSTargetServers]
(
	[ID] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [JobServer_ID] UNIQUEIDENTIFIER NULL, 

	[Name_JSTargetServer] VARCHAR(256) NULL,

	[EnlistDate] DATETIME2 NULL,
	[ID_TargetServer] INT NULL,
	[LastPollDate] DATETIME2 NULL,
	[LocalTime] DATETIME2 NULL,
	[Location] VARCHAR(256) NULL,
	[PendingInstructions] INT NULL,
	[PollingInterval] INT NULL,
	[State] VARCHAR(256) NULL,
	[Status] VARCHAR(256) NULL,

    [SnapShotDate] DATETIME2 NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_JSTargetServers] PRIMARY KEY NONCLUSTERED ([ID]),
	CONSTRAINT [FK_JSTargetServers_JobServers] FOREIGN KEY ([JobServer_ID]) REFERENCES [dbo].[JobServers] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_JSTargetServers_JobServer_ID] ON [dbo].[JSTargetServers] ([JobServer_ID])