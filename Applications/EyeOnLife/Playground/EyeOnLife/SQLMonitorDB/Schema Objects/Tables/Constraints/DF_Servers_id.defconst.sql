ALTER TABLE [dbo].[Servers]
    ADD CONSTRAINT [DF_Servers_id] DEFAULT (newid()) FOR [ID];

