CREATE TABLE [dbo].[DBStoredProcedures] (
    [ID]                  UNIQUEIDENTIFIER NOT NULL,
    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    [Database_ID]         UNIQUEIDENTIFIER NULL,
    [Name_StoredProcedure] VARCHAR (256)     NULL,
    [StoredProcedure_ID]  VARCHAR (50)     NULL,
    [Owner]               VARCHAR (256)     NULL,
    [MethodName]          VARCHAR (256)     NULL,
    [TextHeader]          VARCHAR (MAX)     NULL,
    [TextBody]            VARCHAR (MAX)    NULL,
    [IsSystemObject]      BIT     NULL,
    [CreateDate]          DATETIME         NULL,
    [DateLastModified]    DATETIME         NULL,
    [RetrieveStoredProcedureContent] BIT NULL DEFAULT 0,
    CONSTRAINT [PK_DBStoredProcedures] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_DBStoredProcedures_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID])
);


