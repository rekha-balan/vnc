ALTER TABLE [dbo].[ServerRoles]
    ADD CONSTRAINT [FK_Roles_Instances] FOREIGN KEY ([Instance_ID]) REFERENCES [dbo].[Instances] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

