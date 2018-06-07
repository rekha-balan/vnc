CREATE TABLE dbo.[LKUP_Environments]

(
	[ID] INT NOT NULL PRIMARY KEY, 
    [EnvironmentName] VARCHAR(50) NOT NULL, 
    [ADDomain_ID] INT NULL, 
    [IPBase] VARCHAR(15) NULL, 
    [IPMask] VARCHAR(15) NULL, 
    [SecurityZone] VARCHAR(20) NULL, 
    CONSTRAINT [FK_Environments_ADDomains] FOREIGN KEY ([ADDomain_ID]) REFERENCES [dbo].[LKUP_ADDomains] (ID)
)
