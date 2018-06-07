CREATE TABLE [dbo].[DBUserDefinedFunctions]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [SnapShotDate] DATETIME NULL, 
    [Database_ID] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [PK_DBUserDefinedFunctions] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_DBUserDefinedFunctions_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID])
)
