# This script is designed to be ran as a post-build step.
# If running directly, make sure you're in the root of your project's folder.

# This project also relies on the Microsoft.AspNet.SignalR.Utils package to be
# installed in your project, and packages restored prior to running this script

# Modify the variables below to correspond with your build settings
$packagesFolder = "..\packages" # Your NuGet packages
$appConfigFile = "web.config"   # Your app/web.config to grab binding redirects from
$outputDir = "bin\debug"        # The build output directory containing your SignalR hubs
$proxyOutputPath = "bin\debug"  # The folder/file to save the generated proxy to


# Script logic begins
$ErrorActionPreference = "Stop"
function New-TemporaryDirectory {
    $parent = [System.IO.Path]::GetTempPath()
    [string] $name = [System.Guid]::NewGuid()
    New-Item -ItemType Directory -Path (Join-Path $parent $name)
}

# Create a temp directory
$tempPath = New-TemporaryDirectory

# Copy Signalr.exe to temp directory, along with config file
$utilsDir = ls -Path $packagesFolder -Filter Microsoft.AspNet.SignalR.Utils.* | Select-Object -First 1
$signalr = ls -Path $utilsDir.FullName -Filter Signalr.exe -Recurse
cp $signalr.FullName $tempPath

# Strip the binding redirects out of our app/web.config file, and put them in signalr.exe.config
$appConfig = [xml] (Get-Content $appConfigFile)
$signalrConfig = [xml] "<?xml version='1.0' encoding='utf-8'?><configuration></configuration>"
$runtimeNode = $signalrConfig.ImportNode($appConfig.DocumentElement.runtime, $true)
$signalrConfig.DocumentElement.AppendChild($runtimeNode)
$signalrConfig.Save("$($tempPath)\signalr.exe.config")

# Generate the proxies
$currentLocation = Get-Location
pushd $tempPath.FullName
./signalr.exe ghp "/path:$($currentLocation)\$($outputDir)"
$exitCode = $LASTEXITCODE
if ($exitCode -ne 0) {
    popd
    Write-Output "Signalr.exe returned an error, exiting post-build script!"
    
    # Clean up the temp path
    rm -Recurse -Force $tempPath.FullName
    
    exit $exitCode
}
popd

# Move the script to the destination
mv -Force "$($tempPath.FullName)\server.js" $proxyOutputPath

# Clean up the temp path
rm -Recurse -Force $tempPath.FullName