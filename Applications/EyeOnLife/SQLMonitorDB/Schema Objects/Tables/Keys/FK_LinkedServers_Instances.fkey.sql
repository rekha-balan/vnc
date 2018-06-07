ALTER TABLE [dbo].[LinkedServers]
    ADD CONSTRAINT [FK_LinkedServers_Instances] FOREIGN KEY ([Instance_ID]) REFERENCES [dbo].[Instances] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

