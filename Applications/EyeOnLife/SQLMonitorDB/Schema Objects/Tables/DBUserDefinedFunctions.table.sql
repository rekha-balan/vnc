CREATE TABLE [dbo].[DBUserDefinedFunctions]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [Database_ID] UNIQUEIDENTIFIER NULL, 

    [Name_UserDefinedFunction] VARCHAR(256) NULL, 
    [CreateDate]       DATETIME         NULL,
    [DateLastModified] DATETIME         NULL,
    [IsSystemObject]   BIT     NULL,

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    [NotFound] BIT NULL DEFAULT 0, 

    CONSTRAINT [PK_DBUserDefinedFunctions] PRIMARY KEY NONCLUSTERED ([ID]),
    CONSTRAINT [FK_DBUserDefinedFunctions_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_DBUserDefinedFunctions_Database_ID] ON [dbo].[DBUserDefinedFunctions] ([Database_ID])
