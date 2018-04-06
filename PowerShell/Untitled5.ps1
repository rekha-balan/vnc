[reflection.assemblyname]::GetAssemblyName("${pwd}\EaseCore.dll") | fl
[reflection.assemblyname]::GetAssemblyName("${pwd}\Oracle.DataAccess.dll") | fl


[reflection.assemblyname]::GetAssemblyName("${pwd}\Oracle.ManagedDataAccess.dll") | fl

function Get-ProcessorArch {
    Param(
      [Parameter(Mandatory=$True,ValueFromPipeline=$True)]
      [System.IO.FileInfo]$assemblies
    )

    Process {
        foreach ($assembly in *.dll) {
            [reflection.assemblyname]::GetAssemblyName("${pwd}\$assembly")


        }
    }
}

dir e*.dll | [reflection.assemblyname]::GetAssemblyName("${pwd}\$_)