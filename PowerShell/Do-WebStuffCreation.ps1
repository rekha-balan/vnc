
<#
Do-WebStuffCreation.ps1

.Synopsis
   Short description
.DESCRIPTION
   Long description
.EXAMPLE
   Example of how to use this cmdlet
.EXAMPLE
   Another example of how to use this cmdlet
.INPUTS
   Inputs to this cmdlet (if any)
.OUTPUTS
   Output from this cmdlet (if any)
.NOTES
   General notes
.COMPONENT
   The component this cmdlet belongs to
.ROLE
   The role this cmdlet belongs to
.FUNCTIONALITY
   The functionality that best describes this cmdlet
   
SCC:
	This script is under source code control.  
	Modifications should be checked into 
	the TFS repository located at 
		<Team Project Collection>
	under a project 
		$<TeamProject>/<Path>

ToDo:

Last Update:

v1.0.0 <Author>, <Date>, <Company>
   
#>

param
(
    $XmlFile = "C:\temp\WebStuffCreation.xml"
)

Add-Type -AssemblyName System.Core
Add-Type -AssemblyName System.Xml.Linq

if ( ! (Test-Path $XmlFile))
{
    "File at $XmlFile does not exist"
    Exit
}

$xldoc = [System.Xml.Linq.XDocument]::Load($XmlFile)

$xldoc.Descendants("AppPools").Elements("AppPool").Attributes("Name").Value
$xldoc.Descendants("AppPools").Elements("AppPool").Attributes("managedRuntimeVersion").Value

$xldoc.Descendants("AppPools").Elements("AppPool").Attributes().Value

$xldoc.Descendants("AppPools").Elements("AppPool") | %{$_.Attributes("Name").Value, $_.Attributes("managedRuntimeVersion").Value}
($xldoc.Descendants("AppPools").Elements("AppPool") | %{$_.Attributes("Name").Value, $_.Attributes("managedRuntimeVersion").Value})
$n, $m
  