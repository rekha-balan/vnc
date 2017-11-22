﻿using System;
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

namespace SupportTools_Excel.User_Interface.User_Controls
{
    public partial class ucDBInstanceList : UserControl
    {
        #region Initialization

        public ucDBInstanceList()
        {
            InitializeComponent();
        }

        #endregion

        private XElement _RawXML;
        //private string _RawXML;

        public string InstanceName;
        public string ServerName;

        private IEnumerable<instanceInfo> _DBInstances = null;
        public IEnumerable<instanceInfo> DBInstances
        {
            get
            {
                if (null == _DBInstances)
                {
                    _DBInstances = GetInstanceInfoList(_RawXML, "DBInstanceList"); ;
                }
                
                return _DBInstances;
            }
            set
            {
                _DBInstances = value;
            }
        }

        public delegate void InstanceSelectionChanged();
        public event InstanceSelectionChanged InstanceSelectionChanged_Event;

        #region Event Handlers

        private void cbInstance_SelectedIndexChanged(object sender, EventArgs e)
        {
            //((instanceInfo)cbInstance.SelectedItem).FullName;
            //string a = cbInstance.SelectedValue.ToString();
            //string b = cbInstance.SelectedText.ToString();
            //string c = cbInstance.Text;

            InstanceName = ((instanceInfo)cbInstance.SelectedItem).FullName;
            ServerName = ((instanceInfo)cbInstance.SelectedItem).server;

            InstanceSelectionChanged temp = Interlocked.CompareExchange(ref InstanceSelectionChanged_Event, null, null);
            if (temp != null)
            {
            	temp();
            }
        }

        private void lblInstance_Click(object sender, EventArgs e)
        {
            LoadNewInstanceListFromFile();
        }

        #endregion

        #region Main Function Routines

        //private static IEnumerable<instanceInfo> GetInstanceInfoList(string root, string param)
        private static IEnumerable<instanceInfo> GetInstanceInfoList(XElement root, string param)
        {
            IEnumerable<instanceInfo> dbInstances = null;

            dbInstances =
                from item in root.Descendants(param).Descendants("Environment").Elements("Instance")
                select new instanceInfo(
                                item.Attribute("Server").Value, 
                                item.Attribute("IPv4Address").Value, 
                                item.Attribute("Instance").Value, 
                                item.Attribute("Port").Value);
            
            return dbInstances;
        }

        private void LoadNewInstanceListFromFile()
        {
            openFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog1.FileName = "";

            if(DialogResult.OK == openFileDialog1.ShowDialog())
            {
                string fileName = openFileDialog1.FileName;

                PopulateInstanceListFromFile(fileName, "Excel_Config");
            }
        }

        public void PopulateInstanceListFromFile(string fileName, string office_section)
        {
            using(StreamReader streamReader = new StreamReader(fileName))
            {
                cbInstance.Items.Clear();
                DBInstances = null;
                string fileXML = streamReader.ReadToEnd();
                _RawXML = XDocument.Parse(fileXML).Descendants(office_section).Single();

                foreach(instanceInfo instance in DBInstances)
                {
                    cbInstance.Items.Add(instance);
                }
            }
        }

        #endregion

        public class instanceInfo
        {
            public string server;
            public string ipv4Address;
            public string instance;
            public string port;

            private string _ExpandedName;
            public string ExpandedName
            {
                get
                {
                    if (instance.Length > 0 || port.Length > 0)
                    {
                        return string.Format(@"{0} ({1}) \ {2} ({3})", 
                            server.Length > 0 ? server : "??", 
                            ipv4Address.Length > 0 ? ipv4Address : "??", 
                            instance.Length > 0 ? instance : "??", 
                            port.Length > 0 ? port : "??");
                    }
                    else
                    {
                    	return string.Format(@"{0} ({1}) \ <Default>",
                            server.Length > 0 ? server : "??",
                            ipv4Address.Length > 0 ? ipv4Address : "??");
                    }

                }
                set
                {
                    _ExpandedName = value;
                }
            }
            
            private string _FullName;
            public string FullName
            {
                get
                {
                    string validName;

                    // Prefer HostName over IPs and InstanceNames over Port#s

                    if (instance.Length > 0 || port.Length > 0)  // Have Instance Info           
                    {   
                        validName = string.Format(@"{0}{1}",
                            server.Length > 0 ? server : ipv4Address,
                            instance.Length > 0   ? @"\" + instance : "," + port
                            );
                    }
                    else                                                
                    {
                        validName = server.Length > 0 ? server : ipv4Address;
                    }

                    return validName;
                }
                set
                {
                    _FullName = value;
                }
            }
            
            public instanceInfo(string Server, string IPv4Address, string Name, string Port)
            {
                server = Server;
                ipv4Address = IPv4Address;
                instance = Name;
                port = Port;
            }
        }
    }
}
