CREATE TABLE [dbo].[StateLookup] (
    [StateID]     INT           NOT NULL,
    [StateName]   NVARCHAR (50) NOT NULL,
    [StateAbbrev] NVARCHAR (5)  NOT NULL,
    [SalesTax]    FLOAT (53)    NULL,
    PRIMARY KEY CLUSTERED ([StateID] ASC)
);

