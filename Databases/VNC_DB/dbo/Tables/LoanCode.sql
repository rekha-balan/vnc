CREATE TABLE [dbo].[LoanCode]
(
	[LoanCodeID] INT NOT NULL PRIMARY KEY, 
    [LoanCodeTypeID] INT NULL, 
    [ShortDescription] NVARCHAR(50) NULL, 
    [LongDescription] NVARCHAR(256) NULL
)
