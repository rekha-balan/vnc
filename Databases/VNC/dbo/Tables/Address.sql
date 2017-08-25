CREATE TABLE [dbo].[Address] (
    [AddressID] INT            NOT NULL,
    [Address1]  NVARCHAR (256) NULL,
    [Address2]  NVARCHAR (256) NULL,
    [Address3]  NVARCHAR (256) NULL,
    [City]      NVARCHAR (100) NULL,
    [StateID]   INT            NULL,
    [Country]   NVARCHAR (100) NULL,
    [ZipCode]   NVARCHAR (15)  NULL,
    PRIMARY KEY CLUSTERED ([AddressID] ASC),
    CONSTRAINT [FK_Address_ToState] FOREIGN KEY ([StateID]) REFERENCES [dbo].[StateLookup] ([StateID])
);

