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
    public sealed partial class ProgressMessageControl : UserControl
    {
        public ProgressMessageControl()
        {
            this.InitializeComponent();
        }
       
        //string
        public static readonly DependencyProperty SetMessageProperty = DependencyProperty.Register("SetText", typeof(string),
            typeof(ProgressMessageControl), new PropertyMetadata("", new PropertyChangedCallback(OnMessageTextChanged)));

        public string SetText
        {
            get { return (string)GetValue(SetMessageProperty); }
            set { SetValue(SetMessageProperty, value); }
        }
        private static void OnMessageTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressMessageControl borderControl = d as ProgressMessageControl;
            borderControl.OnMessageTextChanged(e);
        }
        private void OnMessageTextChanged(DependencyPropertyChangedEventArgs e)
        {
            ProgressMessageText.Text = e.NewValue.ToString();
        }

        public enum ShowOrHide
        {
            Show,
            Hide,
        }
        public  void SetProgressMessage(string ProgressText, int secondTimer, bool IsTimerIsEnabled, Grid MainGrid, ShowOrHide ShowOrHide)
        {

           SetText = ProgressText;
           if (ShowOrHide == ShowOrHide.Show)
            {
                MainGrid.Children.Add(this);
                this.Margin = new Thickness(5, 5, 5, 5);
            }
            else
            {
                MainGrid.Children.Remove(this);
            }

            if (IsTimerIsEnabled)
            {
                var Timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(secondTimer) };

                Timer.Start();
                Timer.Tick += (f, g) =>
                {
                    MainGrid.Children.Remove(this);
                };
            }
            
        }
        //public static void SetProgressMessageStatic(string ProgressText, int secondTimer, bool IsTimerIsEnabled, Grid MainGrid, ShowOrHide ShowOrHide)
        //{
        //    ProgressMessageControl ProgressMessage = new ProgressMessageControl();
        //    ProgressMessage.SetProgressMessage( ProgressText,  secondTimer,  IsTimerIsEnabled,  MainGrid, ShowOrHide);
        //    //SetText = ProgressText;
        //    //if (ShowOrHide == ShowOrHide.Show)
        //    //{
        //    //    MainGrid.Children.Add(this);
        //    //    progressMessageControl.Margin = new Thickness(5, 5, 5, 5);
        //    //}
        //    //else
        //    //{
        //    //    MainGrid.Children.Remove(this);
        //    //}

        //    //if (IsTimerIsEnabled)
        //    //{
        //    //    var Timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(secondTimer) };

        //    //    Timer.Start();
        //    //    Timer.Tick += (f, g) =>
        //    //    {
        //    //        MainGrid.Children.Remove(this);
        //    //    };
        //    //}

        //}
    }
}
