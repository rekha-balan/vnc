CREATE TABLE [dbo].[Instances]
(
	[ID] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [Server_ID] UNIQUEIDENTIFIER NULL, 
	[Name_Instance] VARCHAR(256) NOT NULL,
	[Port] INT NULL,

	[ADDomain] VARCHAR (50),
	[Environment] VARCHAR (50),
	[SecurityZone] VARCHAR (50), 

	[BrowserStartMode] VARCHAR(50) NULL,
    [Collation] VARCHAR(50) NULL, 
    [Edition] VARCHAR(50) NULL, 
    [EngineEdition] VARCHAR(50) NULL, 
    [IsClustered] BIT NULL, 
    [NetName] VARCHAR(50) NULL, 
    [OSVersion] VARCHAR(50) NULL,
	[PerfMonMode] VARCHAR(20) NULL,
    [PhysicalMemory] INT NULL, 
    [Platform] VARCHAR(50) NULL, 
    [Processors] INT NULL, 
    [Product] VARCHAR(50) NULL, 
    [ProductLevel] VARCHAR(50) NULL, 
    [ServiceInstanceId] VARCHAR(50) NULL, 
    [ServiceName] VARCHAR(50) NULL, 
    [ServiceAccount] VARCHAR(50) NULL, 
	[Status] VARCHAR(20) NULL,
    [Version] VARCHAR(50) NULL, 

    [IsMonitored] BIT NULL DEFAULT 0, 
    [ExpandContent] BIT NULL DEFAULT 0, 
    [ExpandStorage] BIT NULL DEFAULT 0, 
    [ExpandJobServer] BIT NULL DEFAULT 0, 
    [ExpandLinkedServers] BIT NULL DEFAULT 0,
	[ExpandLogins] BIT NULL DEFAULT 0,
    [ExpandServerRoles] BIT NULL DEFAULT 0,
    [ExpandTriggers] BIT NULL DEFAULT 0,
    [DefaultDatabaseExpandMask] INT NULL DEFAULT 0, 
    [DefaultJobServerExpandMask] INT NULL DEFAULT 0,

	[Notes] VARCHAR(512) NULL,

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL, 
	[SnapShotDuration] FLOAT NULL,

	[Total_DataSpaceUsage] FLOAT NULL,
	[Total_IndexSpaceUsage] FLOAT NULL,
	[Total_Size] FLOAT NULL,
	[Total_SpaceAvailable] FLOAT NULL,

    CONSTRAINT [PK_Instances] PRIMARY KEY NONCLUSTERED ([ID]),
	CONSTRAINT [FK_Instances_Servers] FOREIGN KEY ([Server_ID]) REFERENCES [dbo].[Servers] ([id]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_Instances_Server_ID] ON [dbo].[Instances] ([Server_ID])

GO

CREATE INDEX [IX_Instances_Name_Instance] ON [dbo].[Instances] ([Name_Instance])

GO