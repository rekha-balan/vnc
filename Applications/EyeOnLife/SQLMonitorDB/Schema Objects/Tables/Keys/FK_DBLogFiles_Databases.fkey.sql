﻿ALTER TABLE [dbo].[DBLogFiles]
    ADD CONSTRAINT [FK_DBLogFiles_Databases] FOREIGN KEY ([Database_ID]) REFERENCES [dbo].[Databases] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

