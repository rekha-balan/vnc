﻿ALTER TABLE [dbo].[JSJobs]
    ADD CONSTRAINT [FK_JSJobs_Instances] FOREIGN KEY ([Instance_ID]) REFERENCES [dbo].[Instances] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

