################################################################################
#
# GetBizTalkInfo.ps1
#
# Note:
#	This script is under source code control.  Modifications should be 
#	checked into the VSS repository located at 
#		\\life.pacificlife.net\life\it\vss\TechOffice
#	under a project 
#		$PowerShell/Scripts
#
# Script to automate the collection of information from a BizTalk server.
#
# Last Update:
#
# v1.0 Christopher Rhodes, February 2010, Pacific Life Insurance
#
################################################################################

# This needs to be first

param(
    [string] $action=$(throw 'Need action'),
    [string] $style,
    [string] $all,
    [string] $name,
    [string] $status
)

# Change the size of the output so we can have wider lines

$pshost = get-host
$pswindow = $pshost.ui.rawui

$newbufsize = $pswindow.buffersize
$newbufsize.height = 3000
$newbufsize.width = 175

$pswindow.buffersize = $newbufsize

$newwindowsize = $pswindow.windowsize

$newwindowsize.height = 50
$newwindowsize.width = 175

$pswindow.windowsize = $newwindowsize

#
# Declare out parameters: the action to take
#

#
# Helper function to list all WMI objects of a given type
#

function bts-list-objs($kind)
{
    get-wmiobject $kind `
        -namespace 'root\MicrosoftBizTalkServer'
}

#function Display_MSBTS_AdapterSetting_Info()
#{
#    bts-list-objs MSBTS_AdapterSetting | %{ bts-display-adaptersetting $_ }
#}

##############################
# MSBTS_AdapterSetting
##############################

function Display_MSBTS_AdapterSetting_Info()
{
    $wmiobject = "MSBTS_AdapterSetting"

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap `
                    Caption, Description, MgmtDbNameOverride, MgmtDbServerOverride, SettingID

            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.Name};              Label="Name";              width=30}, `
                    @{Expression={$_.Comment};           Label="Comment";           width=50}, `
                    @{Expression={$_.DefaultInBoundCfg}; Label="DefaultInBoundCfg"; width=50}, `
                    @{Expression={$_.MgmtCLSID};         Label="MgmtCLSID";         width=40}


                bts-list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression | format-list `
                Caption, Comment, Constraints, DefaultInboundCfg, Description, MgmtCLSID, MgmtDbNameOverride, MgmtDbServerOverride, Name, SettingID
        }
}

#
# Display AdapterSetting information to the console
#

function bts-display-adaptersetting($adaptersetting)
{
    'AdapterSetting: '

    $adaptersetting | format-list `
        Caption, Comment, Constraints, DefaultInboundCfg, DefaultOutputCfg, Description, `
        MgmtCLSID, MgmtDbNameOverride, MgmtDbServerOverride, Name, SettingID

    $sendHandlers = @(bts-get-related $adaptersetting.Name MSBTS_SendHandler2)

    if ($sendHandlers.Length -gt 0)
    {
        'Send Handlers:'
        for ( $i = 0 ; $i -lt $sendHandlers.Length ; $i++ )
        {
             '    ' + $sendHandlers[$i].HostName
        }
    }

    $recvHandlers = @(bts-get-related $adaptersetting.Name MSBTS_ReceiveHandler)

    if ($recvHandlers.Length -gt 0)
    {
        'Send Handlers:'
        for ( $i = 0 ; $i -lt $recvHandlers.Length ; $i++ )
        {
             '    ' + $recvHandlers[$i].HostName
        }
    }

    ''
}

#
# Get objects related to an adapter
#

function bts-get-related($adapterName, $kind)
{
    get-wmiobject $kind `
        -namespace 'root\MicrosoftBizTalkServer' `
        -filter "AdapterName='$adapterName'"
}

##############################
# MSBTS_DeploymentService
##############################

function Display_MSBTS_DeploymentService_Info()
{
    $wmiobject = "MSBTS_DeploymentService"

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap `
                    Caption, Description, MgmtDbNameOverride, MgmtDbServerOverride, SettingID

            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.Name};         Label="Name";         width=25}

                bts-list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression | format-list `
                Caption, Description, MgmtDbNameOverride, MgmtDbServerOverride, SettingID
        }
}

##############################
# MSBTS_GroupSetting
##############################

function Display_MSBTS_GroupSetting_Info()
{
    $wmiobject = "MSBTS_GroupSetting"

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap `
        BamDBName, BamDBServerName, BizTalkAdministratorGroup, BizTalkOperatorGroup, Caption, `
        ConfigurationCacheRefreshInterval, Description, GlobalTrackingOption, LMSFragementSize, LMSThreshold, `
        MajorVersion, MgmtDbNameOverride, MgmtDbServerOverride, MinorVersion, Name, RuleEngineDBName, `
        RuleEngineDBServerName, `SettingID, SignCertComment, SignCertThumbprint, SSOServerName, `
        SubscriptionDBName, SubscriptionDBServerName, TrackingAnalysisDBName, TrackingAnalysisDBServerName, `
        TrackingDBName, TrackingDBServerName

            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.Name};         Label="Name";         width=25}

                bts-list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression | format-list `
        BamDBName, BamDBServerName, BizTalkAdministratorGroup, BizTalkOperatorGroup, Caption, `
        ConfigurationCacheRefreshInterval, Description, GlobalTrackingOption, LMSFragementSize, LMSThreshold, `
        MajorVersion, MgmtDbNameOverride, MgmtDbServerOverride, MinorVersion, Name, RuleEngineDBName, `
        RuleEngineDBServerName, `SettingID, SignCertComment, SignCertThumbprint, SSOServerName, `
        SubscriptionDBName, SubscriptionDBServerName, TrackingAnalysisDBName, TrackingAnalysisDBServerName, `
        TrackingDBName, TrackingDBServerName
        }
}


#
# Display GroupSetting information to the console
#

function bts-display-groupsetting($groupsetting)
{
    'GroupSetting: '

    $groupsetting | format-list `
        BamDBName, BamDBServerName, BizTalkAdministratorGroup, BizTalkOperatorGroup, Caption, `
        ConfigurationCacheRefreshInterval, Description, GlobalTrackingOption, LMSFragementSize, LMSThreshold, `
        MajorVersion, MgmtDbNameOverride, MgmtDbServerOverride, MinorVersion, Name, RuleEngineDBName, `
        RuleEngineDBServerName, `SettingID, SignCertComment, SignCertThumbprint, SSOServerName, `
        SubscriptionDBName, SubscriptionDBServerName, TrackingAnalysisDBName, TrackingAnalysisDBServerName, `
        TrackingDBName, TrackingDBServerName
}

##############################
# MSBTS_Host
##############################

function Display_MSBTS_Host_Info()
{
    $wmiobject = "MSBTS_Host"

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts-list-objs MSBTS_Host | sort-object $sortexpression | format-table -Wrap `
                    AuthTrusted, Caption, ClusterResourceGroupName, DecryptCertComment, DecryptCertThumbPrint, `
                    Description, HostTracking, HostType, InstallDate, IsDefault, LastUsedLogon, `
                    MgmtDbNameOVerride, MgmtDbServerOverride, Name, NTGroupName, Status

            }
            else
            {             
                $formatexpression = `
                    @{Expression={$_.AuthTrusted};  Label="AuthTrusted";  width=11; Alignment="Center"}, `
                    @{Expression={$_.HostTracking}; Label="HostTracking"; width=12; Alignment="Center"}, `
                    @{Expression={$_.HostType};     Label="HostType";     width=8;  Alignment="Center"},  `
                    @{Expression={$_.IsDefault};    Label="IsDefault";    width=9;  Alignment="Center"},  `
                    @{Expression={$_.Name};         Label="Name";         width=30}, `
                    @{Expression={$_.NTGroupName};  Label="NTGroupName";  width=40}

                bts-list-objs MSBTS_Host | sort-object $sortexpression | format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs MSBTS_Host | format-list `
                AuthTrusted, Caption, ClusterResourceGroupName, DecryptCertComment, DecryptCertThumbPrint, `
                Description, HostTracking, HostType, InstallDate, IsDefault, LastUsedLogon, `
                MgmtDbNameOVerride, MgmtDbServerOverride, Name, NTGroupName, Status
        }
}

#
# Display Host information to the console
#
# NB.  $host is a keyword so we use $hostinfo instead :)

function bts-display-host($hostinfo)
{
    $hostinfo | format-list `
        AuthTrusted, Caption, ClusterResourceGroupName, DecryptCertComment, DecryptCertThumbPrint, `
        Description, HostTracking, HostType, InstallDate, IsDefault, LastUsedLogon, `
        MgmtDbNameOVerride, MgmtDbServerOverride, Name, NTGroupName, Status
}

##############################
# MSBTS_HostInstance
##############################

function Display_MSBTS_HostInstance_Info()
{
    $wmiobject = "MSBTS_HostInstance"

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap `
                   Caption, ConfigurationState, Description, HostName, HostType, InstallDate, IsDisabled, `
			Logon, MgmtDBNameOverride, MgmtDBServerOverride, Name, NTGroupName, RunningServer, `
			ServiceState, Status, UniqueID


            }
            else
            {            
                $formatexpression = `
                    @{Expression={$_.HostName};      Label="HostName";      width=30}, `
                    @{Expression={$_.Name};          Label="Name";          width=70}, `
                    @{Expression={$_.UniqueID};      Label="UniqueID";      width=40}

                bts-list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression | format-list `
                   Caption, ConfigurationState, Description, HostName, HostType, InstallDate, IsDisabled, `
			Logon, MgmtDBNameOverride, MgmtDBServerOverride, Name, NTGroupName, RunningServer, `
			ServiceState, Status, UniqueID
        }
}

##############################
# MSBTS_HostInstanceSetting
##############################

function Display_MSBTS_HostInstanceSetting_Info()
{
    $wmiobject = "MSBTS_HostInstanceSetting"

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap `
        Caption, ConfigurationState, Description, HostName, HostType, InstallDate, IsDisabled, Logon, `
        MgmtDbNameOverride, MgmtDbServerOverride, Name, NTGroupName, RunningServer, SettingID

            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.HostName};            Label="HostName";            width=25}, `
                    @{Expression={$_.Name};                Label="Name";                width=60}, `
                    @{Expression={$_.ConfigurationState};  Label="ConfigurationState";  width=20;  Alignment="Center"}, `
                    @{Expression={$_.HostType};            Label="HostType";            width=10;  Alignment="Center"}, `
                    @{Expression={$_.IsDisabled};          Label="IsDisabled";          width=10;  Alignment="Center"}

                bts-list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression | format-list `
        Caption, ConfigurationState, Description, HostName, HostType, InstallDate, IsDisabled, Logon, `
        MgmtDbNameOverride, MgmtDbServerOverride, Name, NTGroupName, RunningServer, SettingID
        }
}

##############################
# MSBTS_HostQueue
##############################

function Display_MSBTS_HostQueue_Info()
{
    $wmiobject = "MSBTS_HostQueue"

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap `
                    Caption, Description, HostName, InstallDate, MgmtDbNameOverride, MgmtDbServerOverride, Name, Status
            }
            else
            {
# Update this with list of columns of interest
""
"HostName"
"------------------------"
                
                $formatexpression = `
                    @{Expression={$_.HostName};         Label="HostName";         width=25}

                bts-list-objs $wmiobject | sort-object $sortexpression | format-table -HideTableHeaders -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression | format-list `
                Caption, Description, HostName, InstallDate, MgmtDbNameOverride, MgmtDbServerOverride, Name, Status
        }
}

##############################
# MSBTS_HostSetting
##############################

function Display_MSBTS_HostSetting_Info()
{
    $wmiobject = "MSBTS_HostSetting"

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap `
                    AuthTrusted, Caption, ClusterResourceGroupName, DBQueueSizeThreshold, DecryptCertComment, DecryptCertThumbprint, DeliveryQueueSize, Description, `
        GlobalMemoryThreshold, HostTRacking, HostType, InflightMessageThreshold, IsDefault, IsHost32BitOnly, LastUsedLogon, MessageDeliverySampleSpaceSize, MessageDeliverySampleSpaceWindow, `
        MessageDeliveryOverdriveFactor, MessageDeliveryMaximumDelay, MessagePublishSampleSpaceSize, MessagePublishSampleSpaceWindow, MessagePublishOverdriveFactor, `
        MessagePublishMaximumDelay, MgmtDbNameOverride, MgmtDbServerOverride, Name, NTGroupName, SettingID, ThreadPoolSize, ThreadTheshold, ProcessMemoryThreshold, Status


            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.Name};         Label="Name";         width=35}, `
		    @{Expression={$_.IsDefault};    Label="IsDefault";    width=10;   Alignment="Center"}

                bts-list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression | format-list `
                AuthTrusted, Caption, ClusterResourceGroupName, DBQueueSizeThreshold, DecryptCertComment, DecryptCertThumbprint, DeliveryQueueSize, Description, `
        GlobalMemoryThreshold, HostTRacking, HostType, InflightMessageThreshold, IsDefault, IsHost32BitOnly, LastUsedLogon, MessageDeliverySampleSpaceSize, MessageDeliverySampleSpaceWindow, `
        MessageDeliveryOverdriveFactor, MessageDeliveryMaximumDelay, MessagePublishSampleSpaceSize, MessagePublishSampleSpaceWindow, MessagePublishOverdriveFactor, `
        MessagePublishMaximumDelay, MgmtDbNameOverride, MgmtDbServerOverride, Name, NTGroupName, SettingID, ThreadPoolSize, ThreadTheshold, ProcessMemoryThreshold, Status

        }
}

##############################
# MSBTS_MsgBoxSetting
##############################

function Display_MSBTS_MsgBoxSetting_Info()
{
    $wmiobject = "MSBTS_MsgBoxSetting"

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap `
                Caption, Description, DisableNewMessagePublication, IsMasterMsgBox, `
		MgmtDbNameOverrride, MgmtDbServerOVerride, MsgBoxDBName, MsgBoxDBServerName, SettingID

            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.IsMasterMsgBox};     Label="IsMasterMsgBox";     width=14}, `
                    @{Expression={$_.MsgBoxDBName};       Label="MsgBoxDBName";       width=20}, `
                    @{Expression={$_.MsgBoxDBServerName}; Label="MsgBoxDBServerName"; width=25}

                bts-list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression | format-list `
                Caption, Description, DisableNewMessagePublication, IsMasterMsgBox, `
		MgmtDbNameOverrride, MgmtDbServerOVerride, MsgBoxDBName, MsgBoxDBServerName, SettingID

        }
}

##############################
# MSBTS_Orchestration
##############################

function Display_MSBTS_Orchestration_Info()
{
    $wmiobject = "MSBTS_Orchestration"

    $sortexpression = `
	@{Expression={$_.HostName}; Ascending=$true}, `
	@{Expression={$_.AssemblyName}; Ascending=$true}, `
	@{Expression={$_.Name}; Ascending=$true}

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap `
			AssemblyCulture, AssemblyName, AssemblyPublicKeyToken, AssemblyVersion, Caption, Description, HostName, `
        		InstallDate, MgmtDbNameOverrride, MgmtDbServerOverride, Name, OrchestrationStatus, Status

            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.Name};                Label="Name";                 width=70}, `
                    @{Expression={$_.HostName};            Label="HostName";             width=25}, `
                    @{Expression={$_.OrchestrationStatus}; Label="OrchStatus";  width=10;  Alignment="Center"}, `
                    @{Expression={$_.AssemblyName};        Label="AssemblyName";         width=45}


                bts-list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression | format-list `
                AssemblyCulture, AssemblyName, AssemblyPublicKeyToken, AssemblyVersion, Caption, Description, HostName, `
        	InstallDate, MgmtDbNameOverrride, MgmtDbServerOverride, Name, OrchestrationStatus, Status
        }
}

##############################
# MSBTS_ReceiveHandler
##############################

function Display_MSBTS_ReceiveHandler_Info()
{
    $wmiobject = "MSBTS_ReceiveHandler"
        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap `
                AdapterName, Caption, CustomCfg, Description, HostName, HostNameToSwitchTo, `
		MgmtDbNameOverrride, MgmtDbServerOVerride, SettingId

            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.HostName};         Label="HostName";         width=25}

                bts-list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression | format-list `
                AdapterName, Caption, CustomCfg, Description, HostName, HostNameToSwitchTo, `
		MgmtDbNameOverrride, MgmtDbServerOVerride, SettingId

        }
}

##############################
# MSBTS_ReceiveLocation
##############################

function Display_MSBTS_ReceiveLocation_Info()
{
    $wmiobject = "MSBTS_ReceiveLocation"

    $sortexpression = `
	@{Expression={$_.HostName}; Ascending=$true}

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap `
		ActiveStartDT, ActiveStopDT, AdapterName, Caption, CustomCfg, EncyrptionCert, HostName, `
		InboundAddressableUrl, InboundTransportUrl, IsDisabled, IsPrimary, `
        	MgmtDbNameOverrride, MgmtDbServerOVerride, Name, OperatingWindowEnabled, PipelineName, `
		ReceivePortName, SendPipeline, SendPipelineData, SettingId, SrvWinStartDT, SrvWinStopDT, `
		StartDateEnabled, StopDateEnabled
            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.HostName};              Label="HostName";              width=15}, `
                    @{Expression={$_.AdapterName};           Label="AdapterName";           width=11}, `
                    @{Expression={$_.CustomCfg};             Label="CustomCfg";             width=30}, `
                    @{Expression={$_.InboundTransportURL};   Label="InboundTransportURL";   width=30}, `
                    @{Expression={$_.Name};                  Label="Name";                  width=20}, `
                    @{Expression={$_.ReceivePortName};       Label="ReceivePortName";       width=25}, `
                    @{Expression={$_.SendPipeline};          Label="SendPipeline";          width=25}

                bts-list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression | format-list `
		ActiveStartDT, ActiveStopDT, AdapterName, Caption, CustomCfg, EncyrptionCert, HostName, `
		InboundAddressableUrl, InboundTransportUrl, IsDisabled, IsPrimary, `
        	MgmtDbNameOverrride, MgmtDbServerOVerride, Name, OperatingWindowEnabled, PipelineName, `
		ReceivePortName, SendPipeline, SendPipelineData, SettingId, SrvWinStartDT, SrvWinStopDT, `
		StartDateEnabled, StopDateEnabled

        }
}

##############################
# MSBTS_ReceiveLocationOrchestration
##############################

function Display_MSBTS_ReceiveLocationOrchestration_Info()
{
    $wmiobject = "MSBTS_ReceiveLocationOrchestration"

    $sortexpression = `
	@{Expression={$_.ReceiveHostName}; Ascending=$true}

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap `
                AdapterName, Caption, Description, InboundTransportUrl, IsDisabled, IsPrimary, `
        	MgmtDbNameOverrride, MgmtDbServerOVerride, OrchestrationAssemblyName, `
		OrchestrationAssemblyVersion, OrchestrationHostName, OrchestrationName, OrchestrationStatus, `
        	PipelineName, ReceiveHosttName, ReceiveLocationName, ReceivePortName, SettingId

            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.ReceiveHostName};           Label="ReceiveHostName";           width=20}, `
                    @{Expression={$_.OrchestrationName};         Label="OrchestrationName";         width=20}, `
                    @{Expression={$_.InboundTransportUrl};       Label="InboundTransportUrl";       width=20}, `
                    @{Expression={$_.IsDisabled};                Label="IsDisabled";                width=10}, `
                    @{Expression={$_.OrchestrationAssemblyName}; Label="OrchestrationAssemblyName"; width=20}, `
                    @{Expression={$_.ReceiveHostName};           Label="ReceiveHostName";           width=20}, `
                    @{Expression={$_.ReceiveLocationName};       Label="ReceiveLocationName";       width=20}, `
                    @{Expression={$_.PipelineName};              Label="PipelineName";              width=20}, `
                    @{Expression={$_.ReceivePortName};           Label="ReceivePortName";           width=20}

                bts-list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression | format-list `
                AdapterName, Caption, Description, InboundTransportUrl, IsDisabled, IsPrimary, `
        	MgmtDbNameOverrride, MgmtDbServerOVerride, OrchestrationAssemblyName, `
		OrchestrationAssemblyVersion, OrchestrationHostName, OrchestrationName, OrchestrationStatus, `
        	PipelineName, ReceiveHosttName, ReceiveLocationName, ReceivePortName, SettingId
        }
}

##############################
# MSBTS_ReceivePort
##############################

function Display_MSBTS_ReceivePort_Info()
{
    $wmiobject = "MSBTS_ReceivePort"

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression |format-table -Wrap `
                Caption, Description, InboundTransforms, IsTwoWay, MgmtDbNameOverride, MgmtDbServerOverride, `
		OutboundTransforms, PrimaryReceiveLocation, RouteFailedMessage, SendPipeLine, SettingID, Tracking

            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.Name};                   Label="Name";                   width=50}, `
                    @{Expression={$_.PrimaryReceiveLocation}; Label="PrimaryReceiveLocation"; width=50}

                bts-list-objs $wmiobject | sort-object $sortexpression |format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression |format-list `
                Caption, Description, InboundTransforms, IsTwoWay, MgmtDbNameOverride, MgmtDbServerOverride, `
		OutboundTransforms, PrimaryReceiveLocation, RouteFailedMessage, SendPipeLine, SettingID, Tracking
        }
}

#
#
# Display all information about a receive port
#

function bts-display-receiveport($port)
{
    $portname = $port.Name
    $portname

    $recvlocs = get-wmiobject MSBTS_ReceiveLocation `
        -namespace 'root\MicrosoftBizTalkServer' `
        -filter "ReceivePortName='$portname'"

#    $recvlocs | ft Name, AdapterName, InboundTransportURL, IsDisabled
    $recvlocs | format-list Name, AdapterName, InboundTransportURL, IsDisabled
}

##############################
# MSBTS_SendHandler
##############################

function Display_MSBTS_SendHandler_Info()
{
    $wmiobject = "MSBTS_SendHandler"
        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression |format-table -Wrap `
                AdapterName, Caption, CustomCfg, Description, HostName, HostNameToSwitchTo, IsDefault, `
		MgmtDbNameOverrride, MgmtDbServerOVerride, SettingId

            }
            else
            {                
                $formatexpression = `
                    @{Expression={$_.Name};         Label="Name";         width=25}

                bts-list-objs $wmiobject | sort-object $sortexpression |format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression |format-list `
                AdapterName, Caption, CustomCfg, Description, HostName, HostNameToSwitchTo, IsDefault, `
		MgmtDbNameOverrride, MgmtDbServerOVerride, SettingId

        }
}

##############################
# MSBTS_SendHandler2
##############################

function Display_MSBTS_SendHandler2_Info()
{
    $wmiobject = "MSBTS_SendHandler2"
        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression |format-table -Wrap `
                AdapterName, Caption, CustomCfg, Description, HostName, HostNameToSwitchTo, `
		MgmtDbNameOverrride, MgmtDbServerOVerride, SettingId

            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.Name};         Label="Name";         width=25}

                bts-list-objs $wmiobject | sort-object $sortexpression |format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression |format-list `
                AdapterName, Caption, CustomCfg, Description, HostName, HostNameToSwitchTo, `
		MgmtDbNameOverrride, MgmtDbServerOVerride, SettingId

        }
}

##############################
# MSBTS_SendPort
##############################

function Display_MSBTS_SendPort_Info()
{
    $wmiobject = "MSBTS_SendPort"

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression |format-table -Wrap `
                Caption, Description, EncryptionCert, Filter, InboundTransforms, IsDynamic, IsTwoWay, `
       		MgmtDbNameOverrride, MgmtDbServerOVerride, Name, OutboundTransforms, Priority, PTAddress, PTCustomCfg, PTFromTime, `
		PTOrderedDelivery, PTRetryCount, PTRetryInterval, PTServiceWindowENabled, PTToTime, PTTransportType, ReceivePipeline, `
		SendPipeline, SettingId, Status, STAddress, STCustomCfg, STFromTime, STOrderedDelivery, STRetryCount, STRetryInterval, `
		STServiceWindowEnabled, STToTime, STTransportType, Tracking, OrderedDelivery, StopSendingOnFailure, RouteFailedMessage

            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.Name};         Label="Name";         width=30}, `
                    @{Expression={$_.PTAddress};    Label="PTAddress";    width=50}, `
                    @{Expression={$_.PTCustomCfg};  Label="PTCustomCfg";  width=80}

                bts-list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression |format-list `
                Caption, Description, EncryptionCert, Filter, InboundTransforms, IsDynamic, IsTwoWay, `
       		MgmtDbNameOverrride, MgmtDbServerOVerride, Name, OutboundTransforms, Priority, PTAddress, PTCustomCfg, PTFromTime, `
		PTOrderedDelivery, PTRetryCount, PTRetryInterval, PTServiceWindowENabled, PTToTime, PTTransportType, ReceivePipeline, `
		SendPipeline, SettingId, Status, STAddress, STCustomCfg, STFromTime, STOrderedDelivery, STRetryCount, STRetryInterval, `
		STServiceWindowEnabled, STToTime, STTransportType, Tracking, OrderedDelivery, StopSendingOnFailure, RouteFailedMessage
        }
}

##############################
# MSBTS_SendPortGroup
##############################

function Display_MSBTS_SendPortGroup_Info()
{
    $wmiobject = "MSBTS_SendPortGroup"
        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression |format-table -Wrap `
                    Caption, Description, Filter, MgmtDbNameOverride, MgmtDbServerOverride, Name, SettingId, Status

            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.Name};         Label="Name";         width=25}

                bts-list-objs $wmiobject | sort-object $sortexpression |format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression |format-list `
                Caption, Description, Filter, MgmtDbNameOverride, MgmtDbServerOverride, Name, SettingId, Status
        }
}

#
#
# Display all information about a send port group
#

function bts-display-sendportgroup($sendportgroup)
{
    'SendPortGroup: '

    $sendportgroup | format-list Caption, Description, Filter, MgmtDbNameOverride, MgmtDbServerOverride, Name, SettingId, Status


    $groupname = $sendportgroup.Name

    $ports = get-wmiobject MSBTS_SendPortGroup2SendPort `
        -namespace 'root\MicrosoftBizTalkServer' `
        -filter "SendPortGroupName='$groupname'"

    $groupname

    foreach ( $port in $ports )
    {
        "`t" + $port.SendPortName
    }
}

##############################
# MSBTS_SendPortGroup2SendPort
##############################

function Display_MSBTS_SendPortGroup2SendPort_Info()
{
    $wmiobject = "MSBTS_SendPortGroup2SendPort"

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression |format-table -Wrap `
                Caption, Description, MgmtDbNameOverride, MgmtDbServerOverride, `
		SendPortGroupName, SendPortName, SettingId

            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.Name};         Label="Name";         width=25}

                bts-list-objs $wmiobject | sort-object $sortexpression |format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression | format-list `
                Caption, Description, MgmtDbNameOverride, MgmtDbServerOverride, `
		SendPortGroupName, SendPortName, SettingId

        }
}

##############################
# MSBTS_Server
##############################

function Display_MSBTS_Server_Info()
{
    $wmiobject = "MSBTS_Server"

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression |format-table -Wrap `
                    # Put list of columns here

            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.Name};         Label="Name";         width=25}

                bts-list-objs $wmiobject | sort-object $sortexpression |format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression |format-list `
                Caption, Description, InstallDate, MgmtDbNameOverride, MgmtDbServerOverride, Name, Status
        }
}


##############################
# MSBTS_ServerHost
##############################

function Display_MSBTS_ServerHost_Info()
{
    $wmiobject = "MSBTS_ServerHost"

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression |format-table -Wrap `
                    Caption, Description, HostName, InstallDate, IsMapped, MgmtDbNameOverride, MgmtDbServerOverride, `
		    Name, ServerName, Status

            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.HostName};   Label="HostName";    width=30}, `
                    @{Expression={$_.IsMapped};   Label="IsMapped";    width=10;  Alignment="Center"}, `
                    @{Expression={$_.ServerName}; Label="ServerName";  width=20}

                bts-list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression |format-list `
                Caption, Description, HostName, InstallDate, IsMapped, MgmtDbNameOverride, MgmtDbServerOverride, `
		Name, ServerName, Status

        }
}

##############################
# MSBTS_ServerSetting
##############################

function Display_MSBTS_ServerSetting_Info()
{
    $wmiobject = "MSBTS_ServerSetting"

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression |format-table -Wrap `
                    # Put list of columns here

            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.Name};         Label="Name";         width=25}

                bts-list-objs $wmiobject | sort-object $sortexpression |format-table -Wrap $formatexpression
            }
        }
        else
        {
            bts-list-objs $wmiobject | sort-object $sortexpression |format-list `
                Caption, Description, InstallDate, MgmtDbNameOverride, MgmtDbServerOverride, Name, Status
        }
}

##############################
# MSBTS_ServiceInstance
##############################

function Display_MSBTS_ServiceInstance_Info()
{
    $wmiobject = "MSBTS_ServiceInstance"

        if ($style -eq "table")
        {
            if ($all -eq "all")
            {
                bts_list-objs $wmiobject | sort-object $sortexpression |format-table -Wrap `
                ActivationTime, AssemblyCulture, AssemblyName, AssemblyPublicKeyToken, AssemblyVersion, `
		Caption, Description, ErrorCategory, ErrorDescription, ErrorID, HostName, InstallDate, `
		InstanceID, MgmtDbNameOverride, MgmtDbServerOverride, MsgBoxDBName, MsgBoxServerName, `
		Name, PendingOperation, PendingOperationTime, ServiceClass, ServiceClassID, ServiceName, `
		ServiceStatus, ServiceTypeID, Status, SUspendTime

            }
            else
            {
                $formatexpression = `
                    @{Expression={$_.HostName};           Label="HostName";           width=25}, `
                    @{Expression={$_.InstanceID};         Label="InstanceID";         width=40}, `
                    @{Expression={$_.PendingOperation};   Label="PendingOperation";   width=7}, `
                    @{Expression={$_.ServiceStatus};      Label="ServiceStatus";      width=7}, `
                    @{Expression={$_.ServiceTypeId};      Label="ServiceTypeId";      width=60}

		$sortexpression = `
		     @{Expression={$_.HostName}; Ascending=$true}

                #bts-list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap $formatexpression
		bts-list-objs $wmiobject | sort-object $sortexpression | format-table -Wrap $formatexpression
            }
        }
        else
        {
            'ServiceInstance: '

            bts-list-objs $wmiobject | sort-object $sortexpression |format-list `
                ActivationTime, AssemblyCulture, AssemblyName, AssemblyPublicKeyToken, AssemblyVersion, `
		Caption, Description, ErrorCategory, ErrorDescription, ErrorID, HostName, InstallDate, `
		InstanceID, MgmtDbNameOverride, MgmtDbServerOverride, MsgBoxDBName, MsgBoxServerName, `
		Name, PendingOperation, PendingOperationTime, ServiceClass, ServiceClassID, ServiceName, `
		ServiceStatus, ServiceTypeID, Status, SUspendTime

        }
}

####################################################################################################
#
# Main script
#
####################################################################################################

switch ( $action )
{
# MSBTS_BTSObject derived objects
#
# NB. MSBTS_MessageInstance, MSBTS_TrackedMessageInstance, and MSBTS_TrackedMessageInstance2 not implemented

    'list-hostqueues' {
        Display_MSBTS_HostQueue_Info
    }

    'list-serverhosts' {
        Display_MSBTS_ServerHost_Info
        #bts-list-objs('MSBTS_ServerHost') | %{ bts-display-serverhost($_) }
    }

    'list-serviceinstances' {
        Display_MSBTS_ServiceInstance_Info
    }

# MSBTS_Setting derived objects

    'list-adaptersettings' {
        Display_MSBTS_AdapterSetting_Info
        #bts-list-objs MSBTS_AdapterSetting | %{ bts-display-adaptersetting $_ }
    }

    'list-deploymentservices' {
        Display_MSBTS_DeploymentService_Info
        #bts-list-objs MSBTS_DeploymentService | %{ bts-display-deploymentservice $_ }
    }

    'list-groupsettings' {
        Display_MSBTS_GroupSetting_Info
    }

    'list-hostinstancesettings' {
        Display_MSBTS_HostInstanceSetting_Info
        #bts-list-objs MSBTS_HostInstanceSetting | %{ bts-display-hostinstancesetting $_ }
    }

    'list-hostsettings' {
        Display_MSBTS_HostSetting_Info
        #bts-list-objs MSBTS_HostSetting | %{ bts-display-hostsetting $_ }
    }

    'list-msgboxsettings' {
        Display_MSBTS_MsgBoxSetting_Info
        #bts-list-objs MSBTS_MsgBoxSetting | %{ bts-display-msgboxsetting $_ }
    }

    'list-receivehandlers' {
        Display_MSBTS_ReceiveHandler_Info
        #bts-list-objs MSBTS_ReceiveHandler | %{ bts-display-receivehandler $_ }
    }

    'list-receivelocations' {
        Display_MSBTS_ReceiveLocation_Info
    }

    'list-receivelocationorchestrations' {
        Display_MSBTS_ReceiveLocationOrchestration_Info
    }

    'list-receiveports' {
        Display_MSBTS_ReceivePort_Info
        #bts-list-objs('MSBTS_ReceivePort') | %{ bts-display-recv-port($_) }
    }

    'list-sendhandlers' {
        Display_MSBTS_SendHandler_Info
    }

    'list-sendhandler2s' {
        Display_MSBTS_SendHandler2s_Info
    }

    'list-sendports' {
        Display_MSBTS_SendPort_Info
        #bts-list-objs('MSBTS_SendPort') | %{ bts-display-sendport($_) }
    }

    'list-sendportgroups' {
        Display_MSBTS_SendPortGroup_Info
        #bts-list-objs('MSBTS_SendPortGroup') | %{ bts-display-sendportgroup($_) }
    }

    'list-sendportgroup2SendPorts' {
        Display_MSBTS_SendPortGroup2SendPort_Info
    }

    'list-serversettings' {
        Display_MSBTS_ServerSetting_Info
    }

# MSBTS_Service derived objects

    'list-hosts' {
        Display_MSBTS_Host_Info
    }

    'list-hostinstances' {
        Display_MSBTS_HostInstance_Info
        #bts-list-objs MSBTS_HostInstance | %{ bts-display-hostinstance($_) }
    }

    'list-orchestrations' {
        Display_MSBTS_Orchestration_Info
    }

    'list-servers' {
        Display_MSBTS_Server_Info
    }
}

####################################################################################################
#
# End
#
####################################################################################################

