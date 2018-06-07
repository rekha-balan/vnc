CREATE TABLE [dbo].[DBDdlTriggers]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    [Database_ID] UNIQUEIDENTIFIER NULL, 
    [Name_Trigger] VARCHAR(256) NULL, 
    [CreateDate]       DATETIME         NULL,
    [DateLastModified] DATETIME         NULL,
    CONSTRAINT [PK_DBDdlTriggers] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_DBDdlTriggers_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID])
)
