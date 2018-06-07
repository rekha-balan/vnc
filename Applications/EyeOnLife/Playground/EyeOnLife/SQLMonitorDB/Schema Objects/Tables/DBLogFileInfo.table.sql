CREATE TABLE [dbo].[DBLogFileInfo]
(
	[ID] int NOT NULL, 
    [DBLogFile_ID] UNIQUEIDENTIFIER NULL, 
    [Database_ID] UNIQUEIDENTIFIER NULL, 
    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
	--[AvailableSpace] INT NULL,
    [BytesReadFromDisk] BIGINT NULL, 
    [BytesWrittenToDisk] BIGINT NULL, 
    [MaxSize] FLOAT NULL, 
    [NumberOfDiskReads] BIGINT NULL, 
    [NumberOfDiskWrites] BIGINT NULL, 
    [Size] FLOAT NULL, 
    [UsedSpace] FLOAT NULL, 
    [VolumeFreeSpace] BIGINT NULL, 
    CONSTRAINT [PK_DBLogFileInfo] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_DBLogFileInfo_DBLogFiles] FOREIGN KEY ([DBLogFile_ID]) REFERENCES [dbo].[DBLogFiles] ([ID])
)
