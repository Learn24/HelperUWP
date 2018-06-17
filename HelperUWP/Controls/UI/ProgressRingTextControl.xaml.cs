using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HelperUWP.Controls.UI
{
    public sealed partial class ProgressRingTextControl : UserControl
    {
        public ProgressRingTextControl()
        {
            this.InitializeComponent();
        }
        //string
        public static readonly DependencyProperty SetMessageProperty = DependencyProperty.Register("SetText", typeof(string),
            typeof(ProgressRingTextControl), new PropertyMetadata("", new PropertyChangedCallback(OnMessageTextChanged)));

        public string SetText
        {
            get { return (string)GetValue(SetMessageProperty); }
            set { SetValue(SetMessageProperty, value); }
        }
        private static void OnMessageTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressRingTextControl borderControl = d as ProgressRingTextControl;
            borderControl.OnMessageTextChanged(e);
        }
        private void OnMessageTextChanged(DependencyPropertyChangedEventArgs e)
        {
            ProgressMessageText.Text = e.NewValue.ToString();
        }

    }
}
