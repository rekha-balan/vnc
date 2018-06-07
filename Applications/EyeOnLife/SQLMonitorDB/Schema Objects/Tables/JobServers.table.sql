CREATE TABLE [dbo].[JobServers]
(
	[ID] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [Instance_ID] UNIQUEIDENTIFIER NULL, 
	[Name_Instance] VARCHAR(256) NULL,
	[Name_JobServer] VARCHAR(256) NULL,

	[ErrorLogFile] VARCHAR(256) NULL,
	[HostLoginName] VARCHAR(256) NULL,

	[MaximumHistoryRows] VARCHAR(256) NULL,
	[MaximumJobHistoryRows] VARCHAR(256) NULL,
	[MsxAccountCredentialName] VARCHAR(256) NULL,
	[MsxAccountName] VARCHAR(256) NULL,
	[MsxServerName] VARCHAR(256) NULL,

    [IsMonitored] BIT NULL DEFAULT 0,
    [ExpandAlertCategories] BIT NULL DEFAULT 0,	 
    [ExpandAlerts] BIT NULL DEFAULT 0,
    [ExpandJobCategories] BIT NULL DEFAULT 0,
    [ExpandJobs] BIT NULL DEFAULT 0,
    [ExpandOperatorCategories] BIT NULL DEFAULT 0,
    [ExpandOperators] BIT NULL DEFAULT 0,
    [ExpandProxyAccounts] BIT NULL DEFAULT 0,
    [ExpandSharedSchedules] BIT NULL DEFAULT 0,
    [ExpandTargetServerGroups] BIT NULL DEFAULT 0,
    [ExpandTargetServers] BIT NULL DEFAULT 0,

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
	[SnapShotDuration] FLOAT NULL

    CONSTRAINT [PK_JobServers] PRIMARY KEY NONCLUSTERED ([ID]),
	CONSTRAINT [FK_JobServers_Instances] FOREIGN KEY ([Instance_Id]) REFERENCES [dbo].[Instances] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_JobServers_Instance_ID] ON [dbo].[JobServers] ([Instance_ID])
