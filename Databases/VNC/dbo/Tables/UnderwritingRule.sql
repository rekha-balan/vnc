CREATE TABLE [dbo].[UnderwritingRule] (
    [UnderwritingRuleID] INT            NOT NULL,
    [RuleName]           NVARCHAR (50)  NULL,
    [ShortDescription]   NVARCHAR (256) NULL,
    [EffectiveDate]      DATETIME       NULL,
    [ExpirationDate]     DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([UnderwritingRuleID] ASC)
);

