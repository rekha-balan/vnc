ALTER TABLE [dbo].[JSJobSchedules]
    ADD CONSTRAINT [FK_JSJobSchedules_JSSharedSchedules] FOREIGN KEY ([JSSharedSchedule_ID]) REFERENCES [dbo].[JSSharedSchedules] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

