$gatherInformation = {
    @{
        Date = Get-Date
        FreeSpace = (Get-PSDrive c).Free
        PageFaults = (Get-WmiObject Win32_PerfRawData_PerfOS_Memory).PageFaultsPerSec
        TopCPU = Get-Process | sort CPU -desc | select -First 5 | format-list
        TopWS = Get-Process | sort -desc WS | select -First 5 | Format-List
    }
}

#Invoke-Command  vncdc1 $gatherInformation
Get-Content serverlist.txt
Invoke-Command (Get-Content serverlist.txt) $gatherInformation

$gatherInfo2 = {
    @{
        Date = Get-Date
        Host = $env:COMPUTERNAME
    }
}

Invoke-Command vncdc1 $gatherInfo2

Invoke-Command vncdc1, vncdc2 $gatherInfo2

Invoke-Command (Get-Content serverlist.txt) $gatherInfo2

Write-Host (Get-Content serverlist.txt)

$servers = Import-CSV serverlist.csv |
    where { $_.Day -eq (Get-Date).DayOfWeek } |
    foreach { $_.Name }

Invoke-Command $servers $gatherInfo2
