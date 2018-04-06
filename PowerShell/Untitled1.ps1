# example usage: dir *.exe,*.dll | Get-PEKind
function Get-PEKind {
    Param(
      [Parameter(Mandatory=$True,ValueFromPipeline=$True)]
      [System.IO.FileInfo]$assemblies
    )

    Process {
        foreach ($assembly in $assemblies) {
            $peKinds = new-object Reflection.PortableExecutableKinds
            $imageFileMachine = new-object Reflection.ImageFileMachine
            try
            {
                $a = [Reflection.Assembly]::LoadFile($assembly.Fullname)
                $a.ManifestModule.GetPEKind([ref]$peKinds, [ref]$imageFileMachine)
            }
            catch [System.BadImageFormatException]
            {
                $peKinds = [System.Reflection.PortableExecutableKinds]"NotAPortableExecutableImage"
            }

            $o = New-Object System.Object
            $o | Add-Member -type NoteProperty -name File -value $assembly
            $o | Add-Member -type NoteProperty -name PEKind -value $peKinds
            Write-Output $o
        }
    }
}