CREATE TABLE [dbo].[JSOperators]
(
	[ID] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [JobServer_ID] UNIQUEIDENTIFIER NULL, 

	[Name_JSOperator] VARCHAR(256) NULL,

	[CategoryName] VARCHAR(256) NULL,
	[EmailAddress] VARCHAR(256) NULL,
	[Enabled] BIT NULL DEFAULT 0,
	[ID_Operator] INT NULL,
	[LastEmailDate] DATETIME2 NULL,
	[LastNetSendDate] DATETIME2 NULL,
	[LastPagerDate] DATETIME2 NULL,
	[NetSendAddress] VARCHAR(256) NULL,
	[PagerAddress] VARCHAR(256) NULL,
	[PagerDays] VARCHAR(256) NULL,
	[SaturdayPagerEndTime] VARCHAR(256) NULL,
	[SaturdayPagerStartTime] VARCHAR(256) NULL,
	[SundayPagerEndTime] VARCHAR(256) NULL,
	[SundayPagerStartTime] VARCHAR(256) NULL,
	[WeekdayPagerEndTime] VARCHAR(256) NULL,
	[WeekdayPagerStartTime] VARCHAR(256) NULL,

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_JSOperators] PRIMARY KEY NONCLUSTERED ([ID]),
	CONSTRAINT [FK_JSOperators_JobServers] FOREIGN KEY ([JobServer_ID]) REFERENCES [dbo].[JobServers] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_JSOperators_JobServer_ID] ON [dbo].[JSOperators] ([JobServer_ID])