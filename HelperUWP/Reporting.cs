using HelperUWP.Common;
using HelperUWP.Common.Base;
using HelperUWP.Dialogs;
using Coding4Fun.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace HelperUWP
{
    public class Reporting
    {
       
        private static Package AppProperties = Windows.ApplicationModel.Package.Current;                     
        public static string AppName = AppProperties.DisplayName;
        public static string StorePublisherName = "StorePublisherName";
        /// <summary>
        /// Set Email Address first
        /// </summary>
        //Get the version of the app
        static string appVersion = string.Format("{0}.{1}.{2}.{3}", Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor, Package.Current.Id.Version.Build, Package.Current.Id.Version.Revision);
        /// <summary>
        /// This version has only 3 numbers example - 1.0.9
        /// </summary>
        public static string AppVersion = appVersion;

        public static async void DisplayMessageExemption(Exception exeption)
        {
#if DEBUG

            await Reporting.TextViewerAsync(exeption.ToString());
#else
            string exc = exeption.Message;
            MessageDialog dialog = new MessageDialog("There is an internal error" + "\"" + " Or try to restart your computer" + "," + " ErrorMessage " + "\"" + "\"" + exc + "\"", "Error");
            dialog.Commands.Add(new UICommand("Close", (com) =>
            {

            }));
            dialog.Commands.Add(new UICommand("Report", async (com) =>
            {

                string body = "ErrorMessage \"";
                var mailto = new Uri(EmailAds + AppName + "&body=" + body + exc + " "  +Environment.NewLine + DeviceInfo.GetDeviceInformation() + Environment.NewLine + "\"");
                await Launcher.LaunchUriAsync(mailto);

            }));
            await dialog.ShowAsync();
#endif
        }
        public static async void DisplayMessageDebugExemption(Exception exeption)
        {
#if DEBUG

            await Reporting.TextViewerAsync(exeption.ToString());
#else
#endif

        }
        public static async void DisplayMessageDebug(Exception DebugMessage)
        {
#if DEBUG

            await Reporting.TextViewerAsync(DebugMessage.ToString());
#else
#endif

        }
        public static async void DisplayMessageAction(string content, string title, string ButtonColoredContent, Action action)
        {          
            MessageDialog dialog = new MessageDialog(content, title);
            dialog.Commands.Add(new UICommand(ButtonColoredContent, (com) =>
            {
                action();
            }));
            dialog.Commands.Add(new UICommand("Cancel", (com) =>
            {          

            }));
            await dialog.ShowAsync();
            //return dialog;
        }
        public static async void DisplayMessageAction(string content, string title, string buttonContent1, string buttonContent2, Action action1, Action action2)
        {
            MessageDialog dialog = new MessageDialog(content, title);
            dialog.Commands.Add(new UICommand(buttonContent1, (com) =>
            {
                action1();
            }));
            dialog.Commands.Add(new UICommand(buttonContent2, (com) =>
            {

                action2();

            }));
            await dialog.ShowAsync();
            //return dialog;
        }

        /// <summary>
        /// Two parameter with only close button
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Title"></param>
        public static async void DisplayMessage(string Message, string Title)
        {           
            MessageDialog  dialog = new MessageDialog(Message, Title);
            dialog.Commands.Add(new UICommand("Close", (com) =>
            {

            }));          
            await dialog.ShowAsync();
        }
        /// <summary>
        /// One parameter with only close button
        /// </summary>
        /// <param name="Message"></param>
        public static async void DisplayMessage(string Message)
        {

            string message = Message;
            MessageDialog dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand("Close", (com) =>
            {

            }));

            await dialog.ShowAsync();
        }
      
        public static async void DisplayMessageError(Exception exeption, string EmailAds)
        {

            string exc = exeption.Message;
#if DEBUG

            await Reporting.TextViewerAsync(exeption.ToString());
#else
            MessageDialog dialog = new MessageDialog("There is an internal error" + "\"" + " Or try to restart your computer" + "," + " ErrorMessage " + "\"" + "\"" + exc + "\"", "Error");
            dialog.Commands.Add(new UICommand("Close", (com) =>          
            {

            }));
            dialog.Commands.Add(new UICommand("Report", async (com) =>
            {
                string body = "ErrorMessage \"";
                var mailto = new Uri(EmailAds + AppName + "&body=" + body + exc  +Environment.NewLine + DeviceInfo.GetDeviceInformation() + Environment.NewLine + "\"");
                await Launcher.LaunchUriAsync(mailto);

            }));
            await dialog.ShowAsync();
#endif
        }
        
        /// <summary>
        /// Display a good message. MessageDialog with two buttons Close & Send Report.
        /// </summary>
        /// <param name="MessageExemption"></param>
        /// <param name="Title"></param>
        public static async void DisplayMessageReport(string Message, string Title, string EmailAds)
        {

            string message = Message;
            string title = Title;
            MessageDialog dialog = new MessageDialog(message, title);
            dialog.Commands.Add(new UICommand("Close", (com) =>
            {

            }));
            dialog.Commands.Add(new UICommand("Send Report", async (com) =>
            {
                string body = message;
                var mailto = new Uri(EmailAds + title + "&body=" + body + Environment.NewLine + DeviceInfo.GetDeviceInformation() + Environment.NewLine );
                await Launcher.LaunchUriAsync(mailto);

            }));
            await dialog.ShowAsync();
            //return dialog;
        }       
        /// <summary>
        /// indicates that has no internet connections.
        /// </summary>
        public static async void NoConnections()
        {
            var messageDialog = new Windows.UI.Popups.MessageDialog("An internet connection not available. Please check your connection and try again", "No Connection");
            messageDialog.Commands.Add(new UICommand("Close", /*async*/ (command) =>
            {

            }));
            await messageDialog.ShowAsync();
        }
        public static void ToastPrompt(string title, string message, string imagePath, double imageWidth, double imageHeight)
        {
            ImageSource imageSource = new BitmapImage(new Uri(imagePath));
            var toast = new ToastPrompt();
            toast.Title = " " + title;
            toast.Message = " " + message;
            toast.Stretch = Stretch.Fill;
            toast.ImageWidth = imageWidth;
            toast.ImageHeight = imageHeight;
            toast.ImageSource = imageSource;
            toast.Show();
        }
        public static void ToastPrompt(string title, string message)
        {
            var toast = new ToastPrompt();
            toast.Title = " " + title;
            toast.Message = " " + message;
            toast.Height = 50;          
            toast.Show();
        }
        public static void ToastPrompt(string message)
        {
            var toast = new ToastPrompt();
            toast.Title = " ";
            toast.Height = 50;
            toast.Message = " " + message;
            toast.Show();
        }
        public static void ToastDebugPrompt(string message)
        {
#if DEBUG
            var toast = new ToastPrompt();
            toast.Title = " ";
            toast.Height = 50;
            toast.Message = " " + message;
            toast.Show();
#endif
        }
        public static ToastPrompt ToastPrompt()
        {
           return  new ToastPrompt();
        }
        public async static Task TextViewerAsync(string content)
        {
            TextViewerDialog textViewer = new TextViewerDialog();
            textViewer.SetText = content;
            await textViewer.ShowAsync();
        }   


    }
}
