CREATE TABLE [dbo].[DBUserDefinedFunctions]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    [Database_ID] UNIQUEIDENTIFIER NULL, 
    [Name_UserDefinedFunction] VARCHAR(256) NULL, 
    [CreateDate]       DATETIME         NULL,
    [DateLastModified] DATETIME         NULL,
    [IsSystemObject]   BIT     NULL,
    CONSTRAINT [PK_DBUserDefinedFunctions] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_DBUserDefinedFunctions_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID])
)
