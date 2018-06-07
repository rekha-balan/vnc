CREATE TABLE [dbo].[DBViews] (
    [ID]               UNIQUEIDENTIFIER NOT NULL,
    [Database_ID]      UNIQUEIDENTIFIER NULL,
    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    [CreateDate]       DATETIME         NULL,
    [DateLastModified] DATETIME         NULL,
    [IsSystemObject]   BIT     NULL,
    [Owner]            VARCHAR (50)     NULL,
    [View_ID]          INT     NULL,
    [Name_View]         VARCHAR (256)     NULL,
    [EP_Purpose]       VARCHAR (50)     NULL,
    CONSTRAINT [PK_DBViews] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_DBViews_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID])
);


