SELECT * FROM dbo.Clans
INSERT INTO dbo.clans (ClanName) values  ('VNC Nijas')
SELECT owning_principal_name, instance_pipe_name, heart_beat FROM sys.dm_os_child_instances