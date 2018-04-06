<#

.SYNOPSIS 
Get-MCRInfo.  Displays information about MCR activity

.DESCRIPTION
Get-MCRInfo.  Displays information about MCR activity
        
.EXAMPLE
TODO: Example usage.  This section can be repeated.
.EXAMPLE
TODO: Example usage.  This section can be repeated

.NOTES
TODO: Additional information about the function or script.  Displayed with get-help -full

Get-MCRInfo.ps1

SCC:
    This script is under source code control.  Modifications should be 
    checked into the TFS repository located at 
        \\life.pacificlife.net\life\it\vss\TechOffice
    under a project 
        $PowerShell/bin/PowerShell

Last Update:

v1.0.0 dev, 	2012.01.09, PacificLife
    Initial Version
       
#>

##############################################    
# Script Level Parameters.
##############################################

param
(
# <TODO: Add script level paramters>
    [string] $Listbill,
	[string] $OutputFilePath       = "\\lifenas115\dsto\BizTalkApplications\MCR",
    [switch] $NoHeaderFooter,
    # [string] $StringArg1         = "DefaultValueStringArg1", 
    # [string] $StringArg2,
	# [switch] $Contents,
    [switch] $Verbose
)

##############################################    
# Script Level Variables
##############################################

$ScriptVar1 = "Foo"
$ScriptVar2 = 42
$ScriptVar3 = @(
    ("Apple")
    ,("Pear")
    ,("Yogurt")
)
$SCRIPTNAME = $myInvocation.InvocationName
$SCRIPTPATH = & { $myInvocation.ScriptName }

##############################################
# Main function
##############################################

function Main
{    
    if ($SCRIPT:Verbose)
    {
		"SCRIPTPATH         = $SCRIPTPATH"
        "Listbill           = $Listbill"
        "OutputFilePath     = $OutputFilePath"
        "NoHeaderFooter     = $NoHeaderFooter"
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

    $message = "Beginning " + $SCRIPTNAME + ": " + (Get-Date)
    LogMessage $message "Main" "Info"
    
# <TODO: Add code, functional calls here to do something cool>

    $matchingRequests = GetMatchingRequests($Listbill)
	
	DisplayMatchingRequests $matchingRequests
	
    # Func2
    
    # Func3
    
    $message = "Ending   " + $SCRIPTNAME + ": " + (Get-Date)
    LogMessage $message "Main" "Info"
}

##############################
# Internal Functions
##############################

function GetMatchingRequests([string] $listBill)
{
    $message = "Looking for ListbillID: " + $listBill
    LogMessage $message "GetMatchingRequests" "Info"
	
	$requests = select-string -Path $OutputFilePath\OriginalRequestArchive\* -Pattern $listBill # | % { [xml] $_.Line} | % {$_.EAI_ToTransaction.Xmlpayload.Request.Reporting}
	
	$message = "Found " + $requests.count + " matching requests"	
	LogMessage $message "GetMatchingRequests" "Info"
	
	return $requests
}

function DisplayMatchingRequests()
{
	param
	( $requestFiles )

	$message = "Processing " + $requestFiles.count + " matches"
	LogMessage $message "DisplayMatchingRequests" "Info"
	
	$requestFiles | % {
		OutputEntryHeader $_.Path

		$utid = ([xml] $_.Line).EAI_ToTransaction.UTID
		$metaInformation = ([xml] $_.Line).EAI_ToTransaction.Metainformation
		$reporting = ([xml] $_.Line).EAI_ToTransaction.Xmlpayload.Request.Reporting
		
		# Display the request details
		DisplayRequestDetails $utid $metaInformation $reporting
		
		# $request | format-table ListBillId, RequestId, InstanceID, ReportPeriodStartDate, ReportPeriodEndDate, IncludePremium, ReportType, OfficeID, ReportStorageType		
		
		# Locate the response files based on the request instance ID
		$instanceid = $reporting.instanceid
		$responsefile = get-childitem -filter *$instanceid* -path $OutputFilePath\mcrmainframeinstanceidresponse
		
		# TODO: Handle no response
		# Locate the archive folder from the mainframe response
		$archiveFolderPath = "ArchiveFolderPath: " + ([xml](get-content $responsefile.fullname)).root.archivefolderpath
		$responseFilePath =  "ResponseFile:      " + $responsefile.fullname
		
		# Display the paths
		$responseFilePath, $archiveFolderPath | format-table
		
		OutputEntryFooter
	}	
}

function DisplayRequestDetails($utid, $metaInformation, $reporting)
{
	Write-Host "UTID:            " $utid
	
	# While this will work	
	# $metaInformation | format-table
	
	$formatExpression = `
		@{Expression={$_.TransactionDate}   ; Label="TransactionDate"   ; width=25}, `
		@{Expression={$_.ClientUser}        ; Label="ClientUser"        ; width=25}, `
		@{Expression={$_.ClientApplication} ; Label="ClientApplication" ; width=20}, `
		@{Expression={$_.ClientMachine}     ; Label="ClientMachine"     ; width=20}, `
		@{Expression={$_.SessionId}         ; Label="SessionId"         ; width=12}

	$metaInformation | format-table $formatExpression
		
	# While this will work	
	# $reporting | format-table ListBillId, RequestId, InstanceID, ReportPeriodStartDate, ReportPeriodEndDate, IncludePremium, ReportType, OfficeID, ReportStorageType			
	
	# This makes it pretty
	
	$formatExpression = `
		@{Expression={$_.ListbillId}            ; Label="Listbill"       ; width=12}, `
		@{Expression={$_.RequestId}             ; Label="RequestID"      ; width=12}, `
		@{Expression={$_.InstanceId}            ; Label="InstanceID"     ; width=12}, `
		@{Expression={$_.ReportPeriodStartDate} ; Label="StartDate"      ; width=12}, `
		@{Expression={$_.ReportPeriodEndDate}   ; Label="EndDate"        ; width=12}, `
		# @{Expression={$_.IncludePremium}        ; Label="IncludePremium" ; width=16}, `
		# @{Expression={$_.ReportType}            ; Label="ReportType"     ; width=12}, `
		@{Expression={$_.OfficeId}              ; Label="OfficeId"       ; width=10}, `
		@{Expression={$_.ReportStorageType}     ; Label="StorageType"    ; width=12}, `
		@{Expression={$_.ReportClientType}      ; Label="ClientType"     ; width=12}, `
		@{Expression={$_.SQLReportingType}      ; Label="ReportingType"  ; width=20}			
		
	$reporting | format-table $formatExpression			
}

function OutputEntryFooter([string] $path)
{
	if ( ! $SCRIPT:NoHeaderFooter)
	{
		Write-Host
		Write-Host "<<-----"
	}
}

function OutputEntryHeader([string] $path)
{
	if ( ! $SCRIPT:NoHeaderFooter)
	{
		Write-Host
		Write-Host "----->>"
		Write-Host
		Write-Host "RequestFile:     " $path
	}
}

function Func3()
{
    $message = "Doing Something cool in Func3"
    LogMessage $message "Func3" "Info"
}

##############################
# Internal Support Functions
##############################

function LogMessage()
{
    param
    (
        [string] $message,
        [string] $method,
        [string] $logLevel
    )
    
	# <TODO: Each case can be modified to do the appropriate type of console/PLLog logging.
	
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
            # if ($SCRIPT:Verbose) { Write-Host $message }   
			Write-Host $message
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

function VerifyFunc1()
{

    $message = "  VerifyFunc1()"
    LogMessage $message "VerifyFunc1" "Trace"

    # Verify something
    
    # if ( ! (Test-Path $InputFolder))
    # {
        # $message = "InputFolder: " + $InputFolder + " does not exist"
        # LogMessage $message "VerifyInputFiles" "Error"

        # return $false
    # }
    # else
    # {
        # foreach ($file in $EDMFileNames)
        # {
            # $inputFile = ($InputFolder + "\" + $file + ".csv")
            
            # if ( ! (Test-Path $inputFile))
            # {
                # $message = "    Missing Input file: " + $inputFile
                # LogMessage $message "VerifyInputFiles" "Error"

                # return $false
            # }
        # }
    # }
    
    return $true
}
    
function VerifyFunc2()
{
    $message = "  VerifyFunc2()"
    LogMessage $message "VerifyFunc2" "Trace"

    return $true
}
    
function VerifyPrerequisites()
{
    $message = "VerifyPrerequisites()"
    LogMessage $message "VerifyPrerequisites" "Trace"

    if ( ! (VerifyFunc1))
    {
        return $false
    }
    
    if ( ! (VerifyFunc2))
    {
        return $false
    }
            
    return $true
}

# if ($SCRIPT:Contents)
# {
	# $myInvocation.MyCommand.ScriptBlock
	# exit
# }
	
# Call the main function.  Use Dot Sourcing to ensure executed in Script scope.

. Main

#
# End New-ScriptTemplate1.ps1
#