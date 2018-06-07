ALTER TABLE [dbo].[JSSharedSchedules]
    ADD CONSTRAINT [FK_JSSharedSchedules_JobServers] FOREIGN KEY ([JobServer_ID]) REFERENCES [dbo].[JobServers] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

