CREATE TABLE [dbo].[DBDataFiles]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [FileGroup_ID] UNIQUEIDENTIFIER NULL, 
    [Database_ID] UNIQUEIDENTIFIER NULL,

    [Name_DataFile] VARCHAR(400) NULL, 
	[AvailableSpace] FLOAT NULL,
    --[BytesReadFromDisk] BIGINT NULL, 
    --[BytesWrittenToDisk] BIGINT NULL, 
	[FileName] VARCHAR(400) NULL,
    [Growth] INT NULL, 
    [GrowthType] VARCHAR(50) NULL, 
    [ID_DataFile] INT NULL, 
    [IsPrimaryFile] BIT NULL, 
    [MaxSize] FLOAT NULL, 
    --[NumberOfDiskReads] BIGINT NULL, 
    --[NumberOfDiskWrites] BIGINT NULL, 
    [Size] FLOAT NULL, 
    [UsedSpace] FLOAT NULL, 
    [VolumeFreeSpace] BIGINT NULL, 

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_DataFiles] PRIMARY KEY NONCLUSTERED ([ID]),
    CONSTRAINT [FK_DBDataFiles_FileGroups] FOREIGN KEY ([FileGroup_ID]) REFERENCES [dbo].[DBFileGroups] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_DBDataFiles_FileGroup_ID] ON [dbo].[DBDataFiles] ([FileGroup_ID])

GO

CREATE INDEX [IX_DBDataFiles_Database_ID] ON [dbo].[DBDataFiles] ([Database_ID])
