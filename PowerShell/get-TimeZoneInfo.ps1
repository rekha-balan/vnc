# Get Time Zone on a computer and save to variable
$computer = "A097921NYP"
     write-host “Attempting to determine timezone information for $computer…” `r 
     $Timezone = Get-WMIObject -class Win32_TimeZone -ComputerName $Computer
     $TZDescription = $Timezone.Description
     $TZDaylightTime = $Timezone.DaylightName
     $TZStandardTime = $Timezone.StandardName
     write-host “$computer Timezone is set to $TZDescription” `r