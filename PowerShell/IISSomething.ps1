
$iis = [ADSI]"IIS://$env:COMPUTERNAME/W3SVC/1/ROOT"

# list directories
$iis.Children | select name,@{n='DirBrowsingEnabled';e={$_.psbase.InvokeGet('EnableDirBrowsing')}}

# disable directory browsing on all directories
$iis.Children | where {!$_.psbase.InvokeGet('EnableDirBrowsing')} | foreach {
    $_.put('EnableDirBrowsing',$false)
    $_.psbase.CommitChanges()
}

$objSites = Get-WebSite

$objSites | % {
[xml]$xmlDirBrowsing = C:\Windows\System32\inetsrv\appcmd.exe list config /section:directoryBrowse
$browsenode=$xmlDirBrowsing.SelectNodes(“//directoryBrowse”)
$result = foreach ($node in $browsenode) { $node.enabled }
Else {
}
}

$objSites = Get-WebSite

$objSites | % {
[xml]$xmlDirBrowsing = C:\Windows\System32\inetsrv\appcmd.exe list config /section:directoryBrowse
$browsenode=$xmlDirBrowsing.SelectNodes(“//directoryBrowse”)
$result = foreach ($node in $browsenode) { $node.enabled }
}