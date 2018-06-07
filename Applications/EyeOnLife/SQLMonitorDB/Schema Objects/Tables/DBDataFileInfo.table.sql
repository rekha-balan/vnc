CREATE TABLE [dbo].[DBDataFileInfo]
(
	[ID] int NOT NULL, 
    [DBDataFile_ID] UNIQUEIDENTIFIER NULL, 
    [Database_ID] UNIQUEIDENTIFIER NULL, 

	[AvailableSpace] FLOAT NULL,
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

    CONSTRAINT [PK_DBDataFileInfo] PRIMARY KEY NONCLUSTERED ([ID]),
    CONSTRAINT [FK_DBDataFileInfo_DBDataFiles] FOREIGN KEY ([DBDataFile_ID]) REFERENCES [dbo].[DBDataFiles]([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_DBDataFileInfo_DBDataFile_ID] ON [dbo].[DBDataFileInfo] ([DBDataFile_ID])

GO

CREATE INDEX [IX_DBDataFileInfo_Database_ID] ON [dbo].[DBDataFileInfo] ([Database_ID])
