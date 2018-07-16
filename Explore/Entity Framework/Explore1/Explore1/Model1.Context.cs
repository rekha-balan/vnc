﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Explore1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EASE_DEMOEntities : DbContext
    {
        public EASE_DEMOEntities()
            : base("name=EASE_DEMOEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ABC> ABCs { get; set; }
        public virtual DbSet<ACTIVITY> ACTIVITies { get; set; }
        public virtual DbSet<ACTLIN> ACTLINs { get; set; }
        public virtual DbSet<c_operator> c_operator { get; set; }
        public virtual DbSet<c_operator_track> c_operator_track { get; set; }
        public virtual DbSet<c_team> c_team { get; set; }
        public virtual DbSet<CAPPHDR> CAPPHDRs { get; set; }
        public virtual DbSet<CAPPHDR7> CAPPHDR7 { get; set; }
        public virtual DbSet<CAPPOPHD> CAPPOPHDs { get; set; }
        public virtual DbSet<CAPPOPHDR7> CAPPOPHDR7 { get; set; }
        public virtual DbSet<CLIENT> CLIENTs { get; set; }
        public virtual DbSet<COST> COSTs { get; set; }
        public virtual DbSet<CumminsWI_DefectGroupItems> CumminsWI_DefectGroupItems { get; set; }
        public virtual DbSet<CumminsWI_DefectGroups> CumminsWI_DefectGroups { get; set; }
        public virtual DbSet<CumminsWI_DefectSBM> CumminsWI_DefectSBM { get; set; }
        public virtual DbSet<cumminswi_messages> cumminswi_messages { get; set; }
        public virtual DbSet<cumminswi_messages_Stations> cumminswi_messages_Stations { get; set; }
        public virtual DbSet<cumminswi_mrc_enginelist> cumminswi_mrc_enginelist { get; set; }
        public virtual DbSet<cumminswi_mrc_signoffstatus> cumminswi_mrc_signoffstatus { get; set; }
        public virtual DbSet<cumminswi_mrc_substationdata> cumminswi_mrc_substationdata { get; set; }
        public virtual DbSet<EASE_NEWFIELD_INTERFACE> EASE_NEWFIELD_INTERFACE { get; set; }
        public virtual DbSet<EASECPGAGE> EASECPGAGEs { get; set; }
        public virtual DbSet<EASEGROUP> EASEGROUPS { get; set; }
        public virtual DbSet<EASESY> EASESYS { get; set; }
        public virtual DbSet<EASEWebLogin> EASEWebLogins { get; set; }
        public virtual DbSet<ERGOLIN> ERGOLINs { get; set; }
        public virtual DbSet<ERGOTEXT> ERGOTEXTs { get; set; }
        public virtual DbSet<EUSER> EUSERs { get; set; }
        public virtual DbSet<EUSERRIGHT> EUSERRIGHTS { get; set; }
        public virtual DbSet<FORMATTEDHISTORY> FORMATTEDHISTORies { get; set; }
        public virtual DbSet<FORMULA> Formulae { get; set; }
        public virtual DbSet<HEADERFIELD> HEADERFIELDS { get; set; }
        public virtual DbSet<HISTTEXT> HISTTEXTs { get; set; }
        public virtual DbSet<KVI> KVIs { get; set; }
        public virtual DbSet<LBCATLOG> LBCATLOGs { get; set; }
        public virtual DbSet<LBPART> LBPARTS { get; set; }
        public virtual DbSet<LBTEXT> LBTEXTs { get; set; }
        public virtual DbSet<LINE> LINES { get; set; }
        public virtual DbSet<MACHHDR> MACHHDRs { get; set; }
        public virtual DbSet<MATLIST> MATLISTs { get; set; }
        public virtual DbSet<MATLISTO> MATLISTOes { get; set; }
        public virtual DbSet<MFT> MFTs { get; set; }
        public virtual DbSet<MFTEXT> MFTEXTs { get; set; }
        public virtual DbSet<MTMLIN> MTMLINs { get; set; }
        public virtual DbSet<OPAUTHCNT> OPAUTHCNTs { get; set; }
        public virtual DbSet<OPHDR> OPHDRs { get; set; }
        public virtual DbSet<OPHMM7> OPHMM7 { get; set; }
        public virtual DbSet<OPHPFD> OPHPFDs { get; set; }
        public virtual DbSet<OPMACH7> OPMACH7 { get; set; }
        public virtual DbSet<OPMACHINE> OPMACHINES { get; set; }
        public virtual DbSet<OPTEXT> OPTEXTs { get; set; }
        public virtual DbSet<OPTOOL> OPTOOLs { get; set; }
        public virtual DbSet<PARTXREF> PARTXREFs { get; set; }
        public virtual DbSet<PCNAREATEMPLATE> PCNAREATEMPLATEs { get; set; }
        public virtual DbSet<PCNAUTHGROUP> PCNAUTHGROUPS { get; set; }
        public virtual DbSet<PCNCHANGETEXT> PCNCHANGETEXTs { get; set; }
        public virtual DbSet<PCNHEADER> PCNHEADERs { get; set; }
        public virtual DbSet<PCNMM> PCNMMs { get; set; }
        public virtual DbSet<PCNNOTE> PCNNOTES { get; set; }
        public virtual DbSet<PCNNotifyQuestion> PCNNotifyQuestions { get; set; }
        public virtual DbSet<PCNPART> PCNPARTS { get; set; }
        public virtual DbSet<PCNRFCCATEGORY> PCNRFCCATEGORies { get; set; }
        public virtual DbSet<PCNRFCCOMMENT> PCNRFCCOMMENTs { get; set; }
        public virtual DbSet<PCNSD> PCNSDs { get; set; }
        public virtual DbSet<PCNSDAUTHCNT> PCNSDAUTHCNTs { get; set; }
        public virtual DbSet<PCNTASKDueDateChanx> PCNTASKDueDateChanges { get; set; }
        public virtual DbSet<PCNTASKEscalate> PCNTASKEscalates { get; set; }
        public virtual DbSet<PCNTASK> PCNTASKS { get; set; }
        public virtual DbSet<PCNWITHDRAW> PCNWITHDRAWs { get; set; }
        public virtual DbSet<PDFTEXT> PDFTEXTs { get; set; }
        public virtual DbSet<PDMAuditItem> PDMAuditItems { get; set; }
        public virtual DbSet<PDMCPCDETECTION> PDMCPCDETECTIONs { get; set; }
        public virtual DbSet<PDMCPCONTROLMETHOD> PDMCPCONTROLMETHODS { get; set; }
        public virtual DbSet<PDMCPCPREVENTIONCODE> PDMCPCPREVENTIONCODES { get; set; }
        public virtual DbSet<PDMCPEVALUATIONMETHOD> PDMCPEVALUATIONMETHODS { get; set; }
        public virtual DbSet<PDMCPLINE> PDMCPLINES { get; set; }
        public virtual DbSet<PDMDETCODE> PDMDETCODES { get; set; }
        public virtual DbSet<PDMDOCCATEGORy> PDMDOCCATEGORIES { get; set; }
        public virtual DbSet<PDMErgoHdr> PDMErgoHdrs { get; set; }
        public virtual DbSet<PDMFAILCAUSECODE> PDMFAILCAUSECODES { get; set; }
        public virtual DbSet<PDMFAILEFFECTCODE> PDMFAILEFFECTCODES { get; set; }
        public virtual DbSet<PDMFAILUREMODECODE> PDMFAILUREMODECODES { get; set; }
        public virtual DbSet<PDMFMEACPCORETEAM> PDMFMEACPCORETEAMs { get; set; }
        public virtual DbSet<PDMFMEACPHDR> PDMFMEACPHDRs { get; set; }
        public virtual DbSet<PDMFMEAHistory> PDMFMEAHistories { get; set; }
        public virtual DbSet<PDMFMEALINE> PDMFMEALINES { get; set; }
        public virtual DbSet<PDMISGRAPHIC> PDMISGRAPHICs { get; set; }
        public virtual DbSet<PDMISHDR> PDMISHDRs { get; set; }
        public virtual DbSet<PDMISITEM> PDMISITEMs { get; set; }
        public virtual DbSet<PDMISManufacturer> PDMISManufacturers { get; set; }
        public virtual DbSet<PDMISSHEET> PDMISSHEETs { get; set; }
        public virtual DbSet<PDMISTEXT> PDMISTEXTs { get; set; }
        public virtual DbSet<PDMLocDocsCustomField> PDMLocDocsCustomFields { get; set; }
        public virtual DbSet<PDMLPA_EXCUSEDAUDITS> PDMLPA_EXCUSEDAUDITS { get; set; }
        public virtual DbSet<PDMLPA_FailureCategory> PDMLPA_FailureCategory { get; set; }
        public virtual DbSet<PDMLPA_NUMBEROFWEEKS> PDMLPA_NUMBEROFWEEKS { get; set; }
        public virtual DbSet<PDMLPAAUDITITEMTEXT> PDMLPAAUDITITEMTEXTs { get; set; }
        public virtual DbSet<PDMLPACORETEAM> PDMLPACORETEAMs { get; set; }
        public virtual DbSet<PDMLPAFAILUREMODECODE> PDMLPAFAILUREMODECODES { get; set; }
        public virtual DbSet<PDMLPAHDR> PDMLPAHDRs { get; set; }
        public virtual DbSet<PDMLPALINE> PDMLPALINES { get; set; }
        public virtual DbSet<PDMLPAQ> PDMLPAQs { get; set; }
        public virtual DbSet<PDMLPASHIFT> PDMLPASHIFTs { get; set; }
        public virtual DbSet<PDMMQV_TEMPLATES> PDMMQV_TEMPLATES { get; set; }
        public virtual DbSet<PDMMQV_TEMPLATES_CUSTOMFIELDS> PDMMQV_TEMPLATES_CUSTOMFIELDS { get; set; }
        public virtual DbSet<PDMMQVDEFECTMODE> PDMMQVDEFECTMODES { get; set; }
        public virtual DbSet<PDMMQVDETECTION> PDMMQVDETECTIONs { get; set; }
        public virtual DbSet<PDMMQVMISC> PDMMQVMISCs { get; set; }
        public virtual DbSet<PDMMQVPREVENTION> PDMMQVPREVENTIONs { get; set; }
        public virtual DbSet<PDMOCCCODE> PDMOCCCODES { get; set; }
        public virtual DbSet<PDMPartHistory> PDMPartHistories { get; set; }
        public virtual DbSet<PDMPlant> PDMPlants { get; set; }
        public virtual DbSet<PDMPlantHistory> PDMPlantHistories { get; set; }
        public virtual DbSet<PDMPlantMM1> PDMPlantMM1 { get; set; }
        public virtual DbSet<PDMPlantText> PDMPlantTexts { get; set; }
        public virtual DbSet<PDMPROCESS> PDMPROCESSes { get; set; }
        public virtual DbSet<PDMRemedyPlan> PDMRemedyPlans { get; set; }
        public virtual DbSet<PDMRemedyPlanDiff> PDMRemedyPlanDiffs { get; set; }
        public virtual DbSet<PDMREMEDYPLANMULTISIGNOFF> PDMREMEDYPLANMULTISIGNOFFs { get; set; }
        public virtual DbSet<PDMRemedyPlanTabular> PDMRemedyPlanTabulars { get; set; }
        public virtual DbSet<PDMRemedyPlanTabularValue> PDMRemedyPlanTabularValues { get; set; }
        public virtual DbSet<PDMREMEDYSMART> PDMREMEDYSMARTs { get; set; }
        public virtual DbSet<PDMRPData> PDMRPDatas { get; set; }
        public virtual DbSet<PDMRPDeviation> PDMRPDeviations { get; set; }
        public virtual DbSet<PDMRPItemValue> PDMRPItemValues { get; set; }
        public virtual DbSet<PDMRPITEMVALUESSECONDARY> PDMRPITEMVALUESSECONDARies { get; set; }
        public virtual DbSet<PDMRPPictureTEXT> PDMRPPictureTEXTs { get; set; }
        public virtual DbSet<PDMSEVCODE> PDMSEVCODES { get; set; }
        public virtual DbSet<PDMSharedDocGroup> PDMSharedDocGroups { get; set; }
        public virtual DbSet<PDMWCHistory> PDMWCHistories { get; set; }
        public virtual DbSet<PDMWGHistory> PDMWGHistories { get; set; }
        public virtual DbSet<PDMWorkCenter> PDMWorkCenters { get; set; }
        public virtual DbSet<PDMWorkCenterMM1> PDMWorkCenterMM1 { get; set; }
        public virtual DbSet<PDMWorkCenterText> PDMWorkCenterTexts { get; set; }
        public virtual DbSet<PDMWorkGroup> PDMWorkGroups { get; set; }
        public virtual DbSet<PDMWorkGroupMM1> PDMWorkGroupMM1 { get; set; }
        public virtual DbSet<PDMWorkGroupText> PDMWorkGroupTexts { get; set; }
        public virtual DbSet<PDMWorkStation> PDMWorkStations { get; set; }
        public virtual DbSet<PDMWorkStationMM1> PDMWorkStationMM1 { get; set; }
        public virtual DbSet<PDMWorkStationText> PDMWorkStationTexts { get; set; }
        public virtual DbSet<PDMWSHistory> PDMWSHistories { get; set; }
        public virtual DbSet<PLANTMODELDESTINATION> PLANTMODELDESTINATIONs { get; set; }
        public virtual DbSet<PLANTMODELOPTION> PLANTMODELOPTIONS { get; set; }
        public virtual DbSet<RHMM> RHMMs { get; set; }
        public virtual DbSet<RHTEXT> RHTEXTs { get; set; }
        public virtual DbSet<ROUTEHDR> ROUTEHDRs { get; set; }
        public virtual DbSet<SHAREDOPHDR> SHAREDOPHDRs { get; set; }
        public virtual DbSet<SHCONTROLPLAN> SHCONTROLPLANs { get; set; }
        public virtual DbSet<SHCONTROLPLANTEXT> SHCONTROLPLANTEXTs { get; set; }
        public virtual DbSet<SHELEFORM> SHELEFORMS { get; set; }
        public virtual DbSet<SHELEMENT> SHELEMENTS { get; set; }
        public virtual DbSet<SHMATERIAL> SHMATERIALS { get; set; }
        public virtual DbSet<SHMM> SHMMs { get; set; }
        public virtual DbSet<SHPDFTEXT> SHPDFTEXTs { get; set; }
        public virtual DbSet<SHTEXT> SHTEXTs { get; set; }
        public virtual DbSet<SHTOOL> SHTOOLS { get; set; }
        public virtual DbSet<SMCheckListHeader> SMCheckListHeaders { get; set; }
        public virtual DbSet<SMCheckListQuestion> SMCheckListQuestions { get; set; }
        public virtual DbSet<SMFunction> SMFunctions { get; set; }
        public virtual DbSet<SMFunctionCourse> SMFunctionCourses { get; set; }
        public virtual DbSet<SMOperatorCheckListAnswer> SMOperatorCheckListAnswers { get; set; }
        public virtual DbSet<SMOperatorSkill> SMOperatorSkills { get; set; }
        public virtual DbSet<SMOperatorSkillsLocation> SMOperatorSkillsLocations { get; set; }
        public virtual DbSet<SMQuestion> SMQuestions { get; set; }
        public virtual DbSet<SMQuestionsTEXT> SMQuestionsTEXTs { get; set; }
        public virtual DbSet<STATION> STATIONS { get; set; }
        public virtual DbSet<STDTEXT> STDTEXTs { get; set; }
        public virtual DbSet<SUBHDR> SUBHDRs { get; set; }
        public virtual DbSet<SW_OPHEADER> SW_OPHEADER { get; set; }
        public virtual DbSet<SW_OPTEXT> SW_OPTEXT { get; set; }
        public virtual DbSet<SW_Signoff> SW_Signoff { get; set; }
        public virtual DbSet<SW_SUBHEADER> SW_SUBHEADER { get; set; }
        public virtual DbSet<SW_TEMPLATES> SW_TEMPLATES { get; set; }
        public virtual DbSet<SW_TEMPLATES_CUSTOMFIELDS> SW_TEMPLATES_CUSTOMFIELDS { get; set; }
        public virtual DbSet<SW_TEXT> SW_TEXT { get; set; }
        public virtual DbSet<TBLCELL> TBLCELLs { get; set; }
        public virtual DbSet<TBLHDR> TBLHDRs { get; set; }
        public virtual DbSet<TBLSTRUC> TBLSTRUCs { get; set; }
        public virtual DbSet<TOOLBOM> TOOLBOMs { get; set; }
        public virtual DbSet<TOOLCALIBRATION> TOOLCALIBRATIONs { get; set; }
        public virtual DbSet<TOOLCALIBRATIONHISTORY> TOOLCALIBRATIONHISTORies { get; set; }
        public virtual DbSet<TOOLFORMATTEDHISTORY> TOOLFORMATTEDHISTORies { get; set; }
        public virtual DbSet<TOOLHEADER> TOOLHEADERs { get; set; }
        public virtual DbSet<TOOLITEM> TOOLITEMS { get; set; }
        public virtual DbSet<TOOLITEMTYPE> TOOLITEMTYPEs { get; set; }
        public virtual DbSet<TOOLLOCATION> TOOLLOCATIONS { get; set; }
        public virtual DbSet<TOOLMaximoLink> TOOLMaximoLinks { get; set; }
        public virtual DbSet<TOOLMM> TOOLMMs { get; set; }
        public virtual DbSet<TOOLPDFText> TOOLPDFTexts { get; set; }
        public virtual DbSet<TOOLSUPPLIER> TOOLSUPPLIERS { get; set; }
        public virtual DbSet<TOOLTEXT> TOOLTEXTs { get; set; }
        public virtual DbSet<TOOLTYPE1> TOOLTYPE1 { get; set; }
        public virtual DbSet<TOOLTYPE2> TOOLTYPE2 { get; set; }
        public virtual DbSet<ToolType3> ToolType3 { get; set; }
        public virtual DbSet<ToolType4> ToolType4 { get; set; }
        public virtual DbSet<ToolType5> ToolType5 { get; set; }
        public virtual DbSet<WCLINK> WCLINKs { get; set; }
        public virtual DbSet<WCPFD> WCPFDs { get; set; }
        public virtual DbSet<WIHISTORYTEXT> WIHISTORYTEXTs { get; set; }
        public virtual DbSet<WITEXT> WITEXTs { get; set; }
        public virtual DbSet<WORKCEN> WORKCENs { get; set; }
        public virtual DbSet<BANNERMESSAGE> BANNERMESSAGES { get; set; }
        public virtual DbSet<BANNERMESSAGES_STATIONS> BANNERMESSAGES_STATIONS { get; set; }
        public virtual DbSet<CPOPHCONTROLPLAN> CPOPHCONTROLPLANs { get; set; }
        public virtual DbSet<CPOPHREACTIONTEXT> CPOPHREACTIONTEXTs { get; set; }
        public virtual DbSet<CPWITEXT> CPWITEXTs { get; set; }
        public virtual DbSet<CurrentEngineStatu> CurrentEngineStatus { get; set; }
        public virtual DbSet<EASE_ENGINE_OPTIONS_ARCH_VW> EASE_ENGINE_OPTIONS_ARCH_VW { get; set; }
        public virtual DbSet<EASE_ENGINE_OPTIONS_VW> EASE_ENGINE_OPTIONS_VW { get; set; }
        public virtual DbSet<GENERICOPTION> GENERICOPTIONS { get; set; }
        public virtual DbSet<HITACHIEBSEXPORT> HITACHIEBSEXPORTs { get; set; }
        public virtual DbSet<ODBCDATA> ODBCDATAs { get; set; }
        public virtual DbSet<OPAUTH> OPAUTHs { get; set; }
        public virtual DbSet<OPTIONSFAMILY> OPTIONSFAMILies { get; set; }
        public virtual DbSet<PCNRFCCAT> PCNRFCCATs { get; set; }
        public virtual DbSet<PCNRFCGROUP> PCNRFCGROUPs { get; set; }
        public virtual DbSet<PCNSDAuth> PCNSDAuths { get; set; }
        public virtual DbSet<PCNTASKLIST> PCNTASKLISTs { get; set; }
        public virtual DbSet<PCNWITEXT> PCNWITEXTs { get; set; }
        public virtual DbSet<PDMErgoAudit> PDMErgoAudits { get; set; }
        public virtual DbSet<PDMLPA_AUDITDOCHDR> PDMLPA_AUDITDOCHDR { get; set; }
        public virtual DbSet<PDMLPAAUDITANSWER> PDMLPAAUDITANSWERS { get; set; }
        public virtual DbSet<PDMLPAAUDITREJECTION> PDMLPAAUDITREJECTIONs { get; set; }
        public virtual DbSet<PDMPartMM> PDMPartMMs { get; set; }
        public virtual DbSet<PDMPartPDFText> PDMPartPDFTexts { get; set; }
        public virtual DbSet<PDMPartText> PDMPartTexts { get; set; }
        public virtual DbSet<PDMRPITEMVALUESQCHOLDRELEASE> PDMRPITEMVALUESQCHOLDRELEASEs { get; set; }
        public virtual DbSet<PDMTOOLINDEX1> PDMTOOLINDEX1 { get; set; }
        public virtual DbSet<PDMTOOLINDEX2> PDMTOOLINDEX2 { get; set; }
        public virtual DbSet<PDMTOOLINDEX3> PDMTOOLINDEX3 { get; set; }
        public virtual DbSet<PDMTOOLINDEX4> PDMTOOLINDEX4 { get; set; }
        public virtual DbSet<PDMTOOLLIST> PDMTOOLLISTs { get; set; }
        public virtual DbSet<PDMTT2CONFIG> PDMTT2CONFIG { get; set; }
        public virtual DbSet<PDMTTCONFIG> PDMTTCONFIGs { get; set; }
        public virtual DbSet<PlantModel> PlantModels { get; set; }
        public virtual DbSet<PlantModelYear> PlantModelYears { get; set; }
        public virtual DbSet<RHSIGN> RHSIGNs { get; set; }
        public virtual DbSet<SPECIFICOPTION> SPECIFICOPTIONS { get; set; }
        public virtual DbSet<TOOLITEMGRAPHIC> TOOLITEMGRAPHICs { get; set; }
        public virtual DbSet<TOOLMaximoItemMaster> TOOLMaximoItemMasters { get; set; }
    }
}