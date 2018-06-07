ALTER TABLE [dbo].[JobServers]
    ADD CONSTRAINT [FK_JobServers_Instances] FOREIGN KEY ([Instance_ID]) REFERENCES [dbo].[Instances] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

