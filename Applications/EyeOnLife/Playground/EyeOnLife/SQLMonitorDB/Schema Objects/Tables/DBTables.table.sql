CREATE TABLE [dbo].[DBTables] (
    [ID]               UNIQUEIDENTIFIER NOT NULL,
    [Database_ID]      UNIQUEIDENTIFIER NULL,
    [SnapShotDate]		DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    [Name_Table]        VARCHAR (256)    NULL,
    [CreateDate]       DATETIME         NULL,
    [DataSpaceUsed]    FLOAT     NULL,
    [DateLastModified] DATETIME         NULL,
	[FileGroup]			VARCHAR(256) NULL,
	[HasIndex]			BIT NULL,
    [IsSystemObject]   BIT     NULL,
    [Owner]            VARCHAR (256)     NULL,
    [RowCount]         BIGINT     NULL,
    [Table_ID]         VARCHAR (50)     NULL,
    [EP_Purpose]       VARCHAR (50)     NULL,
    CONSTRAINT [PK_DBTables] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_DBTables_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID])
);


