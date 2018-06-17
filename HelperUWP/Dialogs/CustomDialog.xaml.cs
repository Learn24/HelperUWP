using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HelperUWP.Dialogs
{
    public sealed partial class CustomDialog : ContentDialog
    {
        public CustomDialog()
        {
            this.InitializeComponent();
        }
        private string content1 = string.Empty;
        public string ButtonContentOne
        {
            get
            {
                return this.content1;
            }
            set
            {
                this.content1 = value;
                ButtonContent1.Content = ButtonContentOne;
            }
        }
        private string content2 = string.Empty;
        public string ButtonContentTwo
        {
            get
            {
                return this.content2;
            }
            set
            {
                this.content2 = value;
                ButtonContent2.Content = ButtonContentTwo;
            }
        }
        public event RoutedEventHandler Button1Clicked;
        public event RoutedEventHandler Button2Clicked;
        private void ButtonContent1_Click(object sender, RoutedEventArgs e)
        {
            if (Button1Clicked != null)
                Button1Clicked(sender, e);
        }
        private void ButtonContent2_Click(object sender, RoutedEventArgs e)
        {
            if (Button2Clicked != null)
                Button2Clicked(sender, e);
        }
    }
}
