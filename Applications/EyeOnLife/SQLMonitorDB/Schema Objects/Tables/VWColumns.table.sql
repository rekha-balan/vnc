CREATE TABLE [dbo].[VWColumns]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [DBView_ID] UNIQUEIDENTIFIER NULL,
    [Name_Column] VARCHAR(256) NULL, 

    [DataType] VARCHAR(50) NULL, 
    [Identity] BIT NULL, 
    [IsForeignKey] BIT NULL, 
    [Nullable] BIT NULL, 

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_VWColumns] PRIMARY KEY NONCLUSTERED ([ID]),
    CONSTRAINT [FK_VWColumns_DBViews] FOREIGN KEY ([DBView_ID]) REFERENCES [dbo].[DBViews] ([ID]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_VWColumns_DBView_ID] ON [dbo].[VWColumns] ([DBView_ID])
