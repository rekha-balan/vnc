CREATE TABLE [dbo].[UnderwritingRuleDetail]
(
	[UnderwritingRuleDetailID] INT NOT NULL PRIMARY KEY, 
    [UnderwritingRuleID] INT NULL, 
    [LoanCodeID] INT NULL, 
    [Min] MONEY NULL, 
    [Max] MONEY NULL, 
    [Sequence] INT NULL
)
