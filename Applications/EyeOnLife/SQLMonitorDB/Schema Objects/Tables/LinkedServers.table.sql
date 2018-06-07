CREATE TABLE [dbo].[LinkedServers]
(
	[ID] UNIQUEIDENTIFIER NOT NULL , 
    [Instance_ID] UNIQUEIDENTIFIER NULL,
	[Name_Instance] VARCHAR(256) NULL,

    [Name_LinkedServer] VARCHAR(256) NULL, 
	[Catalog] VARCHAR(256) NULL,
	[DataAccess] BIT NULL,
	[DataSource] VARCHAR(256) NULL,
	
    [DateLastModified] DATETIME NULL,
	[ID_LinkedServer] int NULL,
	[IsPromotionofDistributedTransactionsForRPCEnabled] BIT NULL DEFAULT 0,
	[Location] VARCHAR(256) NULL,
	[ProductName] VARCHAR(256) NULL,
	[ProviderName] VARCHAR(256) NULL,
	[ProviderString] VARCHAR(256) NULL,
	[Publisher] BIT NULL DEFAULT 0,

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_LinkedServers] PRIMARY KEY NONCLUSTERED ([ID]),
	CONSTRAINT [FK_LinkedServers_Instances] FOREIGN KEY ([Instance_ID]) REFERENCES [dbo].[Instances] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_LinkedServers_Instance_ID] ON [dbo].[LinkedServers] ([Instance_ID])
