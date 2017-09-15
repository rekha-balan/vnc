using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

using Microsoft.Win32;


namespace VNCCodeCommandConsole.User_Interface.User_Controls
{
    public partial class wucFind_Picker : UserControl
    {

        #region Constructors and Load

        public wucFind_Picker()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties and Fields

        // TODO:
        //  Add properties, backing fields, and dependency properties to match the attributes from each XML element
        //  Convention is _<Attribute Name>, <AttributeName>, <AttributeName>DP
        //  where <AttributeName> is the name of the attribute for the <Element>

        public List<string> Value
        {
            // IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
            get
            {
                return (List<string>)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }


        // TODO:
        //  Update the parameters to .Register(...) as needed

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(List<string>), typeof(wucFind_Picker), new PropertyMetadata(null, new PropertyChangedCallback(OnValueChanged)));

        #endregion

        #region Delegates and Events

        public delegate void ControlEvent();
        public event ControlEvent ControlChanged;

        #endregion

        #region Event Handlers


        private static void OnValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            wucFind_Picker wucFindPicker = o as wucFind_Picker;
            if (wucFindPicker != null)
                wucFindPicker.OnValueChanged((List<string>)e.OldValue, (List<string>)e.NewValue);
        }

        protected virtual void OnValueChanged(List<string> oldValue, List<string> newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var v = sender;
            var item = ((System.Windows.Controls.ComboBox)v).SelectedItem;

            if (null == item)
            {
                // May have just opened new file and no item has been selected.
                return;
            }

            Value = GetValue((System.Xml.XmlElement)item);

            ControlEvent fireEvent = Interlocked.CompareExchange(ref ControlChanged, null, null);

            if (null != fireEvent)
            {
                fireEvent();
            }
        }

        private List<string> GetValue(System.Xml.XmlElement instanceElement)
        {
            XmlNodeList nodes = instanceElement.GetElementsByTagName("FindItem");

            List<string> Value = null;
            //string[] database2 = new string[] { "One", "Two" };

            if (nodes.Count > 0)
            {
                Value = new List<string>();

                foreach (XmlNode item in nodes)
                {
                    Value.Add(item.Attributes["Value"].Value);
                }
            }

            return Value;
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            LoadDataFromFile();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //this.DataContext = this;
        }
        #endregion

        #region Main Methods

        public void PopulateControlFromFile(string fileNameAndPath)
        {
            comboBox.Source = new Uri(fileNameAndPath);
        }

        #endregion

        #region Private Methods

        private void LoadDataFromFile()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog1.FileName = "";
            openFileDialog1.InitialDirectory = @"C:\temp";

            if (true == openFileDialog1.ShowDialog())
            {
                string fileName = openFileDialog1.FileName;

                PopulateControlFromFile(fileName);
            }
        }

        #endregion
    }
}
