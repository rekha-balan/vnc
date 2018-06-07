using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Ink;
//using IdentityMine.Avalon.PatientSimulator;
//using System.Windows.Forms;
//using IdentityMine.Avalon.Controls;

namespace EyeOnLife
{
    #region Public Enums

    public enum MasterListStyles
    {
        SmallListItem,
        MediumListItem,
        LargeListItem,
        TextListItem
    }

    #endregion

    public class MasterListManager
    {
        FrameworkElement baseFrameworkElement;

        public MasterListManager(object fe)
        {
            baseFrameworkElement = fe as FrameworkElement;

            if (baseFrameworkElement == null)
                return;
        }

        public void Load()
        {
            if (baseFrameworkElement == null)
                return;

            ControlTemplate ct = (ControlTemplate)baseFrameworkElement.FindResource("MasterListStyleSelectorTemplate");
            ContentControl cc = (ContentControl)baseFrameworkElement.FindName("MasterStyleSelectorContent");
            ComboBox comboBox = ct.FindName("MasterSelectorComboBox", cc) as ComboBox;
            comboBox.SelectionChanged += new SelectionChangedEventHandler(MasterSelectorComboBox_SelectionChanged);
        }

        #region Events

        void MasterSelectorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if ((cb == null) || (baseFrameworkElement == null))
                return;

            string listItemStyleKey = "SmallMasterListItem";

            switch (cb.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    listItemStyleKey = "MediumMasterListItem";
                    break;
                case 2:
                    listItemStyleKey = "LargeMasterListItem";
                    break;
            }

            // TODO(crhodes): This might be where we need to switch to use a diffrent template style for the list.
            // Instead of searching from Application, perhaps we can search more locally.  The sender may be able
            // to tell us where we are.

            //Style listItemStyle = System.Windows.Application.Current.FindResource(listItemStyleKey) as Style;

            Style listItemStyle =baseFrameworkElement.FindResource(listItemStyleKey) as Style;

            foreach (System.Windows.Trigger t in listItemStyle.Triggers)
            {
                //Common.WriteToDebugWindow(string.Format("Trigger type:{0} value:{1}  IsSealed:{2}", t.GetType(), t.DependencyObjectType, t.IsSealed));

                foreach (System.Windows.Setter s in t.Setters)
                {
                    //Common.WriteToDebugWindow(string.Format("  Setter Property.Name:{0} value:{1}", s.Property.Name, s.Value));                    
                }
            }

            DynamicResourceExtension dr = new DynamicResourceExtension();

            var tmplt =  System.Windows.Application.Current.FindResource("smallServerDetailsTemplateSelected");
            
            dr.ResourceKey = tmplt;

            //((Setter)((Trigger)listItemStyle.Triggers[0]).Setters[0])
            //((Setter)((Trigger)listItemStyle.Triggers[0]).Setters[0]).Value = tmplt;

            //listItemStyle.Triggers.IndexOf();

            if (listItemStyle != null)
            {
                ListBox lb = (ListBox) baseFrameworkElement.FindName("MasterList");

                if (lb != null)
                {
                    lb.ItemContainerStyle = listItemStyle;
                }
            }
        }

        #endregion

    }
}
