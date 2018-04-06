$a= gci "\\matrix\c$\bin\powershell\" | %{ $_.VersionInfo } 
$b= gci "\\tron\c$\bin\powershell\" | %{ $_.VersionInfo } 
Compare-Object -ReferenceObject $a -DifferenceObject $b -Property fileversion -IncludeEqual -PassThru | select FileName,FileVersion,SideIndicator