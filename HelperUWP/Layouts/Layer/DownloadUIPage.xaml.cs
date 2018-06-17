using HelperUWP.Controls.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
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
using Windows.Web;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HelperUWP.Layouts.Layer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DownloadUIPage : Page
    {
        private List<DownloadOperation> activeDownloads;
        private CancellationTokenSource cts;
        public DownloadUIPage()
        {
            this.InitializeComponent();
            cts = new CancellationTokenSource();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await DiscoverActiveDownloadsAsync();
            backButton.SetInitializeBackButton(this.Frame, true);

        }

        // Enumerate the downloads that were going on in the background while the app was closed.
        private async Task DiscoverActiveDownloadsAsync()
        {
          
            activeDownloads = new List<DownloadOperation>();

            IReadOnlyList<DownloadOperation> downloads = null;
            try
            {
                downloads = await BackgroundDownloader.GetCurrentDownloadsAsync();
            }
            catch (Exception ex)
            {
                Reporting.DisplayMessageExemption(ex);
            }

          // Log("Loading background downloads: " + downloads.);
            if (downloads.Count > 0)
            {
                List<Task> tasks = new List<Task>();
                foreach (DownloadOperation download in downloads)
                {
                    DownloadUIControl downloadControl = new DownloadUIControl();
                    downloadControl.AttachDownloadOperation(download, true);
                    MainPanel.Children.Add(downloadControl);
                    //downloadControl.SetStatusText = String.Format(CultureInfo.CurrentCulture,download.Progress.Status.ToString());
                    // Attach progress and completion handlers.
                  //  tasks.Add(HandleDownloadAsync(download, false));
                }

                // Don't await HandleDownloadAsync() in the foreach loop since we would attach to the second
                // download only when the first one completed; attach to the third download when the second one
                // completes etc. We want to attach to all downloads immediately.
                // If there are actions that need to be taken once downloads complete, await tasks here, outside
                // the loop.
                await Task.WhenAll(tasks);
            }
            else
            {
                Reporting.DisplayMessage("No pending downloads");
            }
        }
       

    }
}
