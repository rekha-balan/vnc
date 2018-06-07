CREATE TABLE [dbo].[Servers] (
    [ID]   UNIQUEIDENTIFIER NOT NULL,
    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL, 
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
    [DefaultInstanceExpandMask] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_Servers] PRIMARY KEY ([ID])
);


