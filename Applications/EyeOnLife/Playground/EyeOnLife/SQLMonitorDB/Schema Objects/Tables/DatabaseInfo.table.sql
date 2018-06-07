CREATE TABLE [dbo].[DatabaseInfo]
(
	[ID] int NOT NULL, 
    [SnapShotDate] DATETIME NOT NULL, 
    [SnapShotError] VARCHAR(256) NULL, 
	[Database_ID] UNIQUEIDENTIFIER NOT NULL, 
    [Instance_ID] UNIQUEIDENTIFIER NOT NULL, 
    [IndexSpaceUsage] FLOAT NULL, 
    [DataSpaceUsage] FLOAT NULL, 
    [Size] FLOAT NULL, 
    [SpaceAvailable] FLOAT NULL, 
    CONSTRAINT [PK_DatabaseInfo] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_DatbaseInfo_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].Databases (ID),
	CONSTRAINT [FK_DatabaseInfo_Instances] FOREIGN KEY ([Instance_ID]) REFERENCES [dbo].Instances (ID)
)
