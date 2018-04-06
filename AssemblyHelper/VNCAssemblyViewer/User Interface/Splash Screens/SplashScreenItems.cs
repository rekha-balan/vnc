using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
//using VNC;
using VNCAssemblyViewer.User_Interface.User_Controls;

namespace VNCAssemblyViewer.User_Interface.SplashScreens
{
    public class SplashScreenItems
    {
        public SplashScreenItems()
        {
            // Initialize the SplashScreen with what is available to all users depending on UserMode.

            List<SplashItem> splashList = new List<SplashItem>
                {
                    //new SplashItem
                    //{ 
                    //    Name="Explorers", 
                    //    Value="SplashScreens.wucSplashScreenExplorerLayout", 
                    //    UserMode=new UserMode( (int)UserMode.Mode.Advanced | (int)UserMode.Mode.Administrator) 
                    //},
                    new SplashItem
                    {
                        Name="Servers",
                        Value="Servers",
                        UserMode=new ViewMode( (int)ViewMode.Mode.Basic | (int)ViewMode.Mode.Advanced | (int)ViewMode.Mode.Administrator )
                    },
                    new SplashItem
                    {
                        Name="SQL Instances",
                        Value="Instances",
                        UserMode=new ViewMode( (int)ViewMode.Mode.Basic | (int)ViewMode.Mode.Advanced | (int)ViewMode.Mode.Administrator )
                    },
                    new SplashItem
                    {
                        Name="SQL Databases",
                        Value="Databases",
                        UserMode=new ViewMode( (int)ViewMode.Mode.Basic | (int)ViewMode.Mode.Advanced | (int)ViewMode.Mode.Administrator )
                    },
                    new SplashItem
                    {
                        Name="Environment Info",
                        Value="Admin_LKUPTables",
                        UserMode=new ViewMode( (int)ViewMode.Mode.Basic | (int)ViewMode.Mode.Advanced | (int)ViewMode.Mode.Administrator )
                    },
                    new SplashItem
                    {
                        Name="Help and Training",
                        Value="HelpAndTraining",
                        UserMode=new ViewMode( (int)ViewMode.Mode.Basic | (int)ViewMode.Mode.Advanced | (int)ViewMode.Mode.Administrator )
                    },
                    new SplashItem
                    {
                        Name="Instance Explorer",
                        Value="Explore_Instances",
                        UserMode=new ViewMode( (int)ViewMode.Mode.Advanced | (int)ViewMode.Mode.Administrator )
                    },
                    new SplashItem
                    {
                        Name="Database Explorer",
                        Value="Explore_Databases",
                        UserMode=new ViewMode( (int)ViewMode.Mode.Advanced | (int)ViewMode.Mode.Administrator )
                    },
                    new SplashItem
                    {
                        Name="Instance Storage Explorer",
                        Value="Explore_InstanceStorage",
                        UserMode=new ViewMode( (int)ViewMode.Mode.Advanced | (int)ViewMode.Mode.Administrator )
                    },
                    new SplashItem
                    {
                        Name="Database Storage Explorer",
                        Value="Explore_DatabaseStorage",
                        UserMode=new ViewMode( (int)ViewMode.Mode.Advanced | (int)ViewMode.Mode.Administrator )
                    },

                    new SplashItem
                    {
                        Name="Individual Tables",
                        Value="EyeOnLifeTables",
                        UserMode=new ViewMode( (int)ViewMode.Mode.Advanced | (int)ViewMode.Mode.Administrator )
                    }
                };

            // Then add what is optionally available depending on group membership.

            if (Common.IsBetaUser)
            {
                splashList.Add(new SplashItem
                {
                    Name = "Hierarchy Explorer",
                    Value = "Explore_Hierarchy",
                    UserMode = new ViewMode((int)ViewMode.Mode.Beta)
                });

                splashList.Add(new SplashItem
                {
                    Name = "JobServer Explorer",
                    Value = "Explore_JobServers",
                    UserMode = new ViewMode((int)ViewMode.Mode.Beta)
                });

                splashList.Add(new SplashItem
                {
                    Name = "Tiles",
                    Value = "SplashScreens.wucSplashScreenTileLayout",
                    UserMode = new ViewMode((int)ViewMode.Mode.Beta)
                });

                splashList.Add(new SplashItem
                {
                    Name = "Exploratory POC",
                    Value = "wucServers_Main",
                    UserMode = new ViewMode((int)ViewMode.Mode.Beta)
                });

                splashList.Add(new SplashItem
                {
                    Name = "Explore Charts",
                    Value = "wucMasterDetailBase1",
                    UserMode = new ViewMode((int)ViewMode.Mode.Beta)
                });
            }

            if (Common.IsAdministrator)
            {
                splashList.Add(new SplashItem
                {
                    Name = "Administration",
                    Value = "Admin_EyeOnLife",
                    UserMode = new ViewMode((int)ViewMode.Mode.Administrator)
                });

                // This has been moved to Admin_EyeOnLife (above)
                //splashList.Add(new SplashItem
                //{
                //    Name = "Crawler Administration",
                //    Value = "Admin_CrawlerExpandSettings",
                //    UserMode = new ViewMode((int)ViewMode.Mode.Administrator)
                //});
            }

            Items = splashList;
        }

        private List<SplashItem> _Items;

        public List<SplashItem> Items
        {
            get { return _Items; }
            set
            {
                _Items = value;
            }
        }

    }
}
