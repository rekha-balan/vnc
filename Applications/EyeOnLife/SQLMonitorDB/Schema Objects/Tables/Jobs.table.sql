CREATE TABLE [dbo].[Jobs]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [Instance_ID] UNIQUEIDENTIFIER NULL, 
    [SnapShotDate] DATETIME NULL, 
    [SnapShotError] VARCHAR(256) NULL,
    /*CONSTRAINT [PK_Jobs] PRIMARY KEY ([ID])*/
	/*CONSTRAINT [FK_Jobs_Instances] FOREIGN KEY ([Instance_Id]) REFERENCES [dbo].[Instances] ([ID])*/
)
