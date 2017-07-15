CREATE TABLE [dbo].[LoandCodeType]
(
	[LoanCodeTypeID] INT NOT NULL PRIMARY KEY, 
    [ShortDescription] NVARCHAR(50) NULL, 
    [LongDescription] NVARCHAR(256) NULL, 
    [IsRange] BIT NULL
)
