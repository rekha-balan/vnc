﻿ALTER TABLE [dbo].[DBDataFiles]
    ADD CONSTRAINT [FK_DBDataFiles_FileGroups] FOREIGN KEY ([FileGroup_ID]) REFERENCES [dbo].[DBFileGroups] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

