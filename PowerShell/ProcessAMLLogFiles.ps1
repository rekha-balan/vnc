cd 'C:\Customers\Aston Martin\2018JulyVisit\AML_Logs_IIS01\LogsDev\'
cd 'C:\Customers\Aston Martin\2018JulyVisit\AML_Logs_IIS02\LogsDev\'
cd 'C:\Customers\Aston Martin\2018JulyVisit\AML_Logs_IIS03\view ease 7 logs\'
cd 'C:\Customers\Aston Martin\2018JulyVisit\AML_Logs_IIS03\view ease 71 logs (chrome)\'

$logfiles = Get-ChildItem *.log | Sort-Object LastWriteTime

foreach ($file in $logfiles)
{
#    "Processing $file"

    Get-Content $file | % { if ($_.Split("|")[10] -eq "Global_asax.Application_Start") { $_; } } >> "Application_Start"

    Get-Content $file | % { if ($_.Split("|")[10] -eq "Global_asax.Session_Start") { $_; } }  >> "Session_Start"
}

<#

Get-Content $file | % { $_.Split("|")[10]  } | Select-String -Pattern 'Connection*'

Get-Content $file | % { if ($_.Split("|")[10] -match 'Global_asax*') {$_;} } 
 
#>

'Searching for Global_asax*'

# $outfile = "Global_asax.txt"
$outfile = ""
#$pattern = "Global_asax*"
#$pattern = "Global_asax*"
#$pattern = "Global_asax*"
$pattern = "frmEngine*"

foreach ($file in $logfiles)
{
    if ($outfile -ne "")
    {
        "Processing $file.Name" >> $outfile

        Get-Content $file | % { if ($_.Split("|")[10] -match $pattern) {$_;} } >> $outfile
    }
    else
    {
        "Processing $file"
    
        Get-Content $file | % { if ($_.Split("|")[10] -match $pattern) {$_;} }
    }
}


'Searching for Long Calls'

$outfile = "LongDurationCalls.txt"
#$outfile = ""

foreach ($file in $logfiles)
{
    if ($outfile -ne "")
    {
        "Processing $file.Name" >> $outfile
         
        Get-Content $file | % { if ([single]$_.Split("|")[11] -gt [single]20.0) {$_.Split("|")[11] + " " + $_.Split("|")[10]; } } | Sort-Object >> $outfile
    }
    else
    {
        "Processing $file.Name"

        Get-Content $file | % { if ([single]$_.Split("|")[11] -gt [single]20.0) {$_.Split("|")[11] + " " + $_.Split("|")[10]; } } | Sort-Object
#    Get-Content $file | % { if ([single]$_.Split("|")[11] -gt [single]20.0) {$_.Split("|")[11] + " " + $_.Split("|")[10]; } } | Method-Info
    }
}

Get-Content $file | % { $_.Split("|")[11] }


