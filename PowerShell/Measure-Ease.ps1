

function GetTotalSize
{
    Get-Childitem -recurse *.vb | Get-Content | Measure-Object -Line
}

function GetFileSize
{
    Get-Childitem -recurse *.vb |
        % { $_ | select name, @{n="lines";e={Get-Content $_ | Measure-Object -Line | select -ExpandProperty lines } } }
}

function GetFolderInfo
{
    "Total Lines"
    GetTotalSize
    "Lines by File"
    GetFileSize
}

cd C:\EaseSource\Source-ease_main-master\Common\EASEClass

GetTotalSize
GetFileSize

cd C:\EaseSource\Source-ease_main-JUNO\Common\EASEClass

GetTotalSize
GetFileSize

cd C:\EaseSource\Source-ease_main-C7.7\Common\EASEClass

cd C:\EaseSource\Source-ease_main-C7.8\Common\EASEClass

cd C:\EaseSource\Source-MES-master\Common\EASEClass

cd C:\EaseSource\Source-MES-MES-Galaxy\Common\EASEClass


