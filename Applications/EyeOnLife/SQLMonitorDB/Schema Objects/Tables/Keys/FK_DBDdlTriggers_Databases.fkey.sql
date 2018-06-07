ALTER TABLE [dbo].[DBDdlTriggers]
    ADD CONSTRAINT [FK_DBDdlTriggers_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

