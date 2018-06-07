using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace XamlExtentionMethods
{
    public static class Methods
    {

        private static Action EmptyDelegate = delegate() {  };

        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }

        //public static void Refresh(this TextBox tb)
        //{
        //    tb.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        //}
    }
}
