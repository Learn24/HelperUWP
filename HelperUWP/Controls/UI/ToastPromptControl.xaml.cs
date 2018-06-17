using HelperUWP.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class ToastPromptControl : UserControl
    {
        public ToastPromptControl()
        {
            this.InitializeComponent();
            Loaded += ToastPromptControl_Loaded;
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(4) };
        private async void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            await InitializeSize();
        }

        private async void ToastPromptControl_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeSize();
        }
        private Task InitializeSize()
        {
            var width = Window.Current.Bounds.Width;
            if (DeviceInfo.IsMobile)
            {
                toastPanel.Width = width;
                toastPanel.HorizontalAlignment = HorizontalAlignment.Center;
            }
            else
            {
                if (width <= 500)
                {
                    toastPanel.Width = width;
                    toastPanel.HorizontalAlignment = HorizontalAlignment.Center;
                }
                else
                {
                    toastPanel.Width = 400;
                    toastPanel.HorizontalAlignment = HorizontalAlignment.Right;
                }
            }
            return Task.CompletedTask;
        }
        public void Hide()
        {
            
            this.HideAnimation.Begin();
            this.Visibility = Visibility.Collapsed;
            timer.Stop();
        }
        public void Show()
        {
            this.Visibility = Visibility.Visible;
            this.ShowAnimation.Begin();
            timer.Start();
            timer.Tick += (s, d) =>
            {
                this.Hide();
            };
        }
        //string
        public static readonly DependencyProperty SetMessageProperty = DependencyProperty.Register("SetMessage", typeof(string),
            typeof(ToastPromptControl), new PropertyMetadata("", new PropertyChangedCallback(OnMessageTextChanged)));

        public string SetMessage
        {
            get { return (string)GetValue(SetMessageProperty); }
            set { SetValue(SetMessageProperty, value); }
        }
        private static void OnMessageTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ToastPromptControl borderControl = d as ToastPromptControl;
            borderControl.OnMessageTextChanged(e);
        }
        private void OnMessageTextChanged(DependencyPropertyChangedEventArgs e)
        {
            message.Text = e.NewValue.ToString();
        }



        public static readonly DependencyProperty SetSourceProperty = DependencyProperty.Register("SetIcon", typeof(ImageSource),
           typeof(ToastPromptControl), new PropertyMetadata("", new PropertyChangedCallback(OnSourceTextChanged)));

        public ImageSource SetIcon
        {
            get { return (ImageSource)GetValue(SetSourceProperty); }
            set { SetValue(SetSourceProperty, value); }
        }
        private static void OnSourceTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ToastPromptControl borderControl = d as ToastPromptControl;
            borderControl.OnSourceTextChanged(e);
        }
        private void OnSourceTextChanged(DependencyPropertyChangedEventArgs e)
        {
            icon.Source = (ImageSource)e.NewValue;
        }

        //background
        public static readonly DependencyProperty SetBackroundColortSourceProperty = DependencyProperty.Register("SetBackroundColor", typeof(SolidColorBrush),
          typeof(ToastPromptControl), new PropertyMetadata(Colors.Red, new PropertyChangedCallback(OnSetBackroundColorChanged)));

        public SolidColorBrush SetBackroundColor
        {
            get { return (SolidColorBrush)GetValue(SetBackroundColortSourceProperty); }
            set { SetValue(SetBackroundColortSourceProperty, value); }
        }
        private static void OnSetBackroundColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ToastPromptControl borderControl = d as ToastPromptControl;
            borderControl.OnSetBackroundColorChanged(e);
        }
        private void OnSetBackroundColorChanged(DependencyPropertyChangedEventArgs e)
        {
            toastPanel.Background = (SolidColorBrush)e.NewValue;
        }

    }
}
