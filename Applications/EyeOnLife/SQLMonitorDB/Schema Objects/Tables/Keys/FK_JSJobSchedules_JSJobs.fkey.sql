﻿ALTER TABLE [dbo].[JSJobSchedules]
    ADD CONSTRAINT [FK_JSJobSchedules_JSJobs] FOREIGN KEY ([JSJob_ID]) REFERENCES [dbo].[JSJobs] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

