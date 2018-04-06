##############################################################################
##
## Get-MachineStartupShutdownScript.ps1
##
## From Windows PowerShell Cookbook (O'Reilly)
## by Lee Holmes (http://www.leeholmes.com/guide)
##
## Get the startup or shutdown scripts assigned to a machine
##
## ie:
##
##  PS >Get-MachineStartupShutdownScript Startup
##
##############################################################################

param(
  $scriptType = $(throw "Please specify the script type")
  )

## Verify that they've specified a correct script type
$scriptOptions = "Startup","Shutdown"
if($scriptOptions -notcontains $scriptType)
{
    $error = "Cannot convert value {0} to a script type. " +
             "Specify one of the following values and try again. " +
             "The possible values are ""{1}""."
    $ofs = ", "
    throw ($error -f $scriptType, ([string] $scriptOptions))
}

## Store the location of the group policy scripts for the machine
$registryKey = "HKLM:\SOFTWARE\Policies\Microsoft\Windows\System\Scripts"

## Go through each of the policies in the specified key
foreach($policy in Get-ChildItem $registryKey\$scriptType)
{
    ## For each of the scripts in that policy, get its script name
    ## and parameters
    foreach($script in Get-ChildItem $policy.PsPath)
    {
        Get-ItemProperty $script.PsPath | Select Script,Parameters
    }
}