CREATE TABLE [dbo].[InstanceInfo]
(
	[ID] int NOT NULL, 
	[Instance_ID] UNIQUEIDENTIFIER NOT NULL, 
	[Name_Instance] VARCHAR(256) NULL,

	[ADDomain] VARCHAR (50) NULL,
	[Environment] VARCHAR (50) NULL,
	[SecurityZone] VARCHAR (50) NULL, 

    [PhysicalMemory] INT NULL, 
    [Processors] INT NULL,
	 
    [SnapShotDate] DATETIME NOT NULL, 
    [SnapShotError] VARCHAR(256) NULL,
	[SnapShotDuration] FLOAT NULL,

	[Total_DataSpaceUsage] FLOAT NULL,
	[Total_IndexSpaceUsage] FLOAT NULL,
	[Total_Size] FLOAT NULL,
	[Total_SpaceAvailable] FLOAT NULL,

    CONSTRAINT [PK_InstanceInfo] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_InstanceInfo_Instances] FOREIGN KEY ([Instance_ID]) REFERENCES [dbo].Instances (ID) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_InstanceInfo_Instance_ID] ON [dbo].[InstanceInfo] ([Instance_ID])
