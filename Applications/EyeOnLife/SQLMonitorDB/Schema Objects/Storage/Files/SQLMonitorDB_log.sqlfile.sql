ALTER DATABASE [$(DatabaseName)]
    ADD LOG FILE (NAME = [SQLMonitorDB_log], FILENAME = 'D:\Program Files\Microsoft SQL Server\MSSQL10_50.REL02\MSSQL\DATA\SQLMonitorDB_Primary.ldf', SIZE = 3480448 KB, MAXSIZE = 2097152 MB, FILEGROWTH = 10 %);



