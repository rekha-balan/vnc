CREATE TABLE [dbo].[ServerRoles]
(
	[ID] UNIQUEIDENTIFIER NOT NULL , 
    [Instance_ID] UNIQUEIDENTIFIER NULL,
    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    [Name_ServerRole] VARCHAR(256) NULL, 
    [CreateDate]       DATETIME         NULL,
    [DateLastModified] DATETIME         NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_Roles_Instances] FOREIGN KEY ([Instance_ID]) REFERENCES [dbo].[Instances] ([ID])
)
