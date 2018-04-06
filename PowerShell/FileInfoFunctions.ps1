# NB.  There is a Pscx version which is better.
function Get-FileVersionInfo1            
{            
  param(            
    [Parameter(Mandatory=$true)]            
     [string]$FileName)            

  if(!(test-path $filename)) {            
  write-host "File not found"            
  return $null            
  }            

  return [System.Diagnostics.FileVersionInfo]::GetVersionInfo($FileName)            
}


