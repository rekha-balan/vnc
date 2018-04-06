$ComputerName = 'A097912X0P'
$SsdbName = 'SQL2008'
 
[void][System.Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.SQLWMIManagement')
 
$smoComputer = New-Object 'Microsoft.SqlServer.Management.Smo.WMI.ManagedComputer' $ComputerName
 if ($SsdbName -eq '.') { # Default database instance
   $smoTcp = $smoComputer.ServerInstances['MSSQLSERVER'].ServerProtocols['Tcp']
 }
 else {  # Named database instance
   $smoTcp = $smoComputer.ServerInstances[$SsdbName].ServerProtocols['Tcp']
 }
 if ($smoTcp.IPAddresses['IPAll'].IPAddressProperties['TcpDynamicPorts'].Value -eq '') {
   [bool]$smoTcpIsDynamic = $false
   [int]$smoTcpPortNumber = $smoTcp.IPAddresses['IPAll'].IPAddressProperties['TcpPort'].Value
 }
 else {
   [bool]$smoTcpIsDynamic = $true
   [int]$smoTcpPortNumber = $smoTcp.IPAddresses['IPAll'].IPAddressProperties['TcpDynamicPorts'].Value
 }
 
"ssdb_tcp_port = $smoTcpPortNumber; ssdb_tcp_is_dynamic = $smoTcpIsDynamic"
 