-- Clean out the stuff from the tables.  Order matters.

delete from [TBColumns]
delete from [TBTriggers]

delete from [VWColumns]
delete from [VWTriggers]

delete from [DBDataFileInfo]
delete from [DBDataFiles]

delete from [DBFileGroups]

delete from [DBLogFileInfo]
delete from [DBLogFiles]

delete from [DBDdlTriggers]
delete from [DBRoles]
delete from [DBStoredProcedures]
delete from [DBTables]
delete from [DBUserDefinedFunctions]
delete from [DBUsers]
delete from [DBViews]

delete from [JSAlertCategories]
delete from [JSAlerts]
delete from [JSJobCategories]
delete from [JSJobs]
delete from [JSJobSchedules]
delete from [JSJobSteps]
delete from [JSOperatorCategories]
delete from [JSOperators]
delete from [JSProxyAccounts]
delete from [JSSharedSchedules]
delete from [JSTargetServerGroups]
delete from [JSTargetServers]

delete from [DatabaseInfo]
delete from [Databases]

delete from [JobServers]
delete from [LinkedServers]
delete from [Logins]
delete from [ServerRoles]

delete from [InstanceInfo]
delete from [ServerInfo]

-- Only use these if you really want to start from scratch

--delete from [Instances]
--delete from [Servers]
