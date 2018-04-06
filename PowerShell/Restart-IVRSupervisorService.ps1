# TODO
# Create a common header to use for scripts

param([string] $hostname =$(throw, "Enter IVR Server Name"))

ping $hostname

# Load the assembly that contains the code we need
[void] ([Reflection.Assembly]::LoadWithPartialName("System.ServiceProcess"))

# Get the current service
$service = [System.ServiceProcess.ServiceController]::GetServices($hostname) |
	Where-Object {$_.DisplayName -eq "Syntellect Supervisor"}
	
Write-Host "Current Status" -ForegroundColor Green

$service

Write-Host
Write-Host "Stopping Service" -ForegroundColor Yellow
$service.Stop()
$service.WaitForStatus("Stopped")
Write-Host "Service Stopped" -ForegroundColor Red
Write-Host

$service

Start-Sleep -Seconds 10

Write-Host
Write-Host "Starting Service" -ForegroundColor Yellow
$service.Start()
$service.WaitForStatus("Running")
Write-Host "Service Started" -ForegroundColor Green
Write-Host

$service