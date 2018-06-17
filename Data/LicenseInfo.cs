using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Store;
using Windows.Storage;
using Windows.UI.Popups;

namespace Backgrounds_HD_Universal.Data
{

    public class LicenseInfo
    {

        public const string ProductKey = "Backgrounds";

#if DEBUG
        LicenseChangedEventHandler licenseChangeHandler = null;
        public async void LicenseSimulator()
        {
            StorageFolder proxyDataFolder = await Package.Current.InstalledLocation.GetFolderAsync("Data");
            StorageFile proxyFile = await proxyDataFolder.GetFileAsync("data.xml");
            licenseChangeHandler = new LicenseChangedEventHandler(InAppPurchaseRefreshScenario);
            CurrentAppSimulator.LicenseInformation.LicenseChanged += licenseChangeHandler;
            await CurrentAppSimulator.ReloadSimulatorAsync(proxyFile);

        }
#endif
        private void InAppPurchaseRefreshScenario()
        {
        }

#if DEBUG
        public async void BuyLicense()
        {
            LicenseInformation licenseInformation = CurrentAppSimulator.LicenseInformation;
            var messageDialog = new Windows.UI.Popups.MessageDialog("You can only save 1 backgrounds each time. Would you like to buy Backgrounds Wallpapers key to unlock all features?", "Trial Version");
            // Add commands and set their callbacks
            messageDialog.Commands.Add(new UICommand("Buy", async (command) =>
            {

                if (!licenseInformation.ProductLicenses[LicenseInfo.ProductKey].IsActive)
                {

                    try
                    {
                        await CurrentAppSimulator.RequestProductPurchaseAsync("RemoveLock");
                        if (licenseInformation.ProductLicenses[LicenseInfo.ProductKey].IsActive)
                        {
                            // var m 
                            var mi = new Windows.UI.Popups.MessageDialog("Please restart the application to reload all purchased products", "Purchased Successfully, Restart The Application");
                            mi.Commands.Add(new UICommand("Exit Now", (com) =>
                            {
                                App.Current.Exit();
                            }));
                            mi.Commands.Add(new UICommand("Later", (com) =>
                            {

                            }));
                            await mi.ShowAsync();
                        }
                        else
                        {
                            MessageDialog m = new MessageDialog("Product key was not purchased, Please check your connections!");
                            await m.ShowAsync();
                        }
                    }
                    catch (Exception)
                    {
                        MessageDialog m = new MessageDialog("Unable to buy Product key, Please check your connections!");
                        await m.ShowAsync();
                    }
                }
                else
                {
                    // Product2SellMessage.Text = "You already own Product 2.";

                }

            }));
            messageDialog.Commands.Add(new UICommand("Cancel", (command) =>
            {

            }));
            await messageDialog.ShowAsync();
        }

#endif




    }
}
