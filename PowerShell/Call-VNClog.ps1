<# 

.SYNOPSIS 
Call VNC.Logging Log routines.

.DESCRIPTION
Used to call VNC.Logging.Log from PowerShell scripts.
		
.EXAMPLE
Call-VNCLog -Info -Message "Message to Log goes here"
.EXAMPLE
Call-VNCLog -Trace -Message "Doing Step One" -class "MyClass" -method "MyMethod"


.NOTES
<ScriptName - Consider Verb-Noun>.ps1

SCC:
	This script is under source code control.  Modifications should be 
	checked into the VSS repository located at 
		\\life.pacificlife.net\life\it\vss\TechOffice
	under a project 
		$PowerShell/Scripts

Last Update:

v1.0.0 crhodes, 2011.02.28, PacificLife
	Initial Version
	
Be sure to leave two blank lines after end of block comment.
#>

param
(
	[switch] $Error,
	[switch] $Warning,
	[switch] $Info,
	[switch] $Debug,
	[switch] $Trace,
	[switch] $Trace1,
	[switch] $TestAll,
	[string] $message,
	[string] $class="",
	[string] $method=""
)

function Main
{
	if ($Error)
	{
		PLLogError
	}
	
	if ($Warning)
	{
		PLLogWarning
	}
	
	if ($Info)
	{
		PLLogInfo
	}
	
	if ($Debug)
	{
		PLLogDebug
	}
	
	if ($Trace)
	{
		PLLogTrace
	}
	
	if ($Trace1)
	{
		PLLogTrace1
	}
	
	if ($TestAll)
	{
		TestAll
	}
		
}

function PLLogError()
{
	[PacificLife.Life.PLLog]::WriteLight($message, 2, "PowerShell", -1, $class, $method, $false, 0)
}
function PLLogWarning()
{
	[PacificLife.Life.PLLog]::WriteLight($message, 4, "PowerShell", 1, $class, $method, $false, 0)
}
function PLLogInfo()
{
	[PacificLife.Life.PLLog]::WriteLight($message, 8, "PowerShell", 100, $class, $method, $false, 0)
}
function PLLogDebug()
{
	[PacificLife.Life.PLLog]::WriteLight($message, 16, "PowerShell", 1000, $class, $method, $false, 0)
}
function PLLogTrace()
{
	[PacificLife.Life.PLLog]::WriteLight($message, 16, "PowerShell", 10000, $class, $method, $false, 0)
}
function PLLogTrace1()
{
	[PacificLife.Life.PLLog]::WriteLight($message, 16, "PowerShell", 10001, $class, $method, $false, 0)
}

function TestAll()
{
	Call-PLLog -Error -message "Error Message"
	Call-PLLog -Warning -message "Warning Message"
	Call-PLLog -Info    -message "Info Message"
	Call-PLLog -Debug   -message "Debug Message"
	Call-PLLog -Trace   -message "Trace Message" -class "MyClass" -method "MyMethod"
	Call-PLLog -Trace1  -message "Trace1 Message"
	
}

# Call the main function.  Use Dot Sourcing to ensure executed in Script scope.

. Main
#
# End Demo-CalingPLLog.ps1
#