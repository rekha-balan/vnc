CREATE TABLE [dbo].[MaritalStatus] (
    [MaritalStatusID] SMALLINT       NOT NULL,
    [MaritalStatus]   NVARCHAR (50)  NULL,
    [Description]     NVARCHAR (256) NULL,
    PRIMARY KEY CLUSTERED ([MaritalStatusID] ASC)
);

