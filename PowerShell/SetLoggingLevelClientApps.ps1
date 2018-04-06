<#
Copy Config Files
#>
<#
 # 
$SourceDirectory = "C:\Source-Ease-Main\ClientApps"
$ApplicationDirectory = "C:\EASE\EASEWorks Version 7"
$OutputFolder = "bin\x86\Release"
#>

$ApplicationDirectory = "C:\EASE\EASEWorks Version 7"
Set-Location -Path $ApplicationDirectory
$inputFile = "AutoUpdate.exe.config"

$ConfigFiles = "AutoUpdate", "Client Editor", "DataLoader", "EASEBatch", "ElementGenerator", "EStart", "MassUpdate", "MDMEditor", "ProcessPlan", "TimeDatabase", "ToolManagement", "Update7"

$matchPriority = 'maximumPriority=\"100\"'
$outputPriority='maximumPriority="10009"'
$outputExt=".10009"

foreach ($file in $ConfigFiles)
{
#"$file.exe.config"
(Get-Content "$file.exe.config") -Replace $matchPriority, $outputPriority | Out-File -Encoding ascii "$file.exe.config$outputExt"
}

$ConfigFiles = "AutoUpdate", "Client Editor", "DataLoader", "EASEBatch", "ElementGenerator", "EStart", "MassUpdate", "MDMEditor", "ProcessPlan", "TimeDatabase", "ToolManagement", "Update7"
$outputExt=".10009"
foreach ($file in $ConfigFiles)
{
    Copy-Item "$file.exe.config$outputExt" "$file.exe.config"
}

#(Get-Content $$file.exe.config) -Replace 'maximumPriority=\"100\"', 'maximumPriority="10009"' | Out-File "$file.exe.config.10009"
<#
Copy-Item "$SourceDirectory\AutoUpdate\bin\Release\AutoUpdate.exe.config"              $ApplicationDirectory

Copy-Item "$SourceDirectory\ClientEditor\bin\x86\Release\Client Editor.exe.config"     $ApplicationDirectory

#Copy-Item "$SourceDirectory\Config\$OutputFolder\Config.exe.config" $ApplicationDirectory

Copy-Item "$SourceDirectory\DataLoader\$OutputFolder\DataLoader.exe.config"      $ApplicationDirectory

Copy-Item "$SourceDirectory\EASEBatch\$OutputFolder\EASEBatch.exe.config"        $ApplicationDirectory

Copy-Item "$SourceDirectory\ElementGenerator\$OutputFolder\ElementGenerator.exe.config" $ApplicationDirectory

Copy-Item "$SourceDirectory\EStart\$OutputFolder\EStart.exe.config" $ApplicationDirectory

Copy-Item "$SourceDirectory\MassUpdate\$OutputFolder\MassUpdate.exe.config" $ApplicationDirectory

Copy-Item "$SourceDirectory\MDMEditor\$OutputFolder\MDMEditor.exe.config" $ApplicationDirectory

Copy-Item "$SourceDirectory\ProcessPlan\$OutputFolder\ProcessPlan.exe.config" $ApplicationDirectory

Copy-Item "$SourceDirectory\TimeDatabase\$OutputFolder\TimeDatabase.exe.config" $ApplicationDirectory

Copy-Item "$SourceDirectory\ToolManagement\$OutputFolder\ToolManagement.exe.config" $ApplicationDirectory

Copy-Item "$SourceDirectory\Update7\$OutputFolder\Update7.exe.config" $ApplicationDirectory

#>




