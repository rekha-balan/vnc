ALTER TABLE [dbo].[Logins]
    ADD CONSTRAINT [FK_Logins_Instances] FOREIGN KEY ([Instance_ID]) REFERENCES [dbo].[Instances] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

