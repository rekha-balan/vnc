$ping = New-Object System.Net.NetworkInformation.Ping
$i = 0
1..255 | foreach { $ip = "10.0.255.$_" 
$Res = $ping.send($ip)

if ($Res.Status -eq "Success")
{

$result = $ip + " = Success"
Write-Host $result

$i++

}


 } 
$Hosts = [string]$i + " Hosts is pingable"
Write-Host $Hosts
