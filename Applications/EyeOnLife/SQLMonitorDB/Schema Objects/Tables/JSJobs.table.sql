CREATE TABLE [dbo].[JSJobs]
(
	[ID] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [JobServer_ID] UNIQUEIDENTIFIER NULL, 
    [Instance_ID] UNIQUEIDENTIFIER NULL, 

	[Name_Instance] VARCHAR(256) NULL,

    [Name_JSJob] VARCHAR(256) NULL, 

	[Category] VARCHAR(256) NULL,
	[CurrentRunRetryAttempt] VARCHAR(256) NULL,
	[CurrentRunStatus] VARCHAR(256) NULL,
    [DateCreated] DATETIME2 NULL, 
    [DateLastModified] DATETIME2 NULL, 
    [Description] VARCHAR(256) NULL,
	[HasSchedule] BIT NULL DEFAULT 0,
	[HasServer] BIT NULL DEFAULT 0,
	[HasStep] BIT NULL DEFAULT 0,
	[IsEnabled] BIT NULL DEFAULT 0,
    [JobType] VARCHAR(256) NULL,
    [LastRunDate] DATETIME2 NULL, 
	[LastRunOutcome] VARCHAR(256) NULL,
    [NextRunDate] DATETIME2 NULL, 
	[NextScheduleRunId] INT NULL,
	[OperatorsEmail] VARCHAR(256) NULL,
	[OriginatorsEmail] VARCHAR(256) NULL,
	[OwnerLoginName] VARCHAR(256) NULL,
	[StartStepID] INT NULL,
	[VersionNumber] VARCHAR(256) NULL,
    
	[ExpandJobSteps] BIT NULL DEFAULT 0, 

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_JSJobs] PRIMARY KEY NONCLUSTERED ([ID]),
	CONSTRAINT [FK_JSJobs_JobServers] FOREIGN KEY ([JobServer_Id]) REFERENCES [dbo].[JobServers] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [FK_JSJobs_Instances] FOREIGN KEY ([Instance_Id]) REFERENCES [dbo].[Instances] ([ID])
)

GO

CREATE INDEX [IX_JSJobs_Instance_ID] ON [dbo].[JSJobs] ([Instance_ID])

GO

CREATE INDEX [IX_JSJobs_JobServer_ID] ON [dbo].[JSJobs] ([JobServer_ID])