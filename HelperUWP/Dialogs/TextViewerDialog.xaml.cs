using HelperUWP.Common;
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

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HelperUWP.Dialogs
{
    public sealed partial class TextViewerDialog : ContentDialog
    {
        public TextViewerDialog()
        {
            this.InitializeComponent();
            Loaded += TextViewerDialog_Loaded;
        }
        //message
        public static readonly DependencyProperty SetMessageProperty = DependencyProperty.Register("SetText", typeof(string),
            typeof(TextViewerDialog), new PropertyMetadata("", new PropertyChangedCallback(OnMessageTextChanged)));

        public string SetText
        {
            get { return (string)GetValue(SetMessageProperty); }
            set { SetValue(SetMessageProperty, value); }
        }
        private static void OnMessageTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextViewerDialog borderControl = d as TextViewerDialog;
            borderControl.OnMessageTextChanged(e);
        }
        private void OnMessageTextChanged(DependencyPropertyChangedEventArgs e)
        {
            TextBoxView.Text = e.NewValue.ToString();
        }

        internal string TextToCopy = string.Empty;
        private void TextViewerDialog_Loaded(object sender, RoutedEventArgs e)
        {
            TextToCopy = TextBoxView.Text;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            StorageData.ClipboardCopyText(TextToCopy);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Hide();
        }

        private void TextBoxView_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {

        }
    }
}
