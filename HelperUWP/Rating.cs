using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System;
using Windows.UI.Popups;

namespace HelperUWP
{
    public class Rating
    {
        private const string RateKey = "askforreviewKey";
        private const string StartedKey = "startedKey";
        public static void RateCounter(int count)
        {
            try
            {
                var settingsContainer = ApplicationData.Current.RoamingSettings;
                settingsContainer.Values[RateKey] = false;
                int started = 0;
                if (settingsContainer.Values.ContainsKey(StartedKey))
                {
                    started = (int)settingsContainer.Values[StartedKey];
                }
                started++;
                settingsContainer.Values[StartedKey] = started;
                if (started == count)
                {
                    settingsContainer.Values[RateKey] = true;
                }
                Rating.RateAndReview();


            }
            catch (Exception ex)
            {
                Reporting.DisplayMessage(ex.Message);
                return;
            }
        }
        public static void RateCounterCustom(int count, string message, string title, string buttonColoredConten)
        {
            try
            {
                var settingsContainer = ApplicationData.Current.RoamingSettings;
                settingsContainer.Values[RateKey] = false;
                int started = 0;
                if (settingsContainer.Values.ContainsKey(StartedKey))
                {
                    started = (int)settingsContainer.Values[StartedKey];
                }
                started++;
                settingsContainer.Values[StartedKey] = started;
                if (started == count)
                {
                    settingsContainer.Values[RateKey] = true;
                }
                Rating.RateAndReview(message, title, buttonColoredConten);


            }
            catch (Exception ex)
            {
                Reporting.DisplayMessage(ex.Message);
                return;
            }
        }
        private async static void RateAndReview()
        {
            var settingsContainer = ApplicationData.Current.RoamingSettings;
            //settingsContainer.Values["askforreview"] = false;
            var askforReview = (bool)settingsContainer.Values[RateKey];
            if (askforReview)
            {
                settingsContainer.Values[RateKey] = false;
                if (settingsContainer.Values.ContainsKey(RateKey))
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog("We'd love you to rate our app 5 stars in just 5 seconds Showing us some love on the store helps us to continue to work on the app and make things even better!", "Please Rate this App in 5 Seconds");
                    messageDialog.Commands.Add(new UICommand("Rate 5 Stars", async (command) =>
                    {
                        //Ratings For Windows
                        await Launcher.LaunchUriAsync(new Uri(
                            String.Format("ms-windows-store:REVIEW?PFN={0}", Windows.ApplicationModel.Package.Current.Id.FamilyName)));
                    }));
                    messageDialog.Commands.Add(new UICommand("Later", /*async */(command) =>
                    {
                        //if (settingsContainer.Containers.ContainsKey("askforreview") && (settingsContainer.Containers.ContainsKey("started")))
                        //{
                        settingsContainer.Values.Remove(RateKey);
                        settingsContainer.Values.Remove(StartedKey);
                        settingsContainer.Values[RateKey] = false;


                        // }
                    }));
                    await messageDialog.ShowAsync();
                }
            }

        }
        private async static void RateAndReview(string message, string title, string buttonColoredConten)
        {
            var settingsContainer = ApplicationData.Current.RoamingSettings;
            //settingsContainer.Values["askforreview"] = false;
            var askforReview = (bool)settingsContainer.Values[RateKey];
            if (askforReview)
            {
                settingsContainer.Values[RateKey] = false;
                if (settingsContainer.Values.ContainsKey(RateKey))
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog(message, title);
                    messageDialog.Commands.Add(new UICommand(buttonColoredConten, async (command) =>
                    {
                        //Ratings For Windows
                        await Launcher.LaunchUriAsync(new Uri(
                            String.Format("ms-windows-store:REVIEW?PFN={0}", Windows.ApplicationModel.Package.Current.Id.FamilyName)));
                    }));
                    messageDialog.Commands.Add(new UICommand("Later", /*async */(command) =>
                    {
                        //if (settingsContainer.Containers.ContainsKey("askforreview") && (settingsContainer.Containers.ContainsKey("started")))
                        //{
                        settingsContainer.Values.Remove(RateKey);
                        settingsContainer.Values.Remove(StartedKey);
                        settingsContainer.Values[RateKey] = false;


                        // }
                    }));
                    await messageDialog.ShowAsync();
                }
            }

        }
    }
}
