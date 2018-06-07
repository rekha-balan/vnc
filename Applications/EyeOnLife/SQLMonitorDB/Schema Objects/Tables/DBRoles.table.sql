CREATE TABLE [dbo].[DBRoles]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [Database_ID] UNIQUEIDENTIFIER NULL, 

    [Name_Role]         VARCHAR (256)     NULL,
    [CreateDate]       DATETIME         NULL,
    [DateLastModified] DATETIME         NULL,
    [IsFixedRole]   BIT     NULL,

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    [NotFound] BIT NULL DEFAULT 0, 

    CONSTRAINT [PK_DBRoles] PRIMARY KEY NONCLUSTERED ([ID]),
    CONSTRAINT [FK_DBRoles_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_DBRoles_Database_ID] ON [dbo].[DBRoles] ([Database_ID])
