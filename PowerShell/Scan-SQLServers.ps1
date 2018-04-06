$infile = ".\" + $args[0] + ".csv"
$outfile = ".\" + $args[0] + "_out.csv" 
$ErrorActionPreference = "SilentlyContinue"
"Server, Instance, Version, Edition" | Out-File $outfile
foreach ($Server in import-csv $infile)
{  
$server  
$version = $null  
$reg = $null  
$reg = [Microsoft.Win32.RegistryKey]::OpenRemoteBaseKey('LocalMachine', $Server.Name)   
$regkey = $null  
$regkey = $reg.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server\\Instance Names\\SQL")   
IF ($regkey)
{  
foreach ($regInstance in $regkey.GetValueNames())  
{    
$regInstanceData = $regKey.GetValue($regInstance)    
$versionKey = $reg.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server\\$regInstanceData\\Setup")    
$version = $versionKey.GetValue('PatchLevel')    
$edition = $versionKey.GetValue('Edition')    
$VersionInfo = $Server.Name + ',' + $regInstance + ',' + $version + ',' + $edition    
$versionInfo | Out-File $outfile -Append  }}
   IF ($version -eq $null)  
   {    
   $regkey = $null    
   $regkey = $reg.OpenSubkey("SOFTWARE\\Microsoft\\Microsoft SQL Server")    
   IF ($regkey)
   {    
   foreach ($regInstance in $regkey.GetValue('InstalledInstances'))    
   {      
   IF ($regInstance -eq "MSSQLServer")      
   {    
   $versionKey = $reg.OpenSubkey("SOFTWARE\\Microsoft\\Microsoft SQL Server\\MSSQLSERVER\Setup")    
   IF ($versionKey.GetValue('PatchLevel') -eq $null)    
   {          
   $versionKey = $reg.OpenSubKey("SOFTWARE\\Microsoft\\MSSQLServer\\Setup")          
   IF ($versionKey.GetValue('PatchLevel') -eq $null)          
   {            
   $versionkey = $reg.OpenSubkey("SOFTWARE\\Microsoft\MSSQLServer\\MSSQLServer\\CurrentVersion")          
   }    
   }      
   }     
   ELSE      
   {        
   $versionKey = $reg.OpenSubkey("SOFTWARE\\Microsoft\\Microsoft SQL Server\\$regInstance\\Setup")      
   }      
   IF ($versionkey)      
   {        
   $version = $versionKey.GetValue('PatchLevel')        
   $edition = $versionKey.GetValue('Edition')        
   IF ($version -eq $null)        
   {          
   $version = $versionKey.GetValue('CSDVersion')          
   IF ($version -eq $null)          
   {        
   $version = $versionKey.GetValue('CurrentVersion')      
   }        
   }    
   IF ($Edition -eq $null){$Edition = "Unknown"}        
   IF ($version)        
   {          
   $VersionInfo = $Server.Name + ',' + $regInstance + ',' + $version + ',' + $edition          
   $versionInfo | Out-File $outfile -Append        
   }      
   }    
   }
   }  
   }
   }