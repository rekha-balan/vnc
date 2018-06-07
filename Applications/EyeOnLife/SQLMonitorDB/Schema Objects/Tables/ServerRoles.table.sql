CREATE TABLE [dbo].[ServerRoles]
(
	[ID] UNIQUEIDENTIFIER NOT NULL , 
    [Instance_ID] UNIQUEIDENTIFIER NULL,
	[Name_Instance] VARCHAR(256) NULL,

    [Name_ServerRole] VARCHAR(256) NULL, 
    [CreateDate]       DATETIME         NULL,
    [DateLastModified] DATETIME         NULL,

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL, 

    CONSTRAINT [PK_ServerRoles] PRIMARY KEY NONCLUSTERED ([ID]),
	CONSTRAINT [FK_Roles_Instances] FOREIGN KEY ([Instance_ID]) REFERENCES [dbo].[Instances] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_ServerRoles_Instance_ID] ON [dbo].[ServerRoles] ([Instance_ID])
