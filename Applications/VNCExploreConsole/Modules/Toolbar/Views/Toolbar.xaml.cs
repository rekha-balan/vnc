﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Infrastructure;
using ModuleInterfaces;

namespace Toolbar
{
    /// <summary>
    /// Interaction logic for ToolbarView.xaml
    /// </summary>
    public partial class Toolbar : UserControl, IToolbar
    {
        public Toolbar()
        {
            InitializeComponent();
        }

        public IViewModel_VM1 ViewModel
        {
            get { return (IViewModel_VM1)DataContext; }
            set { DataContext = value; }
        }
    }
}
