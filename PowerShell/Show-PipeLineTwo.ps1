#{
param(
 $a,  
 $b
)
$SCRIPTNAME = $myInvocation.InvocationName
$SCRIPTPATH = & { $myInvocation.ScriptName }

function foo() 
{
begin{ "$SCRIPTNAME Begin" }
#{ "$SCRIPTNAME Begin" }
process{"$SCRIPTNAME Process"}
end{ "$SCRIPTNAME End" }
}
foo