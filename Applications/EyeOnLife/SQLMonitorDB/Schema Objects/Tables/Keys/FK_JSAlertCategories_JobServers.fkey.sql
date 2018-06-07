ALTER TABLE [dbo].[JSAlertCategories]
    ADD CONSTRAINT [FK_JSAlertCategories_JobServers] FOREIGN KEY ([JobServer_ID]) REFERENCES [dbo].[JobServers] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

