CREATE TABLE [dbo].[DBLogFiles]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [Database_ID] UNIQUEIDENTIFIER NULL, 
    [Name_LogFile] VARCHAR(300) NULL, 

	[AvailableSpace] INT NULL,
    --[BytesReadFromDisk] BIGINT NULL, 
    --[BytesWrittenToDisk] BIGINT NULL, 
	[FileName] VARCHAR(256) NULL,
    [Growth] INT NULL, 
    [GrowthType] VARCHAR(50) NULL, 
    [MaxSize] FLOAT NULL, 
    --[NumberOfDiskReads] BIGINT NULL, 
    --[NumberOfDiskWrites] BIGINT NULL, 
    [Size] FLOAT NULL, 
    [UsedSpace] FLOAT NULL, 
    [VolumeFreeSpace] BIGINT NULL, 

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_LogFiles] PRIMARY KEY NONCLUSTERED ([ID]),
    CONSTRAINT [FK_DBLogFiles_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_DBLogFiles_Database_ID] ON [dbo].[DBLogFiles] ([Database_ID])
