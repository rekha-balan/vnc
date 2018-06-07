ALTER TABLE [dbo].[JSOperators]
    ADD CONSTRAINT [FK_JSOperators_JobServers] FOREIGN KEY ([JobServer_ID]) REFERENCES [dbo].[JobServers] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

