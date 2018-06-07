CREATE TABLE [dbo].[DBStoredProcedures] (
    [ID]                  UNIQUEIDENTIFIER NOT NULL,
    [Database_ID]         UNIQUEIDENTIFIER NULL,

    [Name_StoredProcedure] VARCHAR (256)     NULL,
    [CreateDate]          DATETIME         NULL,
    [DateLastModified]    DATETIME         NULL,
    [IsSystemObject]      BIT     NULL,
    [MethodName]          VARCHAR (256)     NULL,
    [Owner]               VARCHAR (256)     NULL,
    [StoredProcedure_ID]  VARCHAR (50)     NULL,
    [TextBody]            VARCHAR (MAX)    NULL,
    [TextHeader]          VARCHAR (MAX)     NULL,
    [RetrieveStoredProcedureContent] BIT NULL DEFAULT 0,

    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    [NotFound] BIT NULL DEFAULT 0, 

    CONSTRAINT [PK_DBStoredProcedures] PRIMARY KEY NONCLUSTERED ([ID]),
    CONSTRAINT [FK_DBStoredProcedures_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]) ON DELETE CASCADE
);



GO

CREATE INDEX [IX_DBStoredProcedures_Database_ID] ON [dbo].[DBStoredProcedures] ([Database_ID])
