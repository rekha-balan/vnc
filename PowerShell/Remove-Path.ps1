Function global:REMOVE-PATH()
{
[Cmdletbinding()]
param
( 
[parameter(Mandatory=$True,
ValueFromPipeline=$True,
Position=0)]
[String[]]$RemovedFolder
)

# Get the Current Search Path from the environment keys in the registry

$NewPath=(Get-ItemProperty -Path 'Registry::HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Session Manager\Environment' -Name PATH).Path

# Find the value to remove, replace it with $NULL. If it’s not found, nothing will change.

$NewPath=$NewPath –replace $RemovedFolder,$NULL

# Update the Environment Path

Set-ItemProperty -Path 'Registry::HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Session Manager\Environment' -Name PATH –Value $newPath

# Show what we just did

Return $NewPath

}
