ALTER TABLE [dbo].[DBUserDefinedFunctions]
    ADD CONSTRAINT [FK_DBUserDefinedFunctions_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

