ALTER TABLE [dbo].[JSJobCategories]
    ADD CONSTRAINT [FK_JSJobCategories_JobServers] FOREIGN KEY ([JobServer_ID]) REFERENCES [dbo].[JobServers] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

