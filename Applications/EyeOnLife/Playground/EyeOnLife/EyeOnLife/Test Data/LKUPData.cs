using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Collections;

namespace EyeOnLife.Test_Data
{
    class LKUPData
    {

        private static string _RawXML;

        //public string InstanceName;
        //public string ServerName;

        private static IEnumerable<adDomainInfo> _ADDomains = null;
        public static IEnumerable<adDomainInfo> ADDomains
        {
            get
            {
                if(null == _ADDomains)
                {
                    _ADDomains = GetADDomainsList(_RawXML, ""); ;
                }

                return _ADDomains;
            }
            set
            {
                _ADDomains = value;
            }
        }
        private static IEnumerable<environmentInfo> _Environments = null;
        public static IEnumerable<environmentInfo> Environments
        {
            get
            {
                if(null == _Environments)
                {
                    _Environments = GetEnvironmentsList(_RawXML, ""); ;
                }

                return _Environments;
            }
            set
            {
                _Environments = value;
            }
        }


        private static IEnumerable<sqlServerVersionInfo> _SQLServerVersions = null;
        public static IEnumerable<sqlServerVersionInfo> SQLServerVersions
        {
            get
            {
                if (null == _SQLServerVersions)
                {
                    _SQLServerVersions = GetSQLServerVersionList(_RawXML, ""); ;
                }

                return _SQLServerVersions;
            }
            set
            {
                _SQLServerVersions = value;
            }
        }

        //public delegate void InstanceSelectionChanged();
        //public event InstanceSelectionChanged InstanceSelectionChanged_Event;


        #region Main Function Routines

        private static IEnumerable<adDomainInfo> GetADDomainsList(string root, string param)
        {
            IEnumerable<adDomainInfo> adDomains =
                from item in XDocument.Parse(root).Descendants("ADDomains").Elements("ADDomain")
                select new adDomainInfo( int.Parse(item.Attribute("ID").Value),
                                         item.Attribute("Name").Value);

            return adDomains;
        }

        private static IEnumerable<environmentInfo> GetEnvironmentsList(string root, string param)
        {
            IEnumerable<environmentInfo> environments = 
                from item in XDocument.Parse(root).Descendants("Environments").Elements("Environment")
                select new environmentInfo( int.Parse(item.Attribute("ID").Value),
                                            item.Attribute("Name").Value);

            return environments;
        }


        private static IEnumerable<sqlServerVersionInfo> GetSQLServerVersionList(string root, string param)
        {
            IEnumerable<sqlServerVersionInfo> list =
                from item in XDocument.Parse(root).Descendants("SQLServerVersions").Elements("SQLServerVersion")
                select new sqlServerVersionInfo(
                        int.Parse(item.Attribute("ID").Value),
                        item.Attribute("Name").Value,
                        item.Attribute("CodeName").Value,                    
                        item.Attribute("RTM").Value,
                        item.Attribute("SP1").Value,
                        item.Attribute("SP2").Value,
                        item.Attribute("SP3").Value,
                        item.Attribute("SP4").Value );

            return list;
        }

        public static void LoadLKUPInformationFromFile()
        {
            PopulateLKUPListsFromFile(SQLInformation.Data.Config.LKUP_FileName);
        }

        private static void PopulateLKUPListsFromFile(string fileName)
        {
            using(StreamReader streamReader = new StreamReader(fileName))
            {
                ADDomains = null;
                Environments = null;
                SQLServerVersions = null;


                _RawXML = streamReader.ReadToEnd();
            }
        }

        #endregion

        public class adDomainInfo
        {
            public int ID;
            public string Name;
           
            public adDomainInfo(int id, string name)
            {
                ID = id;
                Name = name;
            }
        }

        public class environmentInfo
        {
            public int ID;
            public string Name;

            public environmentInfo(int id, string name)
            {
                ID = id;
                Name = name;
            }
        }


        public class sqlServerVersionInfo
        {
            public int ID;
            public string Name;
            public string CodeName;
            public string RTM;
            public string SP1;
            public string SP2;
            public string SP3;
            public string SP4;

            public sqlServerVersionInfo(int id, string name, string codeName, string rtm, string sp1, string sp2, string sp3, string sp4)
            {
                ID = id;
                Name = name;
                CodeName = codeName;
                RTM = rtm;
                SP1 = sp1;
                SP2 = sp2;
                SP3 = sp3;
                SP4 = sp4;
            }
        }

    }
}
