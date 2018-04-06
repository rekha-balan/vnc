$currentDirectory = $PSScriptRoot

cd $currentDirectory

"Pushing new version of the SignalRClient to the Public folder"

$destinationClient = "C:\Public\SignalRClient"

$sourcePathClient = ".\SignalRClient\bin\Release\*" 

Copy-Item -Path $sourcePathClient -Destination  $destinationClient