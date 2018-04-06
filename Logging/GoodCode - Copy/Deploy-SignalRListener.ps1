$currentDirectory = $PSScriptRoot

cd $currentDirectory

$destination = "C:\Public\SignalRListener"

$signalRFiles = @(
	".\SignalRListener\bin\Release\Newtonsoft.Json.dll"
	".\SignalRListener\bin\Release\Microsoft.AspNet.SignalR.Client.dll"
	".\SignalRListener\bin\Release\VNC.Logging.CustomTraceListeners.SignalRListener.dll")
	
"Pushing new version of the SignalRListener files to the Public folder"

Foreach ($file in $signalRFiles)
{
    $file
	Copy-Item -Path $file -Destination $destination
}