CREATE TABLE [dbo].[VWTriggers]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    [DBView_ID] UNIQUEIDENTIFIER NULL, 
    [Name_Trigger] VARCHAR(256) NULL, 
    [CreateDate]       DATETIME         NULL,
    [DateLastModified] DATETIME         NULL,
    CONSTRAINT [PK_VWTriggers] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_VWTriggers_Tables] FOREIGN KEY ([DBView_ID]) REFERENCES [dbo].[DBViews] ([ID])
);
