CREATE TABLE [dbo].[DBViews] (
    [ID]               UNIQUEIDENTIFIER NOT NULL,
    [Database_ID]      UNIQUEIDENTIFIER NULL,

    [Name_View]         VARCHAR (256)     NULL,
    [CreateDate]       DATETIME         NULL,
    [DateLastModified] DATETIME         NULL,
    [IsSystemObject]   BIT     NULL,
    [Owner]            VARCHAR (50)     NULL,
    [View_ID]          INT     NULL,
    [EP_Purpose]       VARCHAR (50)     NULL,

	[ExpandColumns] BIT NULL DEFAULT 0,

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    [NotFound] BIT NULL DEFAULT 0, 

    CONSTRAINT [PK_DBViews] PRIMARY KEY NONCLUSTERED ([ID]),
    CONSTRAINT [FK_DBViews_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_DBViews_Database_ID] ON [dbo].[DBViews] ([Database_ID])
