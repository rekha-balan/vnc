
$destination = "C:\Public\CCC Release"
$SourcePath = ".\bin\Release\*"

# $destinations = @(
    # "C:\Inetpub\wwwroot\MCR7-GD\bin", 
    # "C:\Inetpub\wwwroot\MCR7-GD-Test1\bin"
    # "C:\inetpub\wwwroot\MES7-GD\bin")

# $sourceFile = ".\bin\Release\VNC.Logging.CustomTraceListeners.SignalRListener.dll"

# Foreach ($file in $destinations)
# {
    # Copy-Item -Path $sourceFile -Destination $file
# }

Copy-Item -Path $sourcePath -Destination  $destination
# Copy-Item 