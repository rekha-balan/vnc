param ($searchPattern)

# $folders = Get-ChildItem -Filter $searchPattern -Recurse | Where-Object {$_.PSIsContainer} # | Select-Object Name
$folders = Get-ChildItem -Filter $searchPattern | Where-Object {$_.PSIsContainer}

# Check-Code "EventLog|PLLog" $files
# exit
# "file in files"

# foreach ($file in $files)
# {
	# Write-Host $file.Name
# }

# "folder in folders"

# foreach ($folder in $folders)
# {
	# "****************************************"
	# "Examining " $folder.Name
	# "****************************************"	
# }

# exit

foreach ($folder in $folders)
{
# $folder | get-member

	"****************************************"
	"Examining " + $folder.Name
	"****************************************"
	""
	
	# $files = Get-ChildItem -Path $folder.Name -Include *.cs
	$files = Get-ChildItem -Path $folder.Name -Filter "*.cs"
	# TODO: Add a switch to do -odx files
	# $files += Get-ChildItem -Path $folder.Name -Filter "*.odx"
	# $files
	
	# Check-Code "EventLog|PLLog" $folder.name\*.cs $folder.name\*.odx
	# Check-Code "Email" $folder.name\*.cs $folder.name\*.odx
	
	if ($files)
	{
		Check-Code "EventLog|PLLog" $files
		Check-Code "Email" $files
	}
}