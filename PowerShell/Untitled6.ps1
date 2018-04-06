Select-String create .\newtablesVer6.sql |  % { $_.Split(' ')[2] }
$answers = Select-String create .\newtablesVer6.sql

$answers | %{ $a=$_.Line.Split(' '); Write-Host "No of fields"$a[0]"="$a.length; }

$answers | % { $_.Line.Split(' ')[2]; } | Sort-Object