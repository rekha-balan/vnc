param([String] $searchPattern, $files)

# Write-Host "Inside Check-Code.ps1 Pattern >" + $searchPattern + "<"
# "Inside Check-Code.ps1 Pattern >" + $searchPattern + "<"
# $searchPattern | get-member
# $files | get-member
# exit
# $searchPattern
# $files
# exit
# $files | get-member
# foreach ($fn in Get-ChildItem $files)
# foreach ($fn in $files)
# {
# $fn
# $fn.Name
# $fn.FullName
# }

foreach ($fn in $files)
{
	# $fn.Name
	# Write-Host $fn.Name, $fn.fullname
	# This returns a MatchInfo object
	
	$output = Select-String $searchPattern $fn | Format-Table -Auto LineNumber, @{Label="Text";Expression={$_.Line.Trim()}}
	
	if ($output)
	{
		# Write-Host ">>> Checking " $fn.Name " for " $searchPattern
		">>> Checking " + $fn.Name + "for " + $searchPattern
		""	
		$output
	}
	
}

exit

foreach ($str in $output)
{
	$str | foreach {$_ -replace "^.*WriteEntry(", "WriteEntry("} | foreach {$_ -replace ",.*$", ")"}
}



