CREATE TABLE [dbo].[ServerDdlTriggers]
(
	[ID] UNIQUEIDENTIFIER NOT NULL , 
    [Instance_ID] UNIQUEIDENTIFIER NULL,
	[Name_Instance] VARCHAR(256) NULL,

    [Name_ServerDdlTrigger] VARCHAR(256) NULL, 

    [CreateDate]       DATETIME         NULL,
    [DateLastModified] DATETIME         NULL,

	[IsSystemObject] BIT NULL, 

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL, 

    CONSTRAINT [PK_ServerDdlTrigger] PRIMARY KEY NONCLUSTERED ([ID]),
	CONSTRAINT [FK_Triggers_Instances] FOREIGN KEY ([Instance_ID]) REFERENCES [dbo].[Instances] ([ID]) ON DELETE CASCADE
)

GO

CREATE INDEX [IX_ServerDdlTriggers_Instance_ID] ON [dbo].[ServerDdlTriggers] ([Instance_ID])
