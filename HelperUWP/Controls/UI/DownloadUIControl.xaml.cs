using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.BackgroundTransfer;
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
    public delegate void DownloadCompletedEventHandLer(object sender);
    public sealed partial class DownloadUIControl : UserControl
    {
        public bool IsCompleted { get; set; } = false;
        private CancellationTokenSource cts;
        public DownloadUIControl()
        {
            this.InitializeComponent();
            // UpdateStatus(download, IsForResuming);

        }


        public async void AttachDownloadOperation(DownloadOperation download, bool IsForResuming = false)
        {

            cts = new CancellationTokenSource();
            Progress<DownloadOperation> progress = new Progress<DownloadOperation>(progressChanged);
            if (IsForResuming)
            {
                try
                {
                    await download.AttachAsync().AsTask(cts.Token, progress);
                }
                catch (Exception ex)
                {
                    this.SetStatusText = ex.Message;
                }
            }
            else
            {
                try
                {
                    await download.StartAsync().AsTask(cts.Token, progress);
                }
                catch (Exception ex)
                {
                    this.SetStatusText = ex.Message;
                }
            }
        }
        public event DownloadCompletedEventHandLer DownloadCompleted;       
        private void progressChanged(DownloadOperation download)
        {
            try
            {
                int progress = 0;
                if (download.Progress.TotalBytesToReceive > 0)
                {
                    progress = (int)(100 * ((double)download.Progress.BytesReceived / (double)download.Progress.TotalBytesToReceive));
                    SetTheValue = progress;
                    this.PercentageText.Text = progress.ToString() + "%";
                    if (progress == 100)
                    {
                        this.CancelButton.Content = "Completed";
                        this.IsCompleted = true;
                        this.DownloadCompleted?.Invoke(this);
                    }
                }
                SetMaximum = 100;
                SetTitle = download.ResultFile.Name;
                SetSourceText = download.RequestedUri.ToString();
                if (progress == 0)
                {
                    SetStatusText = (download.Progress.BytesReceived / 1024).ToString() + " KB" + " Downloading - " + "Unknown" + "%";
                }
                else
                {
                    SetStatusText = (download.Progress.BytesReceived / 1024).ToString() + " KB" + " Downloading - " + progress.ToString() + "%";
                }
                if (download.Progress.Status == BackgroundTransferStatus.Completed)
                {
                    CancelButton.Content = "Completed";
                    this.IsCompleted = true;
                }
            }
            catch (Exception ex)
            {
                SetStatusText = ex.Message;
            }
        }

        //string
        public static readonly DependencyProperty SetMessageProperty = DependencyProperty.Register("SetTitle", typeof(string),
            typeof(DownloadUIControl), new PropertyMetadata("", new PropertyChangedCallback(OnMessageTextChanged)));

        public string SetTitle
        {
            get { return (string)GetValue(SetMessageProperty); }
            set { SetValue(SetMessageProperty, value); }
        }
        private static void OnMessageTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DownloadUIControl borderControl = d as DownloadUIControl;
            borderControl.OnMessageTextChanged(e);
        }
        private void OnMessageTextChanged(DependencyPropertyChangedEventArgs e)
        {
            FileNameTitle.Text = e.NewValue.ToString();
        }
        //SetSourceText
        public static readonly DependencyProperty SetSourceTextProperty = DependencyProperty.Register("SetSourceText", typeof(string),
            typeof(DownloadUIControl), new PropertyMetadata("", new PropertyChangedCallback(OnSetSourceTextChanged)));

        public string SetSourceText
        {
            get { return (string)GetValue(SetSourceTextProperty); }
            set { SetValue(SetSourceTextProperty, value); }
        }
        private static void OnSetSourceTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DownloadUIControl borderControl = d as DownloadUIControl;
            borderControl.OnSetSourceTextChanged(e);
        }
        private void OnSetSourceTextChanged(DependencyPropertyChangedEventArgs e)
        {
            SourceText.Text = e.NewValue.ToString();
        }
        //SetStatusText
        public static readonly DependencyProperty SetStatusTextProperty = DependencyProperty.Register("SetStatusText", typeof(string),
            typeof(DownloadUIControl), new PropertyMetadata("", new PropertyChangedCallback(OnSetStatusTextChanged)));

        public string SetStatusText
        {
            get { return (string)GetValue(SetStatusTextProperty); }
            set { SetValue(SetStatusTextProperty, value); }
        }
        private static void OnSetStatusTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DownloadUIControl borderControl = d as DownloadUIControl;
            borderControl.OnSetStatusTextChanged(e);
        }
        private void OnSetStatusTextChanged(DependencyPropertyChangedEventArgs e)
        {
            StatText.Text = e.NewValue.ToString();
        }
        //SetSourceTextVisibilty
        public static readonly DependencyProperty SetSourceTextVisibiltyProperty = DependencyProperty.Register("SetSourceTextVisibility", typeof(Visibility),
            typeof(DownloadUIControl), new PropertyMetadata(Visibility.Collapsed, new PropertyChangedCallback(OnSetSourceTextVisibiltyChanged)));

        public Visibility SetSourceTextVisibility
        {
            get { return (Visibility)GetValue(SetSourceTextVisibiltyProperty); }
            set { SetValue(SetSourceTextVisibiltyProperty, value); }
        }
        private static void OnSetSourceTextVisibiltyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DownloadUIControl borderControl = d as DownloadUIControl;
            borderControl.OnSetSourceTextVisibiltyChanged(e);
        }
        private void OnSetSourceTextVisibiltyChanged(DependencyPropertyChangedEventArgs e)
        {
            SourceText.Visibility = (Visibility)e.NewValue;
        }
        //SetMaximum
        public static readonly DependencyProperty SetMaximumProperty = DependencyProperty.Register("SetMaximum", typeof(double),
            typeof(DownloadUIControl), new PropertyMetadata((double)100, new PropertyChangedCallback(OnSetMaximumChanged)));

        public double SetMaximum
        {
            get { return (double)GetValue(SetMaximumProperty); }
            set { SetValue(SetMaximumProperty, value); }
        }
        private static void OnSetMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DownloadUIControl borderControl = d as DownloadUIControl;
            borderControl.OnSetMaximumChanged(e);
        }
        private void OnSetMaximumChanged(DependencyPropertyChangedEventArgs e)
        {
            progressbar.Maximum = (double)e.NewValue;
        }
        //SetTheValue
        public static readonly DependencyProperty SetTheValueProperty = DependencyProperty.Register("SetTheValue", typeof(double),
            typeof(DownloadUIControl), new PropertyMetadata((double)0, new PropertyChangedCallback(OnSetTheValueChanged)));

        public double SetTheValue
        {
            get { return (double)GetValue(SetTheValueProperty); }
            set { SetValue(SetTheValueProperty, value); }
        }
        private static void OnSetTheValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DownloadUIControl borderControl = d as DownloadUIControl;
            borderControl.OnSetTheValueChanged(e);
        }
        private void OnSetTheValueChanged(DependencyPropertyChangedEventArgs e)
        {
            progressbar.Value = (double)e.NewValue;
        }
        //SetCancelCommand
        public static readonly DependencyProperty SetCancelCommandProperty = DependencyProperty.Register("SetCancelCommand", typeof(ICommand),
            typeof(DownloadUIControl), new PropertyMetadata(null, new PropertyChangedCallback(OnSetCancelCommandChanged)));

        public ICommand SetCancelCommand
        {
            get { return (ICommand)GetValue(SetCancelCommandProperty); }
            set { SetValue(SetCancelCommandProperty, value); }
        }
        private static void OnSetCancelCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DownloadUIControl borderControl = d as DownloadUIControl;
            borderControl.OnSetCancelCommandChanged(e);
        }
        private void OnSetCancelCommandChanged(DependencyPropertyChangedEventArgs e)
        {
            CancelButton.Command = (ICommand)e.NewValue;

        }
        public RoutedEventHandler CancelClicked = null;
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (cts != null)
            {
                cts.Cancel();
                cts.Dispose();
            }
            this.Visibility = Visibility.Collapsed;
            if (CancelClicked != null)
                CancelClicked(sender, e);
        }
        public RoutedEventHandler CloseClicked = null;
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (cts != null)
            {
                cts.Cancel();
                cts.Dispose();
            }
            this.Visibility = Visibility.Collapsed;
            if (CloseClicked != null)
                CloseClicked(sender, e);
        }
    }
    
}