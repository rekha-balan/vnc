CREATE TABLE [dbo].[TBTriggers]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    [DBTable_ID] UNIQUEIDENTIFIER NULL, 
    [Name_Trigger] VARCHAR(256) NULL, 
    [CreateDate]       DATETIME         NULL,
    [DateLastModified] DATETIME         NULL,
    CONSTRAINT [PK_TBTriggers] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_TBTriggers_Tables] FOREIGN KEY ([DBTable_ID]) REFERENCES [dbo].[DBTables] ([ID])
);
