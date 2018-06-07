CREATE TABLE [dbo].[ApplicationUsage]
(
    [Application] VARCHAR(50) NOT NULL,
    [EventDate] DATETIME2 NULL, 
	[User] VARCHAR(50) NULL, 
    [EventMessage] VARCHAR(256) NULL
)
