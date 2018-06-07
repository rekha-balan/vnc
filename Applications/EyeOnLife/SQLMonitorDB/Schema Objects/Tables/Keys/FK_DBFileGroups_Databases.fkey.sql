ALTER TABLE [dbo].[DBFileGroups]
    ADD CONSTRAINT [FK_DBFileGroups_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

