﻿CREATE TABLE [dbo].[LKUP_ADDomains]
(
	[ID] INT NOT NULL PRIMARY KEY, 
    [ADDomainName] VARCHAR(50) NOT NULL, 
    [DC1_HostName] VARCHAR(50) NULL, 
    [DC1_IPAddress] VARCHAR(20) NULL, 
    [DC2_HostName] VARCHAR(50) NULL, 
    [DC2_IPAddress] VARCHAR(20) NULL
)
