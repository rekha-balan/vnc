CREATE TABLE [dbo].[DBDataFileInfo]
(
	[ID] int NOT NULL, 
    [DBDataFile_ID] UNIQUEIDENTIFIER NULL, 
    [Database_ID] UNIQUEIDENTIFIER NULL, 
    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
	[AvailableSpace] FLOAT NULL,
    [BytesReadFromDisk] BIGINT NULL, 
    [BytesWrittenToDisk] BIGINT NULL, 
    [MaxSize] FLOAT NULL, 
    [NumberOfDiskReads] BIGINT NULL, 
    [NumberOfDiskWrites] BIGINT NULL, 
    [Size] FLOAT NULL, 
    [UsedSpace] FLOAT NULL, 
    [VolumeFreeSpace] BIGINT NULL, 
    CONSTRAINT [PK_DBDataFileInfo] PRIMARY KEY ([ID]) ,
    CONSTRAINT [FK_DBDataFileInfo_DBDataFiles] FOREIGN KEY ([DBDataFile_ID]) REFERENCES [dbo].[DBDataFiles]([ID])
)
