ALTER TABLE [dbo].[JSOperatorCategories]
    ADD CONSTRAINT [FK_JSOperatorCategories_JobServers] FOREIGN KEY ([JobServer_ID]) REFERENCES [dbo].[JobServers] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

