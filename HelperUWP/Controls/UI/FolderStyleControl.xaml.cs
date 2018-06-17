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
    public sealed partial class FolderStyleControl : UserControl
    {
        public FolderStyleControl()
        {
            this.InitializeComponent();
        }
        //SetTitle
        public static readonly DependencyProperty SetTitleProperty = DependencyProperty.Register("SetTitle", typeof(string),
            typeof(FolderStyleControl), new PropertyMetadata("", new PropertyChangedCallback(OnSetTitleChanged)));

        public string SetTitle
        {
            get { return (string)GetValue(SetTitleProperty); }
            set { SetValue(SetTitleProperty, value); }
        }
        private static void OnSetTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FolderStyleControl borderControl = d as FolderStyleControl;
            borderControl.OnSetTitleChanged(e);
        }
        private void OnSetTitleChanged(DependencyPropertyChangedEventArgs e)
        {
            TitleText.Text = e.NewValue.ToString();
        }
        //SetHeader
        public static readonly DependencyProperty SetItemCountProperty = DependencyProperty.Register("SetHeader", typeof(string),
            typeof(FolderStyleControl), new PropertyMetadata("", new PropertyChangedCallback(OnSetItemCountChanged)));

        public string SetHeader
        {
            get { return (string)GetValue(SetItemCountProperty); }
            set { SetValue(SetItemCountProperty, value); }
        }
        private static void OnSetItemCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FolderStyleControl borderControl = d as FolderStyleControl;
            borderControl.OnSetItemCountChanged(e);
        }
        private void OnSetItemCountChanged(DependencyPropertyChangedEventArgs e)
        {
            ItemCountText.Text = e.NewValue.ToString();
        }
        //SetHeaderText
        public static readonly DependencyProperty SetSubHeaderTextProperty = DependencyProperty.Register("SetHeaderText", typeof(string),
            typeof(FolderStyleControl), new PropertyMetadata("", new PropertyChangedCallback(OnSetItemCountChanged)));

        public string SetHeaderText
        {
            get { return (string)GetValue(SetSubHeaderTextProperty); }
            set { SetValue(SetSubHeaderTextProperty, value); }
        }
        private static void OnSetSubHeaderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FolderStyleControl borderControl = d as FolderStyleControl;
            borderControl.OnSetSubHeaderTextChanged(e);
        }
        private void OnSetSubHeaderTextChanged(DependencyPropertyChangedEventArgs e)
        {
            ItemText.Text = e.NewValue.ToString();
        }
        //Set Title Foreground   
        public static readonly DependencyProperty SetTextForegroundProperty = DependencyProperty.Register("SetForeground", typeof(SolidColorBrush),
            typeof(FolderStyleControl), new PropertyMetadata(Colors.White, new PropertyChangedCallback(OnSetTextForegroundChanged)));

        public SolidColorBrush SetForeground
        {
            get { return (SolidColorBrush)GetValue(SetTextForegroundProperty); }
            set { SetValue(SetTextForegroundProperty, value); }
        }
        private static void OnSetTextForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FolderStyleControl borderControl = d as FolderStyleControl;
            borderControl.OnSetTextForegroundChanged(e);
        }
        private void OnSetTextForegroundChanged(DependencyPropertyChangedEventArgs e)
        {
            TitleText.Foreground = (SolidColorBrush)e.NewValue;
            ItemText.Foreground = (SolidColorBrush)e.NewValue;
            ItemCountText.Foreground = (SolidColorBrush)e.NewValue;
            EditButton.Foreground = (SolidColorBrush)e.NewValue;
        }
        //SetImageSource
        public static readonly DependencyProperty SetImageThumbnailProperty = DependencyProperty.Register("SetImageThumbnail", typeof(ImageSource),
            typeof(FolderStyleControl), new PropertyMetadata(null, new PropertyChangedCallback(OnSetImageThumbnailChanged)));

        public ImageSource SetImageThumbnail
        {
            get { return (ImageSource)GetValue(SetImageThumbnailProperty); }
            set { SetValue(SetImageThumbnailProperty, value); }
        }
        private static void OnSetImageThumbnailChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FolderStyleControl borderControl = d as FolderStyleControl;
            borderControl.OnSetImageThumbnailChanged(e);
        }
        private void OnSetImageThumbnailChanged(DependencyPropertyChangedEventArgs e)
        {
            ImageThumbnail.Source = (ImageSource)e.NewValue;
        }
        //SetIconImage
        public static readonly DependencyProperty SetIconImageProperty = DependencyProperty.Register("SetImageIcon", typeof(ImageSource),
            typeof(FolderStyleControl), new PropertyMetadata(null, new PropertyChangedCallback(OnSetIconImageChanged)));

        public ImageSource SetImageIcon
        {
            get { return (ImageSource)GetValue(SetIconImageProperty); }
            set { SetValue(SetIconImageProperty, value); }
        }
        private static void OnSetIconImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FolderStyleControl borderControl = d as FolderStyleControl;
            borderControl.OnSetIconImageChanged(e);
        }
        private void OnSetIconImageChanged(DependencyPropertyChangedEventArgs e)
        {
            IconNull.Source = (ImageSource)e.NewValue;
            IconNullSmall.Source = (ImageSource)e.NewValue;
        }
        //Set Back Image Background Color   
        public static readonly DependencyProperty SetImageColorProperty = DependencyProperty.Register("SetImageColor", typeof(SolidColorBrush),
            typeof(FolderStyleControl), new PropertyMetadata(Colors.Blue, new PropertyChangedCallback(OnSetImageColorChanged)));

        public SolidColorBrush SetImageColor
        {
            get { return (SolidColorBrush)GetValue(SetImageColorProperty); }
            set { SetValue(SetImageColorProperty, value); }
        }
        private static void OnSetImageColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FolderStyleControl borderControl = d as FolderStyleControl;
            borderControl.OnSetImageColorChanged(e);
        }
        private void OnSetImageColorChanged(DependencyPropertyChangedEventArgs e)
        {
            GridTop.Background = (SolidColorBrush)e.NewValue;
        }
        //Set Back Grid Background Color   
        public static readonly DependencyProperty SetGridColorProperty = DependencyProperty.Register("SetGridColor", typeof(SolidColorBrush),
            typeof(FolderStyleControl), new PropertyMetadata(Colors.Blue, new PropertyChangedCallback(OnSetGridColorChanged)));

        public SolidColorBrush SetGridColor
        {
            get { return (SolidColorBrush)GetValue(SetGridColorProperty); }
            set { SetValue(SetGridColorProperty, value); }
        }
        private static void OnSetGridColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FolderStyleControl borderControl = d as FolderStyleControl;
            borderControl.OnSetGridColorChanged(e);
        }
        private void OnSetGridColorChanged(DependencyPropertyChangedEventArgs e)
        {
            GridBelow.Background = (SolidColorBrush)e.NewValue;
        }
        //SetEditVisibility
        public static readonly DependencyProperty SetEditVisibilityProperty = DependencyProperty.Register("SetEditVisibility", typeof(Visibility),
            typeof(FolderStyleControl), new PropertyMetadata(Visibility.Collapsed, new PropertyChangedCallback(OnSetEditVisibilityChanged)));

        public Visibility SetEditVisibility
        {
            get { return (Visibility)GetValue(SetEditVisibilityProperty); }
            set { SetValue(SetEditVisibilityProperty, value); }
        }
        private static void OnSetEditVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FolderStyleControl borderControl = d as FolderStyleControl;
            borderControl.OnSetEditVisibilityChanged(e);
        }
        private void OnSetEditVisibilityChanged(DependencyPropertyChangedEventArgs e)
        {
            EditButton.Visibility = (Visibility)e.NewValue;
        }
        //SetEditCommand
        public static readonly DependencyProperty SetEditCommandProperty = DependencyProperty.Register("SetEditCommand", typeof(ICommand),
            typeof(FolderStyleControl), new PropertyMetadata(null, new PropertyChangedCallback(OnSetEditCommandChanged)));

        public ICommand SetEditCommand
        {
            get { return (ICommand)GetValue(SetEditCommandProperty); }
            set { SetValue(SetEditCommandProperty, value); }
        }
        private static void OnSetEditCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FolderStyleControl borderControl = d as FolderStyleControl;
            borderControl.OnSetEditCommandChanged(e);
        }
        private void OnSetEditCommandChanged(DependencyPropertyChangedEventArgs e)
        {
            EditButton.Command = (ICommand)e.NewValue;
        }
        //Edit button event
        public event RoutedEventHandler SetEditClicked;
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (SetEditClicked != null)
                SetEditClicked(sender, e);
        }
    }
}
