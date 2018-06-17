using HelperUWP.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Windows.Phone.UI.Input;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System.Profile;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HelperUWP.Common
{
    public class DeviceInfo : BindableBase
    {
        /// <summary>
        /// True if it is mobile
        /// </summary>
        public static bool IsMobile
        {
            get
            {
                var qualifiers = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().QualifierValues;
                return (qualifiers.ContainsKey("DeviceFamily") && qualifiers["DeviceFamily"] == "Mobile");
            }
        }
        public static bool IsTabletMode
        {
            get
            {
                bool bIsTabletMode = false;

                var uiMode = UIViewSettings.GetForCurrentView().UserInteractionMode;

                if (uiMode == Windows.UI.ViewManagement.UserInteractionMode.Touch)

                    bIsTabletMode = true;

                else

                    bIsTabletMode = false;
                return bIsTabletMode;
            }
        }
        static ApplicationView view = ApplicationView.GetForCurrentView();
        public static bool IsFullScreenEnabled
        {
            get
            {
                return view.IsFullScreenMode;
            }          
        }
        public static bool IsNetworkAvailable
        {
            get
            {
                var internetIsOk = NetworkInterface.GetIsNetworkAvailable();
                return internetIsOk;
            }
        }

       
        public void DisplayOrientation(DisplayOrientations orientation)
        {
            switch (orientation)
            {
                case DisplayOrientations.Portrait:
                    DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
                    break;
                case DisplayOrientations.Landscape:
                    DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
                    break;
                case DisplayOrientations.None:
                    DisplayInformation.AutoRotationPreferences = DisplayOrientations.None;
                    break;
                case DisplayOrientations.PortraitFlipped:
                    DisplayInformation.AutoRotationPreferences = DisplayOrientations.PortraitFlipped;
                    break;
                case DisplayOrientations.LandscapeFlipped:
                    DisplayInformation.AutoRotationPreferences = DisplayOrientations.LandscapeFlipped;
                    break;
            }
            
        }
        public static string GetDeviceInformation()
        {
            string infoString = " SystemFamily: " + Info.SystemFamily + " "+ Environment.NewLine +
                "SystemVersion: " + Info.SystemVersion + " " + Environment.NewLine +
                "SystemArchitecture: " + Info.SystemArchitecture + " " + Environment.NewLine +
                "ApplicationName: " + Info.ApplicationName + " " + Environment.NewLine +
                "ApplicationVersion: " + Info.ApplicationVersion + " " + Environment.NewLine +
                "DeviceManufacturer: " + Info.DeviceManufacturer + " " + Environment.NewLine +
                "DeviceModel: " + Info.DeviceModel + " " + Environment.NewLine +
                "DeviceType: " + Info.DeviceType + " " + Environment.NewLine +
                "FreeSpace: " + Info.FreeSpace + " " + Environment.NewLine;
            return infoString;
        }

    }
    internal static class  Info
    {
        public static string SystemFamily { get; }
        public static string SystemVersion { get; }
        public static string SystemArchitecture { get; }
        public static string ApplicationName { get; }
        public static string ApplicationVersion { get; }
        public static string DeviceManufacturer { get; }
        public static string DeviceModel { get; }
       
        public static string DeviceType { get; }
        public static string FreeSpace { get; set; } 

        static Info()
        {
            // get the system family name
            AnalyticsVersionInfo ai = AnalyticsInfo.VersionInfo;
            SystemFamily = ai.DeviceFamily;

            // get the system version number
            string sv = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
            ulong v = ulong.Parse(sv);
            ulong v1 = (v & 0xFFFF000000000000L) >> 48;
            ulong v2 = (v & 0x0000FFFF00000000L) >> 32;
            ulong v3 = (v & 0x00000000FFFF0000L) >> 16;
            ulong v4 = (v & 0x000000000000FFFFL);
            SystemVersion = $"{v1}.{v2}.{v3}.{v4}";

            // get the package architecure
            Package package = Package.Current;
            SystemArchitecture = package.Id.Architecture.ToString();

            // get the user friendly app name
            ApplicationName = package.DisplayName;

            // get the app version
            PackageVersion pv = package.Id.Version;
            ApplicationVersion = $"{pv.Major}.{pv.Minor}.{pv.Build}.{pv.Revision}";

            // get the device manufacturer and model name
            EasClientDeviceInformation eas = new EasClientDeviceInformation();
            DeviceManufacturer = eas.SystemManufacturer;
            DeviceModel = eas.SystemProductName;

            //custom
            if (DeviceInfo.IsMobile)
            {
                DeviceType = "Mobile";
            }
            else
            {
                DeviceType = "Unknown";
            }
            InitializeInfo();
        }
        private async static void InitializeInfo()
        {
            var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var free = await StorageData.GetFreeSpaceKilobytesAsync(folder);
            FreeSpace = free.ToString();
        }
    }
}
