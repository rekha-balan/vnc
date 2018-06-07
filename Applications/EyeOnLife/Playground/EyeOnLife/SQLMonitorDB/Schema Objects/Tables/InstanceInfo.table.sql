CREATE TABLE [dbo].[InstanceInfo]
(
	[ID] int NOT NULL, 
	[Instance_ID] UNIQUEIDENTIFIER NOT NULL, 
    [SnapShotDate] DATETIME NOT NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    [PhysicalMemory] INT NULL, 
    [Processors] INT NULL, 
    CONSTRAINT [PK_InstanceInfo] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_InstanceInfo_Instances] FOREIGN KEY ([Instance_ID]) REFERENCES [dbo].Instances (ID)
)
