CREATE TABLE [dbo].[LoanCode] (
    [LoanCodeID]       INT            NOT NULL,
    [LoanCodeTypeID]   INT            NULL,
    [ShortDescription] NVARCHAR (50)  NULL,
    [LongDescription]  NVARCHAR (256) NULL,
    PRIMARY KEY CLUSTERED ([LoanCodeID] ASC)
);

