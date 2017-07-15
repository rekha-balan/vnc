CREATE TABLE [dbo].[GreetingRule]
(
	[GreetingRuleID] INT NOT NULL PRIMARY KEY, 
    [HourMin] INT NULL, 
    [HourMax] INT NULL, 
    [GenderID] SMALLINT NULL, 
    [MaritalStatusID] SMALLINT NULL, 
    [Greeting] NVARCHAR(256) NULL
)
