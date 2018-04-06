#
# Split-TextFile.ps1
#
# By David Barrett, Microsoft Ltd. 2016. Use at your own risk.  No warranties are given.
#
#  DISCLAIMER:
# THIS CODE IS SAMPLE CODE. THESE SAMPLES ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND.
# MICROSOFT FURTHER DISCLAIMS ALL IMPLIED WARRANTIES INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OF MERCHANTABILITY OR OF FITNESS FOR
# A PARTICULAR PURPOSE. THE ENTIRE RISK ARISING OUT OF THE USE OR PERFORMANCE OF THE SAMPLES REMAINS WITH YOU. IN NO EVENT SHALL
# MICROSOFT OR ITS SUPPLIERS BE LIABLE FOR ANY DAMAGES WHATSOEVER (INCLUDING, WITHOUT LIMITATION, DAMAGES FOR LOSS OF BUSINESS PROFITS,
# BUSINESS INTERRUPTION, LOSS OF BUSINESS INFORMATION, OR OTHER PECUNIARY LOSS) ARISING OUT OF THE USE OF OR INABILITY TO USE THE
# SAMPLES, EVEN IF MICROSOFT HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES. BECAUSE SOME STATES DO NOT ALLOW THE EXCLUSION OR LIMITATION
# OF LIABILITY FOR CONSEQUENTIAL OR INCIDENTAL DAMAGES, THE ABOVE LIMITATION MAY NOT APPLY TO YOU.

param (
	[Parameter(Position=0,Mandatory=$True,HelpMessage="Source file that will be split")]
	[ValidateNotNullOrEmpty()]
	[string]$SourceFile,

	[Parameter(Mandatory=$False,HelpMessage="Output path for the split file (if blank, same folder as source file is used)")]
	[string]$OutputPath,

	[Parameter(Mandatory=$False,HelpMessage="Output filename for the split file (number will be appended)")]
	[string]$OutputFilename,

	[Parameter(Mandatory=$False,HelpMessage="Maximum size of split file (in bytes) - defaults to 50Mb")]
	[int]$MaxFileSize = 52428800, # 50Mb default

	[Parameter(Mandatory=$False,HelpMessage="If specified, will perform a text search during splitting and highlight files that contain this text")]
	[string]$SearchFor
)


$reader = $null
$reader = New-Object System.IO.StreamReader($SourceFile)
if ($reader -eq $null)
{
    Write-Host "Failed to open source file" -ForegroundColor Red
    exit
}

if ([String]::IsNullOrEmpty($OutputPath))
{
    $OutputPath = Split-Path $SourceFile
}
if ($OutputPath.EndsWith("\"))
{
    $OutputPath = $OutputPath.Substring(0, $OutputPath.Length-1)
}

if ([String]::IsNullOrEmpty($OutputFilename))
{
    $OutputFilename = Split-Path $SourceFile -leaf
}

$OutputFile = [System.IO.Path]::GetFileNameWithoutExtension($OutputFilename)
$OutputExt = [System.IO.Path]::GetExtension($OutputFilename)
$index = 1

# Now split the file
$curFile = [String]::Format("{0}\{1}{2}{3}", $OutputPath, $OutputFile, $index, $OutputExt)
Write-Host "Writing to $curFile" -ForegroundColor Gray
$writer = New-Object System.IO.StreamWriter($curFile, $false)
$written = 0
$found = $false
$search = ![String]::IsNullOrEmpty($SearchFor)
$lineNumber = 1

while (!$reader.EndOfStream)
{
    $line = $reader.ReadLine()
    if (($written + $line.Length) -gt $MaxFileSize)
    {
        # Start new file
        $writer.Close()
        $index++
        $written = 0
        $lineNumber = 1
        $found = $false
        $curFile = [String]::Format("{0}\{1}{2:0000}{3}", $OutputPath, $OutputFile, $index, $OutputExt)
        Write-Host "Writing to $curFile" -ForegroundColor Gray
        $writer = New-Object System.IO.StreamWriter($curFile, $false)
    }

    if ( $search )
    {
        if ($line.Contains($SearchFor))
        {
            if (!$found)
            {
                $found = $true
                Write-Host "$curFile contains`"$SearchFor`"" -ForegroundColor Green
            }
            Write-Host "Line $($lineNumber): found `"$SearchFor`"" -ForegroundColor Green
        }
    }

    $writer.WriteLine($line)
    $written += $line.Length
    $lineNumber++
}

$writer.Close()