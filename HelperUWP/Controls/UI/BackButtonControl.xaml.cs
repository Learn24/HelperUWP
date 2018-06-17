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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HelperUWP.Controls.UI
{
    public sealed partial class BackButtonControl : UserControl
    {
        public BackButtonControl()
        {
            this.InitializeComponent();
            
        }
       
        public  void SetInitializeBackButton(Frame MyFrame)
        {
            if (DeviceInfo.IsMobile || DeviceInfo.IsTabletMode && MyFrame.CanGoBack == true)
            {
                BackButton.Visibility = Visibility.Visible;
            }
            else
            {
                BackButton.Visibility = Visibility.Collapsed;
            }
            this.BackButtonClick += (g, k) =>
            {
                if (MyFrame.CanGoBack)
                {
                    MyFrame.GoBack();
                }
            };
        }
       

        public void SetInitializeBackButton(Frame MyFrame, bool IsEnabledInDesktop)
        {

            if (IsEnabledInDesktop)
            {
                if (MyFrame.CanGoBack == true)
                {
                    BackButton.Visibility = Visibility.Visible;
                }
                else
                {
                    BackButton.Visibility = Visibility.Collapsed;

                }
            }
            else
            {
                if (DeviceInfo.IsMobile || DeviceInfo.IsTabletMode && MyFrame.CanGoBack == true)
                {
                    BackButton.Visibility = Visibility.Visible;
                }
                else
                {
                    BackButton.Visibility = Visibility.Collapsed;

                }
            }
                
            this.BackButtonClick += (g, k) =>
            {
                if (MyFrame.CanGoBack)
                {
                    MyFrame.GoBack();
                }
            };
        }
        
        public event RoutedEventHandler BackButtonClick;
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (BackButtonClick != null)
            {
                BackButtonClick(sender, e);
            }
        }
        //string
        public static readonly DependencyProperty SetMessageProperty = DependencyProperty.Register("SetTitle", typeof(string),
            typeof(BackButtonControl), new PropertyMetadata("", new PropertyChangedCallback(OnMessageTextChanged)));

        public string SetTitle
        {
            get { return (string)GetValue(SetMessageProperty); }
            set { SetValue(SetMessageProperty, value); }
        }
        private static void OnMessageTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BackButtonControl borderControl = d as BackButtonControl;
            borderControl.OnMessageTextChanged(e);
        }
        private void OnMessageTextChanged(DependencyPropertyChangedEventArgs e)
        {
            TitleText.Text = e.NewValue.ToString();
        }
        //SetButtonVisibility
        public static readonly DependencyProperty SetButtonVisibilityProperty = DependencyProperty.Register("SetButtonVisibility", typeof(Visibility),
            typeof(BackButtonControl), new PropertyMetadata(Visibility.Visible, new PropertyChangedCallback(OnSetButtonVisibilityChanged)));

        public Visibility SetButtonVisibility
        {
            get { return (Visibility)GetValue(SetButtonVisibilityProperty); }
            set { SetValue(SetButtonVisibilityProperty, value); }
        }
        private static void OnSetButtonVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BackButtonControl borderControl = d as BackButtonControl;
            borderControl.OnSetButtonVisibilityChanged(e);
        }
        private void OnSetButtonVisibilityChanged(DependencyPropertyChangedEventArgs e)
        {
            BackButton.Visibility =(Visibility) e.NewValue;
        }
    }
}
