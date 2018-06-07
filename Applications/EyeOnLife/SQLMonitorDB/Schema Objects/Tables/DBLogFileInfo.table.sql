CREATE TABLE [dbo].[DBLogFileInfo]
(
	[ID] int NOT NULL, 
    [DBLogFile_ID] UNIQUEIDENTIFIER NULL, 
    [Database_ID] UNIQUEIDENTIFIER NULL, 

	--[AvailableSpace] INT NULL,
    --[BytesReadFromDisk] BIGINT NULL, 
    --[BytesWrittenToDisk] BIGINT NULL, 
    [MaxSize] FLOAT NULL, 
    --[NumberOfDiskReads] BIGINT NULL, 
    --[NumberOfDiskWrites] BIGINT NULL, 
    [Size] FLOAT NULL, 
    [UsedSpace] FLOAT NULL, 
    [VolumeFreeSpace] BIGINT NULL, 

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_DBLogFileInfo] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_DBLogFileInfo_DBLogFiles] FOREIGN KEY ([DBLogFile_ID]) REFERENCES [dbo].[DBLogFiles] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_DBLogFileInfo_DBLogFile_ID] ON [dbo].[DBLogFileInfo] ([DBLogFile_ID])

GO

CREATE INDEX [IX_DBLogFileInfo_Database_ID] ON [dbo].[DBLogFileInfo] ([Database_ID])
