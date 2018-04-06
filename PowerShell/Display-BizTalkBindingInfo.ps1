# Query BizTalk Binding XML file
#
# TODO:
#	Handle values with embedded commas
#	Handle empty comments more gracefully

param
(
	$xmlbindingfile=$(throw 'Must provide XML binding file'),
	$env=$(throw 'Must provide env: iDev,iTest,Stage,Prod'),
    [string] $style,
    [string] $all
)

$xml = $null	# in case we have it set.  Learn how to do scope

$xml = [xml] (get-content $xmlbindingfile)

# Show what is in file
# $xml.BindingInfo

$xmlbindingfile

$ModuleRefs = ($xml.BindingInfo.ModuleRefCollection.ModuleRef | sort -Property Name)

$ReceivePorts = ($xml.BindingInfo.ReceivePortCollection.ReceivePort | sort -Property Name)

$SendPorts = ($xml.BindingInfo.SendPortCollection.SendPort | sort -Property Name)

# Display Info about ModuleRefs

if ($style -ne "csv")
{
	"********************"
	"ModuleRefs"
	"********************"
	""
}
# else
# {
	# "ModuleRef,Env,FullName"
# }

foreach ($mr in $ModuleRefs)
{
	if ($style -eq "csv")
	{
		"ModuleRef," + $env + "," + $mr.FullName
	}
	else
	{
		$mr.FullName
	}
}

# Display Info about ReceivePorts

if ($style -ne "csv")
{
	""
	"********************"
	"ReceivePorts"
	"********************"
	""	
}
# else
# {
	# "ReceivePort,Env,Name,ApplicationName,Description,TransmitPipeLine,Address"
# }

foreach ($rp in $ReceivePorts)
{
	$rcvlocations = ($rp.ReceiveLocations.ReceiveLocation | Sort -Property Address)
	
	if ($style -eq "csv")
	{
		$output = "ReceivePort," + $env + "," + $rp.Name + "," + $rp.ApplicationName + "," + $rp.Description + "," + $rp.TransmitPipeLine.Name
		
		foreach ($rl in $rcvlocations)
		{
			$output + "," + $rl.Address
		}		
	}
	else
	{

		$rp.Name
		"  ApplicationName," + $rp.ApplicationName	
		"  Description," + $rp.Description
		"  TransmitPipeline.Name," + $rp.TransmitPipeLine.Name
		
		foreach ($rl in $rcvlocations)
		{
			"  ReceiveLocation.Address," + $rl.Address
		}

		""
	}
}

# Display Info about SendPorts

if ($style -ne "csv")
{
	""
	"********************"
	"SendPorts"
	"********************"
	""
}
# else
# {
	# "SendPort,Env,Name,ApplicationName,Description,Address,SendHandler"
# }

foreach ($sp in $SendPorts)
{
	if ($style -eq "csv")
	{
		$output = "SendPort," + $env + "," + $sp.Name + "," + $sp.ApplicationName + "," + $sp.Description + "," + $sp.PrimaryTransport.Address + "," + $sp.PrimaryTransport.SendHander.Name
		$output
	}
	else
	{
		$sp.Name
		"  ApplicationName," + $sp.ApplicationName
		"  Description," + $sp.Description
		"  PrimaryTransport.Address," + $sp.PrimaryTransport.Address
		"  PrimaryTransport.SendHandler," + $sp.PrimaryTransport.SendHandler.Name
		""
	}
}