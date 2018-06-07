CREATE TABLE [dbo].[TBTriggers]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [DBTable_ID] UNIQUEIDENTIFIER NULL,
	 
    [Name_Trigger] VARCHAR(256) NULL, 
    [CreateDate]       DATETIME         NULL,
    [DateLastModified] DATETIME         NULL,

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_TBTriggers] PRIMARY KEY NONCLUSTERED ([ID]),
    CONSTRAINT [FK_TBTriggers_DBTables] FOREIGN KEY ([DBTable_ID]) REFERENCES [dbo].[DBTables] ([ID]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_TBTriggers_DBTable_ID] ON [dbo].[TBTriggers] ([DBTable_ID])
