<#
Copy Config Files
#>
$SourceDirectory = "C:\Source-Ease-Main\ClientApps"
$ApplicationDirectory = "C:\EASE\EASEWorks Version 7"
$OutputFolder = "bin\x86\Release"

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




