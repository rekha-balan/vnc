CREATE TABLE [dbo].[UnderwritingRuleDetail] (
    [UnderwritingRuleDetailID] INT   NOT NULL,
    [UnderwritingRuleID]       INT   NULL,
    [LoanCodeID]               INT   NULL,
    [Min]                      MONEY NULL,
    [Max]                      MONEY NULL,
    [Sequence]                 INT   NULL,
    PRIMARY KEY CLUSTERED ([UnderwritingRuleDetailID] ASC)
);

