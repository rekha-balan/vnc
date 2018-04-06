# Create Symbol stuff for debugging

param
(
    [string] $DebugDir = "F:"
)

mkdir ($DebugDir + "\Dbg")
mkdir ($DebugDir + "\Dbg\Src")
mkdir ($DebugDir + "\Dbg\Sym")
mkdir ($DebugDir + "\Dbg\SymCache")

# Create user level environment variables

#[Environment]::SetEnvironmentVariable("_NT_SOURCE_PATH", "$DebugDir\Dbg\Src", "User")
#[Environment]::SetEnvironmentVariable("_NT_SYMBOL_PATH", "SRV*$DebugDir\Dbg\Sym*http://msdl.microsoft.com/download/symbols", "User")
#[Environment]::SetEnvironmentVariable("_NT_SYMCACHE_PATH", "$DebugDir\Dbg\SymCache", "User")

# Create Machine level environment variables

[Environment]::SetEnvironmentVariable("_NT_SOURCE_PATH", "$DebugDir\Dbg\Src", "Machine")
[Environment]::SetEnvironmentVariable("_NT_SYMBOL_PATH", "SRV*$DebugDir\Dbg\Sym*http://msdl.microsoft.com/download/symbols", "Machine")
[Environment]::SetEnvironmentVariable("_NT_SYMCACHE_PATH", "$DebugDir\Dbg\SymCache", "Machine")