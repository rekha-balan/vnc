ALTER TABLE [dbo].[JSProxyAccounts]
    ADD CONSTRAINT [FK_JSProxyAccounts_JobServers] FOREIGN KEY ([JobServer_ID]) REFERENCES [dbo].[JobServers] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

