CREATE TABLE [dbo].[TBColumns]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [DBTable_ID] UNIQUEIDENTIFIER NULL,
    [Name_Column] VARCHAR(256) NULL, 

    [DataType] VARCHAR(50) NULL, 
    [Identity] BIT NULL, 
    [IsForeignKey] BIT NULL, 

    [Nullable] BIT NULL, 

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_TBColumns] PRIMARY KEY NONCLUSTERED ([ID]),
    CONSTRAINT [FK_TBColumns_DBTables] FOREIGN KEY ([DBTable_ID]) REFERENCES [dbo].[DBTables] ([ID]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_TBColumns_DBTable_ID] ON [dbo].[TBColumns] ([DBTable_ID])