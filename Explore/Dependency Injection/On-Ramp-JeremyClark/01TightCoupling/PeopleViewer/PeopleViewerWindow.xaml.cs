using PeopleViewer.Presentation;
using PeopleViewer.SharedObjects;
using System.Windows;

namespace PeopleViewer
{
    public partial class PeopleViewerWindow : Window
    {
        public PeopleViewerWindow()
        {
            InitializeComponent();
            DataContext = new PeopleViewerViewModel();
        }
    }
}
