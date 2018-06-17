using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class ProgressBarControl : UserControl
    {
        public ProgressBarControl()
        {
            this.InitializeComponent();
        }
        //Set the width
        public static readonly DependencyProperty SetWidthProperty = DependencyProperty.Register("SetWidth", typeof(int),
            typeof(ProgressBarControl), new PropertyMetadata(100, new PropertyChangedCallback(OnSetWidthChanged)));

        public int SetWidth
        {
            get { return (int)GetValue(SetWidthProperty); }
            set { SetValue(SetWidthProperty, value); }
        }
        private static void OnSetWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressBarControl borderControl = d as ProgressBarControl;
            borderControl.OnSetWidthChanged(e);
        }
        private void OnSetWidthChanged(DependencyPropertyChangedEventArgs e)
        {
            ProgressBarName.Width = (int)e.NewValue;
        }

        //Set the height
        public static readonly DependencyProperty SetHeightProperty = DependencyProperty.Register("SetHeight", typeof(int),
            typeof(ProgressBarControl), new PropertyMetadata(100, new PropertyChangedCallback(OnSetHeightChanged)));

        public int SetHeight
        {
            get { return (int)GetValue(SetHeightProperty); }
            set { SetValue(SetHeightProperty, value); }
        }
        private static void OnSetHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressBarControl borderControl = d as ProgressBarControl;
            borderControl.OnSetHeightChanged(e);
        }
        private void OnSetHeightChanged(DependencyPropertyChangedEventArgs e)
        {
            ProgressBarName.Height = (int)e.NewValue;
        }
        //Set Foreground
        public static readonly DependencyProperty SetForegroundProperty = DependencyProperty.Register("SetForeground", typeof(SolidColorBrush),
            typeof(ProgressBarControl), new PropertyMetadata(Colors.Green, new PropertyChangedCallback(OnSetForegroundChanged)));

        public SolidColorBrush SetForeground
        {
            get { return (SolidColorBrush)GetValue(SetForegroundProperty); }
            set { SetValue(SetForegroundProperty, value); }
        }
        private static void OnSetForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressBarControl borderControl = d as ProgressBarControl;
            borderControl.OnSetForegroundChanged(e);
        }
        private void OnSetForegroundChanged(DependencyPropertyChangedEventArgs e)
        {           
            ProgressBarName.Foreground = (SolidColorBrush)e.NewValue;
        }
        //Set Background
        public static readonly DependencyProperty SetBackgroundProperty = DependencyProperty.Register("SetBackground", typeof(SolidColorBrush),
            typeof(ProgressBarControl), new PropertyMetadata(Colors.White, new PropertyChangedCallback(OnSetBackgroundChanged)));

        public SolidColorBrush SetBackground
        {
            get { return (SolidColorBrush)GetValue(SetBackgroundProperty); }
            set { SetValue(SetBackgroundProperty, value); }
        }
        private static void OnSetBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressBarControl borderControl = d as ProgressBarControl;
            borderControl.OnSetBackgroundChanged(e);
        }
        private void OnSetBackgroundChanged(DependencyPropertyChangedEventArgs e)
        {

            ProgressBarName.Background = (SolidColorBrush)e.NewValue;
        }
        //Set the Maximum
        public static readonly DependencyProperty SetMaximumProperty = DependencyProperty.Register("SetMaximum", typeof(int),
            typeof(ProgressBarControl), new PropertyMetadata(100, new PropertyChangedCallback(OnSetMaximumChanged)));

        public int SetMaximum
        {
            get { return (int)GetValue(SetMaximumProperty); }
            set { SetValue(SetMaximumProperty, value); }
        }
        private static void OnSetMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressBarControl borderControl = d as ProgressBarControl;
            borderControl.OnSetMaximumChanged(e);
        }
        private void OnSetMaximumChanged(DependencyPropertyChangedEventArgs e)
        {
            ProgressBarName.Maximum = (int)e.NewValue;
        }
        //Set the Value
        public static readonly DependencyProperty SetValueProperty = DependencyProperty.Register("SetTheValue", typeof(int),
            typeof(ProgressBarControl), new PropertyMetadata(50, new PropertyChangedCallback(OnSetValueChanged)));

        public int SetTheValue
        {
            get { return (int)GetValue(SetValueProperty); }
            set { SetValue(SetValueProperty, value); }
        }
        private static void OnSetValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressBarControl borderControl = d as ProgressBarControl;
            borderControl.OnSetValueChanged(e);
        }
        private void OnSetValueChanged(DependencyPropertyChangedEventArgs e)
        {
            ProgressBarName.Value = (int)e.NewValue;
        }



        public enum ShowOrHide
        {
            Show,
            Hide,
        }
        public void SetProgressBar(int width, int height, int secondTimer, bool IsTimerIsEnabled,
            SolidColorBrush forgroundColor, SolidColorBrush backgroundColor, int maximum, int value, Grid MainGrid, ShowOrHide ShowOrHide)
        {

            SetTheValue = value;
            SetBackground = backgroundColor;
            SetForeground = forgroundColor;
            SetMaximum = maximum;
            SetWidth = width;
            SetHeight = height;
            if (ShowOrHide == ShowOrHide.Show)
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
        public void SetProgressBar(int width, int height, int secondTimer, bool IsTimerIsEnabled,
           SolidColorBrush forgroundColor, SolidColorBrush backgroundColor, int maximum, int value)
        {

            SetTheValue = value;
            SetBackground = backgroundColor;
            SetForeground = forgroundColor;
            SetMaximum = maximum;
            SetWidth = width;
            SetHeight = height;
           

            if (IsTimerIsEnabled)
            {
                var Timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(secondTimer) };

                Timer.Start();
                Timer.Tick += (f, g) =>
                {
                    this.Visibility = Visibility.Collapsed;
                };
            }

        }
    }
}
