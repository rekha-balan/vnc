CREATE TABLE [dbo].[ServerInfo]
(
	[ID] int NOT NULL, 
	[Server_ID] UNIQUEIDENTIFIER NOT NULL, 
    
	[SnapShotDate] DATETIME NOT NULL, 
    [SnapShotError] VARCHAR(256) NULL,

    CONSTRAINT [PK_ServerInfo] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_ServerInfo_Servers] FOREIGN KEY ([Server_ID]) REFERENCES [dbo].Servers (ID) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_ServerInfo_Server_ID] ON [dbo].[ServerInfo] ([Server_ID])
