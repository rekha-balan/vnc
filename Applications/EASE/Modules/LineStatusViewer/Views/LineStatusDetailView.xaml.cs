using System;
using System.Windows.Controls;

namespace LineStatusViewer.Views
{
    /// <summary>
    /// Interaction logic for LineStatusDetailView
    /// </summary>
    public partial class LineStatusDetailView : UserControl
    {
        public LineStatusDetailView()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                var foo = ex;
            }
        }
    }
}
