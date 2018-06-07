ALTER TABLE [dbo].[JSTargetServerGroups]
    ADD CONSTRAINT [FK_JSTargetServerGroups_JobServers] FOREIGN KEY ([JobServer_ID]) REFERENCES [dbo].[JobServers] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

