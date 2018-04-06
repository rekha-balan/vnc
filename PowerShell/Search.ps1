# Load WPF Assemblies
Add-Type -Assembly PresentationCore, PresentationFramework

trap { break }

$mode = [System.Theading.Thread]::CurrentThread.ApartmentState

if ($mode -ne "STA")
{
    $m = "This script can only be run when powershell is started " +
        "with the -sta switch."
    throw $m
}

# Compute path to XAML file

function Add-PSScriptRoot($file)
{

}