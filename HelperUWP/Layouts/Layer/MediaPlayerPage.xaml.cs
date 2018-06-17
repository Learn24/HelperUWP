using HelperUWP.Common;
using HelperUWP.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HelperUWP.Layouts.Layer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MediaPlayerPage : Page
    {
        public MediaPlayerPage()
        {
            this.InitializeComponent();
            this.SizeChanged += (f, g) =>
            {
                mediaPlayer.Width = this.Width;
                mediaPlayer.Height = this.Height;
            };

        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            try
            {
                progress.IsActive = true;
                await TaskRunExtensions.ToTaskAsync(async () =>
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, async () =>
                    {
                        var file = (StorageFile)e.Parameter;
                        mediaPlayer.SetSource(await file.OpenAsync(FileAccessMode.Read), file.ContentType);
                        mediaPlayer.Play();
                        progress.IsActive = false;
                    });
                });

               
            }
            catch(Exception ex)
            {
                Reporting.DisplayMessage(ex.Message);
                this.Frame.GoBack();
            }
            
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            var frame = Window.Current.Content as Frame;
           
        }
    }
}
