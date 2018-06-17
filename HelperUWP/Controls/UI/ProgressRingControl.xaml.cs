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
    public sealed partial class ProgressRingControl : UserControl
    {
        public ProgressRingControl()
        {
            this.InitializeComponent();
        }
        //Set the width
        public static readonly DependencyProperty SetWidthProperty = DependencyProperty.Register("SetWidth", typeof(int),
            typeof(ProgressRingControl), new PropertyMetadata("", new PropertyChangedCallback(OnSetWidthChanged)));

        public int SetWidth
        {
            get { return (int)GetValue(SetWidthProperty); }
            set { SetValue(SetWidthProperty, value); }
        }
        private static void OnSetWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressRingControl borderControl = d as ProgressRingControl;
            borderControl.OnSetWidthChanged(e);
        }
        private void OnSetWidthChanged(DependencyPropertyChangedEventArgs e)
        {
            ProgressRingName.Width = (int)e.NewValue;
        }

        //Set the height
        public static readonly DependencyProperty SetHeightProperty = DependencyProperty.Register("SetHeight", typeof(int),
            typeof(ProgressRingControl), new PropertyMetadata("", new PropertyChangedCallback(OnSetWidthChanged)));

        public int SetHeight
        {
            get { return (int)GetValue(SetHeightProperty); }
            set { SetValue(SetHeightProperty, value); }
        }
        private static void OnSetHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressRingControl borderControl = d as ProgressRingControl;
            borderControl.OnSetHeightChanged(e);
        }
        private void OnSetHeightChanged(DependencyPropertyChangedEventArgs e)
        {
            ProgressRingName.Height = (int)e.NewValue;
        }
        public enum ShowOrHide
        {
            Show,
            Hide,
        }
        public  void SetProgressRing(int width, int height, int secondTimer, bool IsTimerIsEnabled, Grid MainGrid, ShowOrHide ShowOrHide)
        {
            
            SetWidth = width;
            SetHeight = height;
           if(ShowOrHide == ShowOrHide.Show)
            {
                MainGrid.Children.Add(this);
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
    }
}
