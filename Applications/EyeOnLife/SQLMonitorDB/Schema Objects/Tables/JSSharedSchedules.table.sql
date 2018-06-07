CREATE TABLE [dbo].[JSSharedSchedules]
(
	[ID] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [JobServer_ID] UNIQUEIDENTIFIER NULL, 

	[Name_JSSharedSchedule] VARCHAR(256) NULL,

    [ActiveEndDate] DATETIME2 NULL, 
    [ActiveEndTimeOfDay] TIME NULL, 
    [ActiveStartDate] DATETIME2 NULL, 
    [ActiveStartTimeOfDay] TIME NULL, 
    [DateCreated] DATETIME2 NULL, 
    [FrequencyInterval] VARCHAR(50) NULL, 
    [FrequencyRecurrenceFactor] VARCHAR(50) NULL, 
    [FrequencyRelativeIntervals] VARCHAR(50) NULL, 
    [FreqeuencySubDayInterval] VARCHAR(50) NULL, 
    [FrequencySubDayTypes] VARCHAR(50) NULL, 
    [FrequencyTypes] VARCHAR(50) NULL, 
    [ID_JobSchedule] INT NULL, 
    [IsEnabled] BIT NULL DEFAULT 0, 
    [JobCount] INT NULL, 

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_JSSharedSchedules] PRIMARY KEY NONCLUSTERED ([ID]),
	CONSTRAINT [FK_JSSharedSchedules_JobServers] FOREIGN KEY ([JobServer_ID]) REFERENCES [dbo].[JobServers] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_JSSharedSchedules_JobServer_ID] ON [dbo].[JSSharedSchedules] ([JobServer_ID])