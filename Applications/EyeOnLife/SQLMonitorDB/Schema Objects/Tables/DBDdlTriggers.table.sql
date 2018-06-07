CREATE TABLE [dbo].[DBDdlTriggers]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [Database_ID] UNIQUEIDENTIFIER NULL, 

    [Name_Trigger] VARCHAR(256) NULL, 
    [CreateDate]       DATETIME         NULL,
    [DateLastModified] DATETIME         NULL,

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    [NotFound] BIT NULL DEFAULT 0, 

    CONSTRAINT [PK_DBDdlTriggers] PRIMARY KEY NONCLUSTERED ([ID]),
    CONSTRAINT [FK_DBDdlTriggers_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_DBDdlTriggers_Database_ID] ON [dbo].[DBDdlTriggers] ([Database_ID])
