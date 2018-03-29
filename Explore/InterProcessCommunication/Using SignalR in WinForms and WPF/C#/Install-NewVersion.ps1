
$destinationClient = "C:\Public\WPFClient"
$destinationServer = "C:\Public\WPFServer"

$sourcePathClient = ".\WPFClient\bin\Release\*" 
$sourcePathServer = ".\WPFServer\bin\Release\*"

Copy-Item -Path $sourcePathClient -Destination  $destinationClient
Copy-Item -Path $sourcePathServer -Destination  $destinationServer
