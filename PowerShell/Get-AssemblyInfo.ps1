################################################################################
#
# GetAssemblyInfo.ps1
#
# Note:
#	This script is under source code control.  Modifications should be 
#	checked into the VSS repository located at 
#		\\life.pacificlife.net\life\it\vss\TechOffice
#	under a project 
#		$PowerShell/Scripts
#
# Script to automate the collection of Assembly Version information.
#
# TODO:
#
# Last Update:
#
# v1.0 Christopher Rhodes, October 2010, Pacific Life Insurance
#
################################################################################

# This needs to be first.
# Get command line arguments

param
(
	$files = (get-childitem *.dll)
)
	
"Assembly                       AssemblyVersion      FileVersion          ProductVersion"
foreach ($file in $files)
{
	$asmb = [System.Reflection.Assembly]::Loadfile($file)
	$Aname = $asmb.GetName()
	$Aver = $Aname.version
	$VersionInfo = (get-childitem $file).VersionInfo
	$AFileVersion = $VersionInfo.FileVersion
	$AProductVersion = $VersionInfo.ProductVersion
	"{0,30}  {1,-20} {2,-20} {3,-20}" -f $Aname.name, $Aver, $AFileVersion, $AProductVersion
	
}

################################################################################
#
# GetAssemblyInfo.ps1
#
################################################################################