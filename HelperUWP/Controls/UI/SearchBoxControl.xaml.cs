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
    public sealed partial class SearchBoxControl : UserControl
    {
        public SearchBoxControl()
        {
            this.InitializeComponent();
        }
        private string waterText = string.Empty;
        public event RoutedEventHandler SearchboxGotFocus;
        private void Searchbox_GotFocus(object sender, RoutedEventArgs e)
        {
            waterText = WarterMarkText.Text;
            WarterMarkText.Text = string.Empty;
            if (SearchboxGotFocus != null)
            {
                SearchboxGotFocus(sender, e);
            }
        }
        public event RoutedEventHandler SearchboxLostFocus;
        private void Searchbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchBoxName.Text))
            {
                WarterMarkText.Text = waterText;
            }
            if (SearchboxLostFocus != null)
            {
                SearchboxLostFocus(sender, e);
            }
        }
        public event RoutedEventHandler SetSearchButtonClick;
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchBoxName.Visibility = Visibility.Visible;
            WarterMarkText.Visibility = Visibility.Visible;
            if (SetSearchButtonClick != null)
            {
                SetSearchButtonClick(sender, e);
            }
        }
        //string
        public static readonly DependencyProperty SetMessageProperty = DependencyProperty.Register("SetSearchText", typeof(string),
            typeof(SearchBoxControl), new PropertyMetadata("", new PropertyChangedCallback(OnMessageTextChanged)));

        public string SetSearchText
        {
            get { return (string)GetValue(SetMessageProperty); }
            set { SetValue(SetMessageProperty, value); }
        }
        private static void OnMessageTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SearchBoxControl borderControl = d as SearchBoxControl;
            borderControl.OnMessageTextChanged(e);
        }
        private void OnMessageTextChanged(DependencyPropertyChangedEventArgs e)
        {
            SearchBoxName.Text = e.NewValue.ToString();
        }
        //string
        public static readonly DependencyProperty SetWaterMarkTextProperty = DependencyProperty.Register("SetWaterMarkText", typeof(string),
            typeof(SearchBoxControl), new PropertyMetadata("", new PropertyChangedCallback(OnSetWaterMarkTextChanged)));

        public string SetWaterMarkText
        {
            get { return (string)GetValue(SetWaterMarkTextProperty); }
            set { SetValue(SetWaterMarkTextProperty, value); }
        }
        private static void OnSetWaterMarkTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SearchBoxControl borderControl = d as SearchBoxControl;
            borderControl.OnSetWaterMarkTextChanged(e);
        }
        private void OnSetWaterMarkTextChanged(DependencyPropertyChangedEventArgs e)
        {
            WarterMarkText.Text = e.NewValue.ToString();
        }
        //Visibilty
        public static readonly DependencyProperty SetSearchBoxVisibilityProperty = DependencyProperty.Register("SetSearchBoxVisibility", typeof(string),
            typeof(SearchBoxControl), new PropertyMetadata(Visibility.Collapsed, new PropertyChangedCallback(OnSetSearchBoxVisibilityChanged)));

        public Visibility SetSearchBoxVisibility
        {
            get { return (Visibility)GetValue(SetSearchBoxVisibilityProperty); }
            set { SetValue(SetSearchBoxVisibilityProperty, value); }
        }
        private static void OnSetSearchBoxVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SearchBoxControl borderControl = d as SearchBoxControl;
            borderControl.OnSetSearchBoxVisibilityChanged(e);
        }
        private void OnSetSearchBoxVisibilityChanged(DependencyPropertyChangedEventArgs e)
        {
            WarterMarkText.Visibility =(Visibility) e.NewValue;
            SearchBoxName.Visibility = (Visibility)e.NewValue;
        }

    }
}
