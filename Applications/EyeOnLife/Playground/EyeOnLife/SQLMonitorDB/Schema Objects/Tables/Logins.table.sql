CREATE TABLE [dbo].[Logins]
(
	[ID] UNIQUEIDENTIFIER NOT NULL , 
    [Instance_ID] UNIQUEIDENTIFIER NULL,
    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    [Name_Login] VARCHAR(256) NULL, 
    [CreateDate] DATETIME NULL, 
    [DateLastModified] DATETIME NULL, 
    [DefaultDatabase] VARCHAR(256) NULL, 
    [ID_Login] INT NULL, 
    [IsSystemObject] BIT NULL, 
    [LoginType] VARCHAR(50) NULL, 
    CONSTRAINT [PK_Logins] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_Logins_Instances] FOREIGN KEY ([Instance_ID]) REFERENCES [dbo].[Instances] (ID)
)
