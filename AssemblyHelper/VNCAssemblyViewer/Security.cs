using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace VNCAssemblyViewer
{
    public class Security : DependencyObject
    {
        public static readonly DependencyProperty IsAdministratorModeProperty = DependencyProperty.Register("IsAdministratorMode", typeof(bool), typeof(Security), new UIPropertyMetadata(false, new PropertyChangedCallback(OnIsAdministratorModeChanged), new CoerceValueCallback(OnCoerceIsAdministratorMode)));

        private static object OnCoerceIsAdministratorMode(DependencyObject o, object value)
        {
            Security security = o as Security;
            if (security != null)
                return security.OnCoerceIsAdministratorMode((bool)value);
            else
                return value;
        }

        private static void OnIsAdministratorModeChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Security security = o as Security;
            if (security != null)
                security.OnIsAdministratorModeChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual bool OnCoerceIsAdministratorMode(bool value)
        {
            // TODO: Keep the proposed value within the desired range.
            return value;
        }

        protected virtual void OnIsAdministratorModeChanged(bool oldValue, bool newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        public bool IsAdministratorMode
        {
            // IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
            get
            {
                return (bool)GetValue(IsAdministratorModeProperty);
            }
            set
            {
                SetValue(IsAdministratorModeProperty, value);
            }
        }

    }
}
