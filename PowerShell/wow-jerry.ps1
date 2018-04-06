# cd .\OriginalRequestArchive
# $requests = select-string -Path OriginalRequestArchive\* -Pattern "A438250000" | % { [xml] $_.Line} | % {$_.EAI_ToTransaction.Xmlpayload.Request.Reporting}

$OutputFilePath = "\\lifenas115\dsto\biztalkapplications\mcr"

# cd ..
# NB. Have to send the first $_ out with format-list to avoid having it go down the pipeline and then get consumed by format-table.
	$requests | % {
		# $_ | format-list
		$_ | format-table ListBillId, RequestId, InstanceID, ReportPeriodStartDate, ReportPeriodEndDate, IncludePremium, ReportType, OfficeID, ReportStorageType
		# $_
		$instanceid = $_.instanceid
		$responsefile = get-childitem -filter *$instanceid* -path $OutputFilePath\mcrmainframeinstanceidresponse
		# Not sure why this doesn't work
		# $responsefile = get-childitem -filter *($_.instanceid)* -path mcrmainframeinstanceidresponse		
		# $responsefile | format-table fullname, creationtime
		# "mainframeresponsefile"
		# $responsefile.fullname
		# "archivefolderpath:";([xml](get-content $responsefile.fullname)).root.archivefolderpath
		$archiveFolderPath = "ArchiveFolderPath: " + ([xml](get-content $responsefile.fullname)).root.archivefolderpath
		$responseFilePath =  "ResponseFile:      " + $responsefile.fullname
		$responseFilePath, $archiveFolderPath | format-table
		# Write-Host ($responsefile.fullname | get-member)
		# Write-Host ($archivefolderpath | get-member)
		# format-table @($responsefile.fullname, $archivefolderpath)
		# " Comma "
		# $v1 = ($responsefile.fullname, $archivefolderpath)
		# Write-Host ($v1 | get-member)
		# Write-Host ($responsefile.fullname, $archivefolderpath | get-member)
		# ' @() '
		# Write-Host ((@($responsefile.fullname, $archivefolderpath)) | get-member | format-list)
		
		# $responsefile.fullname, $archivefolderpath | format-table
		# @($responsefile.fullname, $archivefolderpath) | format-table -auto -wrap
		# @($responsefile.fullname, $archivefolderpath) | format-table fullname		
		# @($responsefile.fullname, $archivefolderpath) | format-list		
		#get-childitem -filter *$instanceid* -path mcrmainframeinstanceidresponse | format-table fullname, creationtime
	}	