CREATE TABLE [dbo].[Gender] (
    [GenderID]    SMALLINT       NOT NULL,
    [Gender]      NVARCHAR (20)  NULL,
    [Description] NVARCHAR (256) NULL,
    PRIMARY KEY CLUSTERED ([GenderID] ASC)
);

