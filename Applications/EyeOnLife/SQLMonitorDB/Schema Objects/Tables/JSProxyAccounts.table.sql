CREATE TABLE [dbo].[JSProxyAccounts]
(
	[ID] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [JobServer_ID] UNIQUEIDENTIFIER NULL, 

	[Name_JSProxyAccount] VARCHAR(256) NULL,

	[CredentialID] INT NULL,
	[CredentialIdentity] VARCHAR(256) NULL,
	[CredentialName] VARCHAR(256) NULL,
	[Description] VARCHAR(256) NULL,
	[ID_ProxyAccount] INT NULL,
	[IsEnabled] BIT NULL DEFAULT 0,

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_JSProxyAccounts] PRIMARY KEY NONCLUSTERED ([ID]),
	CONSTRAINT [FK_JSProxyAccounts_JobServers] FOREIGN KEY ([JobServer_ID]) REFERENCES [dbo].[JobServers] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_JSProxyAccounts_JobServer_ID] ON [dbo].[JSProxyAccounts] ([JobServer_ID])