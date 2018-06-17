using HelperUWP.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HelperUWP.Common
{
    public class SupportHelper
    {

        /// <summary>
        /// Rate and review this app
        /// </summary>
        public static async void RateThisApp()
        {
            await Launcher.LaunchUriAsync(new Uri(
                            String.Format("ms-windows-store:REVIEW?PFN={0}", Windows.ApplicationModel.Package.Current.Id.FamilyName)));
        }
        /// <summary>
        /// Launch the store app list
        /// </summary>
        public static async void MoreAppList(string storePublisherName = null)
        {
            if (storePublisherName == null)
                storePublisherName = Reporting.StorePublisherName;
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store:Publisher?name=" + storePublisherName));
        }

        /// <summary>
        /// Send and email
        /// </summary>
        public async static void ContactEmail(string supportEmail)
        {
            string subject = Reporting.AppName;
            string body = "";
            var emailMessage = new Windows.ApplicationModel.Email.EmailMessage();
            emailMessage.Body = body;

            var emailRecipient = new Windows.ApplicationModel.Email.EmailRecipient(supportEmail);
            emailMessage.To.Add(emailRecipient);
            emailMessage.Subject = subject;
            await Windows.ApplicationModel.Email.EmailManager.ShowComposeNewEmailAsync(emailMessage);


        }
        public static async void OpenAppDetail(string appId)
        {
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store://pdp/?PFN=" + appId));
        }

    }
}
