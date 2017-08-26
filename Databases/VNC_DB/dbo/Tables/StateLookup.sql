CREATE TABLE [dbo].[StateLookup]
(
	[StateID] INT NOT NULL PRIMARY KEY, 
    [StateName] NVARCHAR(50) NOT NULL, 
    [StateAbbrev] NVARCHAR(5) NOT NULL, 
    [SalesTax] FLOAT NULL
)
