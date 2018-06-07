CREATE TABLE [dbo].[Roles]
(
	[ID] UNIQUEIDENTIFIER NOT NULL , 
    [Instance_ID] UNIQUEIDENTIFIER NULL, 
    [RoleName] NCHAR(10) NULL, 
    CONSTRAINT [PK_Roles] PRIMARY KEY ([ID]),
	CONSTRAINT [FK_Roles_Instances] FOREIGN KEY ([Instance_ID]) REFERENCES [dbo].[Instances] ([ID])
)
