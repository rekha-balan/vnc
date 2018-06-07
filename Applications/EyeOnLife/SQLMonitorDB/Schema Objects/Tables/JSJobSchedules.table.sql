CREATE TABLE [dbo].[JSJobSchedules]
(
	[ID] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    --[JobServer_ID] UNIQUEIDENTIFIER NULL, 
    [JSJob_ID] UNIQUEIDENTIFIER NULL, 
    [JSSharedSchedule_ID] UNIQUEIDENTIFIER NULL, 
	[Name_JSJobSchedule] VARCHAR(256) NULL, 

    [ActiveEndDate] DATETIME2 NULL, 
    [ActiveEndTimeOfDay] TIME NULL, 
    [ActiveStartDate] DATETIME2 NULL, 
    [ActiveStartTimeOfDay] TIME NULL, 
    [DateCreated] DATETIME2 NULL, 
    [FrequencyTypes] VARCHAR(50) NULL, 
    [FrequencyInterval] VARCHAR(50) NULL, 
    [FrequencyRecurrenceFactor] VARCHAR(50) NULL, 
    [FrequencyRelativeIntervals] VARCHAR(50) NULL, 
    [FrequencySubDayInterval] VARCHAR(50) NULL, 
    [FrequencySubDayTypes] VARCHAR(50) NULL, 
    [ID_JobSchedule] INT NULL, 
    [IsEnabled] BIT NULL, 
    [JobCount] INT NULL, 
	
	[SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_JSJobSchedules] PRIMARY KEY NONCLUSTERED ([ID]),
	CONSTRAINT [FK_JSJobSchedules_JSJobs] FOREIGN KEY ([JSJob_ID]) REFERENCES [dbo].[JSJobs] ([ID]),
	CONSTRAINT [FK_JSJobSchedules_JSSharedSchedules] FOREIGN KEY ([JSSharedSchedule_ID]) REFERENCES [dbo].[JSSharedSchedules] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_JSJobSchedules_JSJob_ID] ON [dbo].[JSJobSchedules] ([JSJob_ID])

GO

CREATE INDEX [IX_JSJobSchedules_JSSharedSchedule_ID] ON [dbo].[JSJobSchedules] ([JSSharedSchedule_ID])