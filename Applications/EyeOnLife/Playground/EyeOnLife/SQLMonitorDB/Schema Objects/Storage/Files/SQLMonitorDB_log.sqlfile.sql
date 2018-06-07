ALTER DATABASE [$(DatabaseName)]
    ADD LOG FILE (NAME = [SQLMonitorDB_log], FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10.SQL2008\MSSQL\DATA\SQLMonitorDB_log.ldf', SIZE = 1024 KB, MAXSIZE = 2097152 MB, FILEGROWTH = 10 %);

