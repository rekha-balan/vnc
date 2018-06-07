EXECUTE sp_addrolemember @rolename = N'db_owner', @membername = N'DEVLIFEINT\TechSvcSQLAdmin';


GO
EXECUTE sp_addrolemember @rolename = N'db_datareader', @membername = N'PACIFICMUTUAL\WADG_SQL_TechServices';

