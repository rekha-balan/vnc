ALTER TABLE [dbo].[JSTargetServers]
    ADD CONSTRAINT [FK_JSTargetServers_JobServers] FOREIGN KEY ([JobServer_ID]) REFERENCES [dbo].[JobServers] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

