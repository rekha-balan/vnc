$FilesToSync = New-Object System.Collections.ArrayList

# [void] to ignore output of .Add

[void] $FilesToSync.Add("CHR Notes - Ease - 2017 Future.vsdx")
[void] $FilesToSync.Add("CHR Notes - Ease - AML - Applications and Implementation.vsdx")        
[void] $FilesToSync.Add("CHR Notes - Ease - Architecture, Configuration, Databases, Tables, and Structures.vsdx")
[void] $FilesToSync.Add("CHR Notes - Ease - Business, Concepts, Customers, Products, Timeline.vsdx")
[void] $FilesToSync.Add("CHR Notes - Ease - ClientApps - Architecture and Implementation.vsdx")
[void] $FilesToSync.Add("CHR Notes - Ease - Configuration and Release Management, Environments, and Tools.vsdx ")
[void] $FilesToSync.Add("CHR Notes - Ease - Cummins - Applications and Implementation.vsdx")
[void] $FilesToSync.Add("CHR Notes - Ease - EaseWorksCommon.vsdx")
[void] $FilesToSync.Add("CHR Notes - Ease - JUNO - Applications and Implementation.vsdx")
[void] $FilesToSync.Add("CHR Notes - Ease - MES7 - Applications, and Implementation.vsdx")
[void] $FilesToSync.Add("CHR Notes - Ease - Process.vsdx")
[void] $FilesToSync.Add("CHR Notes - Ease - UAT Systems.vsdx")

$SourceDir = 'B:\CHR Notes - Files'
$TargetDir = 'E:\EaseWorks (Legacy)\Documentation\CHR Notes'

"Syncing CHR Notes - EASE* files from $SourceDir to $TargetDir"

foreach ($file in $FilesToSync)
{
    "Copying $file"
    Copy-Item "$SourceDir\$File" $TargetDir
}

