CREATE TABLE [dbo].[Servers] (
    [ID]   UNIQUEIDENTIFIER NOT NULL,

    [NetName] VARCHAR (50)     NOT NULL,
	[IPAddress] VARCHAR(20),
	[State] VARCHAR (50),
	[OSVersion] VARCHAR (50),
	[PhysicalMemory] INT,
	[Processors] INT,
	[Platform] VARCHAR (50),
	[ADDomain] VARCHAR (50),
	[Environment] VARCHAR (50),
	[SecurityZone] VARCHAR (50), 
    [DefaultInstanceExpandMask] INT NULL DEFAULT 0, 
	[Notes] VARCHAR(512) NULL,

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL, 

    CONSTRAINT [PK_Servers] PRIMARY KEY NONCLUSTERED ([ID])
);



GO

CREATE INDEX [IX_Servers_NetName] ON [dbo].[Servers] ([NetName])
