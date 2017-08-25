CREATE TABLE [dbo].[LoandCodeType] (
    [LoanCodeTypeID]   INT            NOT NULL,
    [ShortDescription] NVARCHAR (50)  NULL,
    [LongDescription]  NVARCHAR (256) NULL,
    [IsRange]          BIT            NULL,
    PRIMARY KEY CLUSTERED ([LoanCodeTypeID] ASC)
);

