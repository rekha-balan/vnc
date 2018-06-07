CREATE TABLE [dbo].[DBLogFiles]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [Database_ID] UNIQUEIDENTIFIER NULL, 
    [Name_LogFile] VARCHAR(300) NULL, 
    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
	--[AvailableSpace] INT NULL,
    [BytesReadFromDisk] BIGINT NULL, 
    [BytesWrittenToDisk] BIGINT NULL, 
	[FileName] VARCHAR(256) NULL,
    [Growth] INT NULL, 
    [GrowthType] VARCHAR(50) NULL, 
    [ID_FileGroup] INT NULL, 
    [IsPrimaryFile] BIT NULL, 
    [MaxSize] BIGINT NULL, 
    [NumberOfDiskReads] BIGINT NULL, 
    [NumberOfDiskWrites] BIGINT NULL, 
    [Size] BIGINT NULL, 
    [UsedSpace] BIGINT NULL, 
    [VolumeFreeSpace] BIGINT NULL, 
    CONSTRAINT [PK_LogFiles] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_DBLogFiles_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID])
)
