Get-ADOrganizationalUnit -Filter 'Name -like "*"' | 
    Foreach-Object `
    { 
        Write-Output ($_.DistinguishedName).Replace(",", "-") ; `
        Get-ADObject -Filter 'Name -like "*" -and ObjectClass -eq "computer"' -SearchBase $_.DistinguishedName -Properties * |
        Sort-Object -Property Name | 
        Foreach-Object `
        {
            ",{0},{1},{2},{3},{4}" -f $_.Name, $_.DNSHostName, $_.OperatingSystem, $_.Description, $_.Created
        }
    }

