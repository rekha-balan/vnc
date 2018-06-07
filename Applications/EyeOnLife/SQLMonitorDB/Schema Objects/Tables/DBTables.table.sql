CREATE TABLE [dbo].[DBTables] (
    [ID]               UNIQUEIDENTIFIER NOT NULL,
    [Database_ID]      UNIQUEIDENTIFIER NULL,

    [Name_Table]        VARCHAR (256)    NULL,
    [CreateDate]		DATETIME         NULL,
    [DataSpaceUsed]		FLOAT     NULL,
    [DateLastModified]	DATETIME         NULL,
	[FileGroup]			VARCHAR(256) NULL,
	[HasIndex]			BIT NULL,
    [IsSystemObject]	BIT     NULL,
    [Owner]				VARCHAR (256)     NULL,
    [RowCount]			BIGINT     NULL,
    [Table_ID]			VARCHAR (50)     NULL,
    [EP_Purpose]		VARCHAR (50)     NULL,

	[ExpandColumns] BIT NULL DEFAULT 0, 

    [SnapShotDate]		DATETIME NULL, 
    [SnapShotError]		VARCHAR(256) NULL,
    [NotFound] BIT NULL DEFAULT 0, 

    CONSTRAINT [PK_DBTables] PRIMARY KEY NONCLUSTERED ([ID]),
    CONSTRAINT [FK_DBTables_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]) ON DELETE CASCADE
);



GO

CREATE INDEX [IX_DBTables_Database_ID] ON [dbo].[DBTables] ([Database_ID])
