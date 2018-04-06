<#
Show-WebAppPools.ps1

.Synopsis
   Show Web AppPools Information
.DESCRIPTION
   Long description
.EXAMPLE
   Show-WebAppPools
.EXAMPLE
   Show-WebAppPools -Details
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

Output HTML with links

Last Update:

v1.0.0 crhodes, 2013.01.21
#>   

param(
    [switch] $Details,
    [switch] $Recurse
)

function Show-WebConfigurationElement
{
    param
    (
        $ce, 
        [switch] $Recurse,
        [string] $Pad = ""
    )

    "Recurse:{0}" -f $Recurse

    $Pad + "*"*40
    $Pad + $ce.ElementTagName + " - ConfigurationElement"
    $Pad + "*"*40
    ""
    $Pad + "Attributes:"
    $Pad + "-----------"

    # Need to learn how to shove this whole table over by $Pad amount
    # For now just cheat and add the $Pad column.  Looks ugly, I know.

    if ($Pad.Length -gt 0)
    {
        $ce.Attributes | 
            Sort-Object Name |
            Format-Table $pad, Name, TypeName, Value  -AutoSize
    }
    else
    {
        $ce.Attributes | 
            Sort-Object Name |
            Format-Table Name, TypeName, Value  -AutoSize
    }

    if ($Recurse)
    {
        $Pad += "    "

        foreach($ce1 in ($ce.ChildElements | Sort-Object Name))
        {
            Show-WebConfigurationElement $ce1 -Recurse: $Recurse -Pad $Pad
        }
    }
}

function Show-WebAppPool 
{
    param
    (
        $AppPoolName = "DefaultAppPool",
        [switch] $Recurse
    )

    $AppPool = Get-Item ("IIS:\AppPools\" + $AppPoolName)

    "*"*40
    "AppPool: " + $AppPool.name
    "*"*40

    "Attributes:"
    "-----------"

    $AppPool.Attributes | 
        Sort-Object Name | 
        Format-Table  Name, TypeName,Value -AutoSize

    foreach($ce in ($AppPool.ChildElements | Sort-Object Name))
    {
        Show-WebConfigurationElement $ce -Recurse: $Recurse
    }
}

if ((Get-Module WebAdministration) -eq $Null) {Import-Module WebAdministration}

Push-Location IIS:\

"*"*50
"  Application Pools for " + $env:COMPUTERNAME
"  $(Get-Date)"
"*"*50

Get-ChildItem AppPools | 
    Format-Table `
        @{n="AppPool"; e={$_.name}}, `
        @{n="State"; e={$_.state}}, `
        @{n="RuntimeVersion"; e={$_.managedRuntimeVersion}}, `
        @{n="PipelineMode"; e={$_.managedPipelineMode}} -AutoSize

if ($Details)
{
    foreach($ap in (Get-ChildItem AppPools))
    {
        Show-WebAppPool $ap.name -Recurse: $Recurse
    }
}

Pop-Location