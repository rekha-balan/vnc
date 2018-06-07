CREATE TABLE [dbo].[JSAlerts]
(
	[ID] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [JobServer_ID] UNIQUEIDENTIFIER NULL, 

	[Name_JSAlert] VARCHAR(256) NULL,

	[AlertType] VARCHAR(256) NULL,
	[CategoryName] VARCHAR(256) NULL,
	[CountResetDate] DATETIME2 NULL,
	[DatabaseName] VARCHAR(256) NULL,

	[IsEnabled] BIT NULL DEFAULT 0,
	[JobID] UNIQUEIDENTIFIER NULL,
	[JobName] VARCHAR(256) NULL,
	[LastOccurrenceDate] DATETIME2 NULL,
	[LastResponseDate] DATETIME2 NULL,
	[NotificationMessage] VARCHAR(256) NULL,
	[OccurrenceCount] int NULL,
	[Severity] INT NULL,
	[WmiEventNamespace] VARCHAR(256) NULL,
	[WmiEventQuery] VARCHAR(256) NULL,

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_JSAlerts] PRIMARY KEY NONCLUSTERED ([ID]),
	CONSTRAINT [FK_JSAlerts_JobServers] FOREIGN KEY ([JobServer_ID]) REFERENCES [dbo].[JobServers] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_JSAlerts_JobServer_ID] ON [dbo].[JSAlerts] ([JobServer_ID])