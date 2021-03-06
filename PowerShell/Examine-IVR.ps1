<#

.SYNOPSIS 
Examines various IVR logs and eventlog activity.

.DESCRIPTION
Examines various IVR logs and eventlog activity.
        
.EXAMPLE
TODO: Example usage.  This section can be repeated.
.EXAMPLE
TODO: Example usage.  This section can be repeated

.NOTES
TODO: Additional information about the function or script.  Displayed with get-help -full

SCRIPTNAME.ps1

SCC:
    This script is under source code control.  Modifications should be 
    checked into the VSS repository located at 
        \\life.pacificlife.net\life\it\vss\TechOffice
    under a project 
        $PowerShell/bin/PowerShell

Last Update:

v1.0.0 crhodes,     2011.05.11, PacificLife
    Initial Version
    
Be sure to leave two blank lines after end of block comment.    
#>

##############################################    
# Script Level Parameters.
##############################################

param
(
# <TODO: Add script level paramters>
    [switch] $AppProcessorLog,
    [string] $SearchPattern,
	[DateTime] $SearchDate,
	[string] $LogFileFolder = "C:\Program Files\Vista\Server\log",
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
		"LogFileFolder      = $LogFileFolder"
        "AppProcessorLog    = $AppProcessorLog"
		"SearchDate         = $SearchDate"
        "SearchPattern      = $SearchPattern"
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

    $message = "Begining " + $SCRIPTNAME + ": " + (Get-Date)
    LogMessage $message "Main" "Info"
    
# <TODO: Add code, functional calls here to do something cool>

	if ($SCRIPT:AppProcessorLog)
	{
		SearchAppProcessorLog $SearchPattern
	}
    
    $message = "Ending   " + $SCRIPTNAME + ": " + (Get-Date)
    LogMessage $message "Main" "Info"
}

##############################
# Internal Functions
##############################

function SearchAppProcessorLog()
{
    # param
    # (
        # [string] $searchPattern
    # )

	$logfiles = Get-ChildItem $LogFileFolder -filter "AppProcessor*.log"
	
	if ($SearchDate)
	{
		$logfiles = $logfiles | Where-Object { $_.LastWriteTime.toshortdatestring() -eq $SearchDate.toshortdatestring() }
	}
	
	foreach ($logFile in $logfiles)
	{
		# $logFile
		Select-String $searchPattern $logFile | Select Line | Sort-Object
	}
}

function Func2()
{
    $message = "Doing Something cool in Func2"
    LogMessage $message "Func2" "Info"
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

# Call the main function.  Use Dot Sourcing to ensure executed in Script scope.

. Main

#
# End New-ScriptTemplate1.ps1
#
