################################################################################
#
# NASSpaceUtilization.ps1
#
# Note:
#	This script is under source code control.  Modifications should be 
#	checked into the VSS repository located at 
#		\\life.pacificlife.net\life\it\vss\TechOffice
#	under a project 
#		$PowerShell/Scripts
#
# Script to automate the collection of space utilized by folders on a NAS share.
#
# TODO:
#	Need to auto-discover folders.
#	Need to handle case where no access on sub-folder
#
# Last Update:
#
# v1.1 Christopher Rhodes, October 2010, Pacific Life Insurance
#	Added support for Excel output.
#
# v1.0 Christopher Rhodes, October 2010, Pacific Life Insurance
#	Originial Version
#
################################################################################

# This needs to be first.
# Get command line arguments

param(
	[string] $NASShare=$(throw 'Need NAS Share as \\ShareName')
	)

# For now the list of shares on each NAS drive are hard coded in this program.  
# A future release may make this discoverable at runtime

$IndFTP01 = "ADMSSQL", "DCM", "DCM_SUPPORT", "DCMCorr", "DCMCorrStg", "DCMProd", `
		"DCMProdDataSets", "DCMstage", "DCMStgDatasets", "ExtAdj", "FieldFin", `
		"FTPPROD", "ftproot", "ftptest", "Helpdesk", "LAWSON", "LegacyDataDelivery", `
		"LogShare", "LogShareStg", "NDM2", "NDMTEST", "netsec", "NTI", "PBSRPTS", `
		"Pivotal", "PLCWS", "PLDocuMaker", "PLDocumakerStg", "RCExtAdj", "SDD", `
		"STG_Helpdesk", "wwwroot", "xlr750"
		
$LifeExtFil02 = "AWDTempFiles", "CaptivaAWD", "CaptivaIAC", "CaptivaIAS", "CSDFORMS", `
		"DataFeeds", "DSTO", "IWMClientLogs", "IWMLogs", "MCR", "MoveItDMZ", `
		"NavigatorOutput", "NavPPTAutomationFiles", "PLCWS", "PPT", "PPTIllustrationOutputs", `
		"PPTReportsOutput", "root", "Webtrends", "Z"
		
$LifeNas115 = "BizApps", "BizDeploy", "ClientServices", "CDSFORMS", "DataDelivery", `
		"DCM Logs", "DSTO", "LifeNas115", "LifeRptSvc_FundXferAudit", "SQLDumps", `
		"STGCSDFORMS", "TRNCSDFORMS", "Z"
		
$LifeWeb27 = "ALIS"	, "bowne", "Client_Services_Reports", "DailyUnitValues", `
		"DataDelivery", "DataServices", "DUV_in", "LabOne", "MCR", "MCRReports", `
		"NDM", "Reports", "temp", "UsageOutput", "VITs", "WF_Reins"

# Display space used by NAS Share

function DisplayNASFolderUsage($folders)
{
	Write-Host "Space Utilization on $NASShare"
	
	foreach ($folder in $folders)
	{
		# Verify there is access to the folder
		Get-Item $NASShare\$folder > $null 2> $null
		
		if ($?)
		{
			$results = (Get-ChildItem $NASShare\$folder -recurse -ea silentlycontinue | Measure-Object -property length -sum)		
			# 10 is length of " <No Access>"
			"{0,10:N2}" -f ($results.Sum / 1MB) + " MB" + " : \" + $folder		
		}
		else
		{
			" <No Access>" + " : \" + $folder
		}
	}
}

################################################################################
#
# Main script
#
################################################################################

switch ($NASShare)
{
	'\\Ind-Ftp-01'
	{
		DisplayNASFolderUsage($IndFTP01)
	}
	
	'\\LifeExtFil02'
	{
		DisplayNASFolderUsage($LifeExtFil02)
	}
	
	'\\LifeNas115'
	{
		DisplayNASFolderUsage($LifeNas115)
	}
	
	'\\LifeWeb27'
	{
		#$output = 
		DisplayNASFolderUsage($LifeWeb27)
		# $output | Export-Csv "C:\temp\Get-NasSpace.csv"
		# $output
	}	
}

################################################################################
#
# End NASSpaceUtilization.ps1
#
################################################################################




