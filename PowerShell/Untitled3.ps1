$Ap1 = Get-Item IIS:\AppPools\FooBar
$ap2 = Get-Item IIS:\AppPools\FooBar2C
$ap3 = Get-Item IIS:\AppPools\DefaultAppPool

$ap1.name
$ap2.name

Compare-Object $ap1.name $ap2.name

$ap1 | Export-Clixml C:\temp\ap1.xml
$ap2 | Export-Clixml C:\temp\ap2.xml
$ap3 | Export-Clixml C:

Compare-Object (Import-Clixml C:\temp\ap1.xml) (Import-Clixml C:\temp\ap2.xml) -Property processModel.identityType, processModel.userName

