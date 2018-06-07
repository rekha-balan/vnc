CREATE TABLE [dbo].[CrawlerExpandSettings]
(
	[ID] INT NOT NULL PRIMARY KEY, 
	[TimePeriod] VARCHAR(256) NOT NULL,
	[TargetObject] VARCHAR(256) NOT NULL,
	[ExpandSetting] INT NOT NULL DEFAULT 0,

	[Notes] VARCHAR(512) NULL
)

GO
