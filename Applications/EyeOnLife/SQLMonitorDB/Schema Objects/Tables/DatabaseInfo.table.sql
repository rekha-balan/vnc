CREATE TABLE [dbo].[DatabaseInfo]
(
	[ID] int NOT NULL, 
	[Database_ID] UNIQUEIDENTIFIER NOT NULL, 
    [Instance_ID] UNIQUEIDENTIFIER NOT NULL, 

	[Name_Instance] VARCHAR(256) NULL,

	[Name_Database] VARCHAR(256) NULL, 

    [DataSpaceUsage] FLOAT NULL, 
    [IndexSpaceUsage] FLOAT NULL, 
    [Size] FLOAT NULL, 
    [SpaceAvailable] FLOAT NULL, 

    [SnapShotDate] DATETIME NOT NULL, 
    [SnapShotError] VARCHAR(256) NULL, 

    CONSTRAINT [PK_DatabaseInfo] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_DatbaseInfo_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].Databases (ID) ON DELETE CASCADE,
	CONSTRAINT [FK_DatabaseInfo_Instances] FOREIGN KEY ([Instance_ID]) REFERENCES [dbo].Instances (ID)
)

GO


CREATE INDEX [IX_DatabaseInfo_Database_ID] ON [dbo].[DatabaseInfo] ([Database_ID])

GO

CREATE INDEX [IX_DatabaseInfo_Instance_ID] ON [dbo].[DatabaseInfo] ([Instance_ID])
