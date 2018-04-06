<#
.SYNOPSIS 
Starts and stops USB tracing.

.DESCRIPTION
The USB-Trace.ps1 script enables and disables the tracing of USB activity.
It depends on the ETW trace facility in Windows 7

.PARAMETER InputPath
Specifies the path to the CSV-based input file.

.PARAMETER OutputPath
Specifies the name and path for the CSV-based output file. By default, 
MonthlyUpdates.ps1 generates a name from the date and time it runs, and
saves the output in the local directory.

.INPUTS
None. You cannot pipe objects to Update-Month.ps1.

.OUTPUTS
None. USB-Trace.ps1 does not generate any output.

.EXAMPLE
C:\PS> .\USB-Trace.ps1 -start
.EXAMPLE
C:\PS> .\USB-Trace.ps1 -stop
#>
##############################################################################
#
# USB-Trace.ps1
#
# <Description>
#
# <Usage>
#
# <Notes/ToDo>
#
# SCC:
#	This script is under source code control.  Modifications should be 
#	checked into the VSS repository located at 
#		\\life.pacificlife.net\life\it\vss\TechOffice
#	under a project 
#		$PowerShell/Scripts
#
#
# Last Update:
#
# v1.0.0 <Author>, <Date>, <Company>
##############################################################################


param([Switch] $start, [Switch] $stop)
if ($start)
{
	"Starting USB trace"
	logman start UsbTrace -p Microsoft-Windows-USB-USBPORT -o C:\temp\usbtrace.etl -ets -nb 128 640 -bs 128
	logman update UsbTrace -p Microsoft-Windows-USB-USBHUB -ets
}

if ($stop)
{
	"Stopping USB trace"
	logman stop UsbTrace -ets
}

