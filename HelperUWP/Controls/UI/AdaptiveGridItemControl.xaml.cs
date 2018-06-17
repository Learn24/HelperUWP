using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
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
    public sealed partial class AdaptiveGridItemControl : UserControl
    {
        public AdaptiveGridItemControl()
        {
            this.InitializeComponent();
        }
        //SetTitle
        public static readonly DependencyProperty SetTitleProperty = DependencyProperty.Register("SetTitle", typeof(string),
            typeof(AdaptiveGridItemControl), new PropertyMetadata("", new PropertyChangedCallback(OnSetTitleChanged)));

        public string SetTitle
        {
            get { return (string)GetValue(SetTitleProperty); }
            set { SetValue(SetTitleProperty, value); }
        }
        private static void OnSetTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AdaptiveGridItemControl borderControl = d as AdaptiveGridItemControl;
            borderControl.OnSetTitleChanged(e);
        }
        private void OnSetTitleChanged(DependencyPropertyChangedEventArgs e)
        {
            HeadlineTextBlock.Text = e.NewValue.ToString();
        }
        //Set Title Foreground   
        public static readonly DependencyProperty SetTextForegroundProperty = DependencyProperty.Register("SetTextForeground", typeof(SolidColorBrush),
            typeof(AdaptiveGridItemControl), new PropertyMetadata(Colors.Black, new PropertyChangedCallback(OnSetTextForegroundChanged)));

        public SolidColorBrush SetTitleForeground
        {
            get { return (SolidColorBrush)GetValue(SetTextForegroundProperty); }
            set { SetValue(SetTextForegroundProperty, value); }
        }
        private static void OnSetTextForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AdaptiveGridItemControl borderControl = d as AdaptiveGridItemControl;
            borderControl.OnSetTextForegroundChanged(e);
        }
        private void OnSetTextForegroundChanged(DependencyPropertyChangedEventArgs e)
        {
            HeadlineTextBlock.Foreground =(SolidColorBrush)e.NewValue;
            ItemCounts.Foreground = (SolidColorBrush)e.NewValue;
            ItemText.Foreground = (SolidColorBrush)e.NewValue;
            EditButton.Foreground = (SolidColorBrush)e.NewValue;
        }
        //Set Main Background Color   
        public static readonly DependencyProperty SetBackgroundColorProperty = DependencyProperty.Register("SetBackgroundColor", typeof(SolidColorBrush),
            typeof(AdaptiveGridItemControl), new PropertyMetadata(Colors.Blue, new PropertyChangedCallback(OnSetBackgroundColorChanged)));

        public SolidColorBrush SetBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(SetBackgroundColorProperty); }
            set { SetValue(SetBackgroundColorProperty, value); }
        }
        private static void OnSetBackgroundColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AdaptiveGridItemControl borderControl = d as AdaptiveGridItemControl;
            borderControl.OnSetBackgroundColorChanged(e);
        }
        private void OnSetBackgroundColorChanged(DependencyPropertyChangedEventArgs e)
        {
            GridCons.Background = (SolidColorBrush)e.NewValue;
        }
        //Set Back Image Background Color   
        public static readonly DependencyProperty SetImageColorProperty = DependencyProperty.Register("SetImageColor", typeof(SolidColorBrush),
            typeof(AdaptiveGridItemControl), new PropertyMetadata(Colors.Blue, new PropertyChangedCallback(OnSetImageColorChanged)));

        public SolidColorBrush SetImageColor
        {
            get { return (SolidColorBrush)GetValue(SetImageColorProperty); }
            set { SetValue(SetImageColorProperty, value); }
        }
        private static void OnSetImageColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AdaptiveGridItemControl borderControl = d as AdaptiveGridItemControl;
            borderControl.OnSetImageColorChanged(e);
        }
        private void OnSetImageColorChanged(DependencyPropertyChangedEventArgs e)
        {
            ImagePanel.Background = (SolidColorBrush)e.NewValue;
        }
        //Set Item Counts
        public static readonly DependencyProperty SetItemCountProperty = DependencyProperty.Register("SetItemCount", typeof(string),
            typeof(AdaptiveGridItemControl), new PropertyMetadata("", new PropertyChangedCallback(OnSetItemCountChanged)));

        public string SetItemCount
        {
            get { return (string)GetValue(SetItemCountProperty); }
            set { SetValue(SetItemCountProperty, value); }
        }
        private static void OnSetItemCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AdaptiveGridItemControl borderControl = d as AdaptiveGridItemControl;
            borderControl.OnSetItemCountChanged(e);
        }
        private void OnSetItemCountChanged(DependencyPropertyChangedEventArgs e)
        {
            ItemCounts.Text = e.NewValue.ToString();
        }
        //SetImageSource
        public static readonly DependencyProperty SetImageSourceProperty = DependencyProperty.Register("SetImageSource", typeof(ImageSource),
            typeof(AdaptiveGridItemControl), new PropertyMetadata(null, new PropertyChangedCallback(OnSetImageSourceChanged)));

        public ImageSource SetImageSource
        {
            get { return (ImageSource)GetValue(SetImageSourceProperty); }
            set { SetValue(SetImageSourceProperty, value); }
        }
        private static void OnSetImageSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AdaptiveGridItemControl borderControl = d as AdaptiveGridItemControl;
            borderControl.OnSetImageSourceChanged(e);
        }
        private void OnSetImageSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            MyImage.Source =(ImageSource)e.NewValue;
        }
        //SetEditVisibility
        public static readonly DependencyProperty SetEditVisibilityProperty = DependencyProperty.Register("SetEditVisibility", typeof(Visibility),
            typeof(AdaptiveGridItemControl), new PropertyMetadata(Visibility.Collapsed, new PropertyChangedCallback(OnSetEditVisibilityChanged)));

        public Visibility SetEditVisibility
        {
            get { return (Visibility)GetValue(SetEditVisibilityProperty); }
            set { SetValue(SetEditVisibilityProperty, value); }
        }
        private static void OnSetEditVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AdaptiveGridItemControl borderControl = d as AdaptiveGridItemControl;
            borderControl.OnSetEditVisibilityChanged(e);
        }
        private void OnSetEditVisibilityChanged(DependencyPropertyChangedEventArgs e)
        {
            EditButton.Visibility = (Visibility)e.NewValue;
        }
        //SetEditCommand
        public static readonly DependencyProperty SetEditCommandProperty = DependencyProperty.Register("SetEditCommand", typeof(ICommand),
            typeof(AdaptiveGridItemControl), new PropertyMetadata(null, new PropertyChangedCallback(OnSetEditCommandChanged)));

        public ICommand SetEditCommand
        {
            get { return (ICommand)GetValue(SetEditCommandProperty); }
            set { SetValue(SetEditCommandProperty, value); }
        }
        private static void OnSetEditCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AdaptiveGridItemControl borderControl = d as AdaptiveGridItemControl;
            borderControl.OnSetEditCommandChanged(e);
        }
        private void OnSetEditCommandChanged(DependencyPropertyChangedEventArgs e)
        {
            EditButton.Command = (ICommand)e.NewValue;
        }
        //Edit button event
        public event RoutedEventHandler EditClicked;      
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (EditClicked != null)
                EditClicked(sender, e);
        }
    }
}
