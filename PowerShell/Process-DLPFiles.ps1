<# 

.SYNOPSIS 
Process monthly DLP security extracts.

.DESCRIPTION
Calls the Symantec(Vontu) EDMIndexer program on each DLP security extract (.csv) file
and produces an index file.
		
.EXAMPLE
Process-DLPFiles.ps1
.EXAMPLE
Process-DLPFiles -verbose
.EXAMPLE
Process-DLPFiles -DevOutput -verbose
.EXAMPLE
Process-DLPFiles -InputFolder "C:\MyInputFolder" -OutputFolder "\\ind-ftp-01\OutputFolder" -verbose

.NOTES
Additional information about the function or script.

Process-DLPFiles.ps1

SCC:
	This script is under source code control.  Modifications should be 
	checked into the VSS repository located at 
		\\life.pacificlife.net\life\it\vss\TechOffice
	under a project 
		$PowerShell/bin/PowerShell

Last Update:

v1.0.1 crhodes, 2011.04.26, PacificLife
	Support -DevOutput switch to make specifying development server
	output folders easier - the input folders should be same on Dev and Prod.
	
	Log if output files present when run - the Corporate FTP process 
	should delete after they are picked up.  Cleaned up logging.
	
	Changed $OutputFolder and $LogFolder to \\ind-ftp-01\ftproot...
	
v1.0.0 crhodes, 2011.02.28, PacificLife
	Initial Version
	
Be sure to leave two blank lines after end of block comment.
#>

param
(
	[switch] $SendEmailStatus, 
	[string] $EmailStatusAddress = "crhodes@pacificlife.com", 
	[string] $EDMProfileFolder   = "D:\vontu\DLP_EDMProfiles",
	[string] $InputFolder        = "D:\vontu\DLP_InputFiles",
	[string] $OutputFolder       = "\\ind-ftp-01\ftproot\Corp\DLP\DLP_OutputFiles\",
	[string] $InputFilePattern   = '.*\.csv',
	[string] $LogFolder          = "\\ind-ftp-01\ftproot\Corp\DLP\DLP_Log",
	[string] $LogFile            = "EDMIndexerLog.txt",
	[switch] $Verbose,
	[switch] $DevOutput
)

##############################################	
# TODO:
#	Update path to program as needed.	
#	Update DIVISION as needed.
##############################################

$VontuBinFolder = "D:\Vontu\Protect\bin"
$EDMIndexer = "D:\Vontu\protect\bin\RemoteEDMIndexer.exe"
$DIVISION = "LID"

$EDMFileNames = @(
	("PolicyData")
	# ,("PolicyDataForeign"),
	,("PolicyDataWithBankAccount")
	,("ProducerData")
	#,("ProducerDataWithBankAccount")
)

# Main function

function Main
{	
	if ($SCRIPT:DevOutput)
	{
		$OutputFolder = "D:\vontu\DLP_OutputFiles\"
		$LogFolder    = "D:\vontu\DLP_Log"
	}
	
	if ($SCRIPT:Verbose)
	{
		"SendEmailStatus    = $SendEmailStatus"
		"EmailStatusAddress = $EmailStatusAddress"
		"InputFolder        = $InputFolder"
		"OutputFolder       = $OutputFolder"
		"EDMProfileFolder   = $EDMProfileFolder"
		# "InputFilePattern   = $InputFilePattern"
		# "InputFiles         = $InputFiles"
		# "InputFiles.Count   = " + $InputFiles.Count
		"EDMIndexer         = $EDMIndexer"
		"LogFolder          = $LogFolder"
		"LogFile            = $LogFile"
		""
	}
	
	if ( ! (VerifyPrerequisites))
	{
		LogMessage "Error Verifying Prerequistes" "Main" "Error"
		exit
	}
	else
	{
		LogMessage "Prerequisites OK" "Main" "Info"
	}

	$message = "Beginning Process-DLPFiles: " + (Get-Date)
	LogMessage $message "Main" "Info"
	
	ClearOutputFolder
	
	ProcessFiles
	
	ClearInputFolder
	
	$message = "Ending Process-DLPFiles: " + (Get-Date)
	LogMessage $message "Main" "Info"
}

##############################
# Internal Functions
##############################

function ClearInputFolder()
{
	$message = "Removing InputFiles from:" + $InputFolder
	LogMessage $message "ClearInputFolder" "Trace"
	
	foreach ($dataFile in (Get-ChildItem $InputFolder -filter "*.csv"))
	{
		$message = "Removing input file:" + $dataFile.FullName
		LogMessage $message "ClearInputFolder" "Info"
		
		Remove-Item $dataFile.FullName >> $LogFolder\$LogFile
	}
}

function ClearOutputFolder()
{
	$message = "Removing OutputFiles from:" + $OutputFolder
	LogMessage $message "ClearOutputFolder" "Trace"

	foreach ($dataFile in (Get-ChildItem $OutputFolder -filter "ExternalDataSource.LID*"))
	{
		$message = "Removing unexpected output file:" + $dataFile.FullName
		LogMessage $message "ClearOutputFolder" "Error"

		Remove-Item $dataFile.FullName >> $LogFolder\$LogFile
	}
}

function LogMessage()
{
	param
	(
		[string] $message,
		[string] $method,
		[string] $logLevel
	)
	
	$message >> $LogFolder\$LogFile
	
	switch ($logLevel)
	{
		"Trace"
		{ 
			if ($SCRIPT:Verbose) { Write-Host $message }
			Call-PLLog -Trace   -message $message -class "Process-DLPFiles" -method $method
			break
		}	
		
		"Info"
		{ 
			if ($SCRIPT:Verbose) { Write-Host $message }		
			Call-PLLog -Info    -message $message -class "Process-DLPFiles" -method $method
			break
		}
		
		"Warning"
		{ 
			Write-Host $message
			Call-PLLog -Warning -message $message -class "Process-DLPFiles" -method $method
			break
		}
		"Error"
		{ 
			Write-Host $message
			Call-PLLog -Error   -message $message -class "Process-DLPFiles" -method $method
			break
		}
		
		"None"
		{ 
			if ($SCRIPT:Verbose) { Write-Host $message }		
			break
		}
		
		default
		{
			Write-Host $message		
			Call-PLLog -Error "Unexpected log level" + $logLevel -class "Process-DLPFiles" -method "LogMessage"
			break
		}
	}
}

function ProcessFiles()
{
	# For some really silly reason, we have to run the command from the Vontu bin directory.
	
	$currentLocation = get-location
	set-location $VontuBinFolder
	
	LogMessage "ProcessFiles()" "ProcessFiles" "Trace"
	
	foreach ($edmfile in $EDMFileNames)
	{
		$dataFile = $InputFolder + "\" + $edmfile + ".csv"
		$profileFile = $EDMProfileFolder + "\LID" + $edmfile + ".edm"
		$resultFolder = $OutputFolder
			
		$EDMCommand = "$EDMIndexer -data=$dataFile -profile=$profileFile -result=$OutputFolder -ignore_date -verbose"	
		
		date >> $LogFolder\$LogFile
		LogMessage $EDMCommand "ProcessFiles" "Info"
		
		(Invoke-Expression -Command $EDMCommand) >> $LogFolder\$LogFile
	}

	$message = "ProcessFiles() Complete"
	LogMessage $message "ProcessFiles" "Trace"
	
	set-location $currentLocation
}

function VerifyEDMProfiles()
{

	$message = "  VerifyEDMProfiles()"
	LogMessage $message "VerifyEDMProfiles" "Trace"
	
	# Verify the EDM Profiles are available
	
	if ( ! (Test-Path $EDMProfileFolder))
	{
		$message = "EDMProfileFolder: " + $EDMProfileFolder + " does not exist"
		LogMessage $message "VerifyEDMProfiles" "Error"	
		
		return $false
	}
	else
	{
		foreach ($file in $EDMFileNames)
		{
			$edmFile = ($EDMProfileFolder + "\" + $DIVISION + $file + ".edm")
			
			if ( ! (Test-Path $edmFile))
			{
				$message = "Missing EDMProfile file: " + $edmFile
				LogMessage $message "VerifyEDMProfiles" "Error"
				
				return $false
			}
		}
	}
	
	return $true
}
	
function VerifyInputFiles()
{

	$message = "  VerifyInputFiles()"
	LogMessage $message "VerifyInputFiles" "Trace"

	# Verify all the input files are available
	
	if ( ! (Test-Path $InputFolder))
	{
		$message = "InputFolder: " + $InputFolder + " does not exist"
		LogMessage $message "VerifyInputFiles" "Error"

		return $false
	}
	else
	{
		foreach ($file in $EDMFileNames)
		{
			$inputFile = ($InputFolder + "\" + $file + ".csv")
			
			if ( ! (Test-Path $inputFile))
			{
				$message = "    Missing Input file: " + $inputFile
				LogMessage $message "VerifyInputFiles" "Error"

				return $false
			}
		}
	}
	
	return $true
}
	
function VerifyPrerequisites()
{
	$message = "VerifyPrerequisites()"
	LogMessage $message "VerifyPrerequisites" "Trace"

	if ( ! (VerifyEDMProfiles))
	{
		return $false
	}
	
	if ( ! (VerifyInputFiles))
	{
		return $false
	}
		
	$folders = @($OutputFolder, $LogFolder)

	foreach ($folder in $folders)
	{
		if ( ! (Test-Path $folder))
		{
			$message = "Creating $folder"
			LogMessage $message "VerifyPrerequisites" "Info"		
			mkdir $folder
		}
	}
	
	return $true
}

# Note use of @() to force result into array so can check count later.
# $InputFiles = @(Get-ChildItem $InputFolder | Where-Object { $_.Name -match $InputFilePattern })

# Call the main function.  Use Dot Sourcing to ensure executed in Script scope.

. Main
#
# End Process-DLPFiles.ps1
#