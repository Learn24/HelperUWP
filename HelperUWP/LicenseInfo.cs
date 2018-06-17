using HelperUWP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Store;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace HelperUWP
{

    public class LicenseInfo
    {

        public async static void RequestLicense(string productKey)
        {
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                try
                {
#if DEBUG
                    await CurrentAppSimulator.RequestProductPurchaseAsync(productKey);
#else
                                 var result = await CurrentApp.RequestProductPurchaseAsync(productKey);
#endif

                    if (licenseInformation.ProductLicenses[productKey].IsActive)
                    {

                        HelperUWP.Reporting.DisplayMessage("Successfully Purchased the product forever! " + HelperUWP.Reporting.AppName, "Successfully Purchased");

                    }
                    else
                    {
                        HelperUWP.Reporting.DisplayMessage("Product key was not purchased" + HelperUWP.Reporting.AppName + " Please restart app and try again" + " -> UpradePage");

                    }
                }
                catch (Exception ex)
                {
                    HelperUWP.Reporting.DisplayMessage(ex.Message + "Product key was not purchased" + HelperUWP.Reporting.AppName + " Please restart app and try again" + "->UpradePage");


                }

            }
            else
            {
                HelperUWP.Reporting.NoConnections();
            }
        }
        public async static Task<bool> LicenseRequestDirectAsync(string productKey, bool InternetEnabled)
        {
            bool IsSuccess = false;
            if (!licenseInformation.ProductLicenses[productKey].IsActive)
            {
                if (NetworkInterface.GetIsNetworkAvailable() == InternetEnabled)
                {
                    try
                    {
                        if (!licenseInformation.ProductLicenses[productKey].IsActive)
                        {
#if DEBUG
                            await CurrentAppSimulator.RequestProductPurchaseAsync(productKey);
#else
                                 var result = await CurrentApp.RequestProductPurchaseAsync(productKey);
#endif

                            if (licenseInformation.ProductLicenses[productKey].IsActive)
                            {

                                HelperUWP.Reporting.DisplayMessage("Product Purchased Successfully." + HelperUWP.Reporting.AppName, "Successfully Purchased");
                                IsSuccess = true;
                            }
                            else
                            {
                                HelperUWP.Reporting.DisplayMessage("Product key was not purchased" + HelperUWP.Reporting.AppName + " Please restart app and try again" + " -> UpradePage");
                                IsSuccess = false;

                            }
                        }
                        else
                        {
                            Reporting.DisplayMessage("Product already purchased");
                            IsSuccess = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        HelperUWP.Reporting.DisplayMessage(ex.Message + "Product key was not purchased" + HelperUWP.Reporting.AppName + " Please restart app and try again" + "->UpradePage");
                        IsSuccess = false;

                    }

                }
                else
                {
                    HelperUWP.Reporting.NoConnections();
                }
            }
            else
            {
                IsSuccess = true;
            }


               
            return IsSuccess;


        }


        /// <summary>
        /// Create a purchase message dialog with custom product key, Title, Description
        /// </summary>
        /// <param name="productKey"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        public async static Task<bool> DownloadLicenseDialogAsync(string productKey, string title, string description, string buttonColored, string buttonColorLess, bool InternetEnabled)
        {
            bool IsSuccessPurchase = false;
            try
            {
                if (!licenseInformation.ProductLicenses[productKey].IsActive)
                {
                    MessageDialog dialog = new MessageDialog(description, title);
                    dialog.Commands.Add(new UICommand(buttonColored, async (com) =>
                    {
                        if (NetworkInterface.GetIsNetworkAvailable() == InternetEnabled)
                        {
                            try
                            {
#if DEBUG
                                await CurrentAppSimulator.RequestProductPurchaseAsync(productKey);
#else
                                 var result = await CurrentApp.RequestProductPurchaseAsync(productKey);
#endif

                                if (licenseInformation.ProductLicenses[productKey].IsActive)
                                {

                                    HelperUWP.Reporting.DisplayMessage("Successfully Purchased the product forever! " + HelperUWP.Reporting.AppName, "Successfully Purchased");
                                    IsSuccessPurchase = true;
                                }
                                else
                                {
                                    HelperUWP.Reporting.DisplayMessage("Product key was not purchased" + HelperUWP.Reporting.AppName + " Please restart app and try again" + " -> UpradePage");
                                    IsSuccessPurchase = false;
                                }

                            }
                            catch (Exception ex)
                            {
                                HelperUWP.Reporting.DisplayMessage(ex.Message + "Product key was not purchased" + HelperUWP.Reporting.AppName + " Please restart app and try again" + "->UpradePage");
                                IsSuccessPurchase = false;
                            }

                        }
                        else
                        {
                            HelperUWP.Reporting.NoConnections();
                        }

                    }));
                    dialog.Commands.Add(new UICommand(buttonColorLess, (com) =>
                    {

                    }));
                    await dialog.ShowAsync();
                }
                else
                {
                    Reporting.DisplayMessage("Product key already purchased");
                    IsSuccessPurchase = true;
                }


            }
            catch (Exception ex)
            {
                HelperUWP.Reporting.DisplayMessage(ex.Message + ":Product key was not purchased" + HelperUWP.Reporting.AppName + " Please restart app and try again" + "->UpradePage");
                IsSuccessPurchase = false;
            }
            return IsSuccessPurchase;
        }
        /// <summary>
        /// Collapse if Productkey is not activated;
        /// </summary>
        public static Visibility LicenseVisibility(string produckKey)
        {
            if (!LicenseInfo.licenseInformation.ProductLicenses[produckKey].IsActive)
            {
                return Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                return Windows.UI.Xaml.Visibility.Collapsed;
            }
        }
        public static bool IsProductKeyActive(string productKey)
        {
            if (!licenseInformation.ProductLicenses[productKey].IsActive)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Get the price for the current product / product key.
        /// </summary>
        /// <param name="productKey"></param>
        /// <returns></returns>
        public async static Task<string> GetPriceAsync(string productKey)
        {
            string price = string.Empty;
            try
            {
                ListingInformation listings = await CurrentApp.LoadListingInformationAsync();
                ProductListing product = listings.ProductListings[productKey];

                 price = product.FormattedPrice;
            }
            catch { }
            return price;

        }
        public static async Task<string> GetPriceAsync(string productKey, string offlineText)
        {
            string price = "Purchase";
          
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                try
                {
                    ListingInformation listings = await CurrentApp.LoadListingInformationAsync();
                    ProductListing product = listings.ProductListings[productKey];

                    price = product.FormattedPrice;
                }
                catch
                {
                    price = offlineText;
                }
            }
            else
            {
                price = offlineText;

            }
            return price;
        }
    
    /// <summary>
    /// Put it in DEBUG mode// You need to create your own data.xml in Data Folder
    /// </summary>
#if DEBUG

       static LicenseChangedEventHandler licenseChangeHandler = null;
        /// <summary>
        /// HelperUWP.Data.data.xml
        /// </summary>
        //public static async void LicenseSimulator()
        //{
        //    StorageFolder proxyDataFolder = await Package.Current.InstalledLocation.GetFolderAsync("Data");
        //    StorageFile proxyFile = await proxyDataFolder.GetFileAsync("data.xml");
        //    licenseChangeHandler = new LicenseChangedEventHandler(InAppPurchaseRefreshScenario);
        //    CurrentAppSimulator.LicenseInformation.LicenseChanged += licenseChangeHandler;
        //    await CurrentAppSimulator.ReloadSimulatorAsync(proxyFile);

        //}
#endif
        private static void InAppPurchaseRefreshScenario()
        {
        }


        /// <summary>
        /// Set your own Uri file to locate the new Uri("ms-appx:///Data/Data.data.xml) file. Put it on "if DEBUG Region".
        /// </summary>
#if DEBUG
         
        public static async void LicenseSimulatorCustom(Uri uri)
        {
            try
            {
                var proxyFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uri);
                licenseChangeHandler = new LicenseChangedEventHandler(InAppPurchaseRefreshScenario);
                CurrentAppSimulator.LicenseInformation.LicenseChanged += licenseChangeHandler;
                await CurrentAppSimulator.ReloadSimulatorAsync(proxyFile);
            }
            catch { }

        }

#endif

#if DEBUG
        /// <summary>
        /// Use CurrentAppSimulator if in DEBUG Mode
        /// </summary>
        public static  LicenseInformation licenseInformation = CurrentAppSimulator.LicenseInformation;
#else
         public static  LicenseInformation licenseInformation = CurrentApp.LicenseInformation;
#endif




    }
}
