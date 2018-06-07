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
//using IdentityMine.Avalon.Controls;

namespace SQLInformation
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

            Style listItemStyle = Application.Current.FindResource(listItemStyleKey) as Style;

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
