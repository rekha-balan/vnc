$currentDirectory = $PSScriptRoot

cd $currentDirectory

"Pushing new version of the SignalRServerHub to the Public folder"

$destinationServer = "C:\Public\SignalRServerHub"
 
$sourcePathServer = ".\SignalRServerHub\bin\Release\*"

Copy-Item -Path $sourcePathServer -Destination  $destinationServer