CREATE TABLE [dbo].[VWTriggers]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [DBView_ID] UNIQUEIDENTIFIER NULL, 

    [Name_Trigger] VARCHAR(256) NULL, 
    [CreateDate]       DATETIME         NULL,
    [DateLastModified] DATETIME         NULL,

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_VWTriggers] PRIMARY KEY NONCLUSTERED ([ID]),
    CONSTRAINT [FK_VWTriggers_DBViews] FOREIGN KEY ([DBView_ID]) REFERENCES [dbo].[DBViews] ([ID]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_VWTriggers_DBView_ID] ON [dbo].[VWTriggers] ([DBView_ID])
