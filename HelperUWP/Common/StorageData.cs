using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace HelperUWP.Common
{
    public  class StorageData
    {
        public static Stream GetEmbeddedResourceStreamAsync(string embeddedResourcePath, Type classType)
        {
            //Class name is CommonsExtensions
            var assembly = classType.GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(embeddedResourcePath);
            return stream;
        }
        public static bool ClipboardCopyText(string text)
        {
            bool success = false;
            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText(text);
            try
            {
                // Set the DataPackage to clipboard.
                Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;

                Reporting.DisplayMessageDebugExemption(ex);
            }
            return success;
        }
        public static bool ClipboardCopyFile(StorageFile file)
        {
            bool success = false;
            DataPackage dataPackage = new DataPackage();
            List<IStorageItem> imageItems = new List<IStorageItem>();
            imageItems.Add(file);
            dataPackage.SetStorageItems(imageItems);
            try
            {
                // Set the DataPackage to clipboard.
                Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;

                Reporting.DisplayMessageDebugExemption(ex);
            }
            return success;
        }
        #region Settings
        public static  Windows.Storage.ApplicationDataContainer LocalSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public static Windows.Storage.ApplicationDataContainer RoamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

        /// <summary>
        /// Save string to local setting 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fileName"></param>
        /// <param name="ModelValue"></param>
        public static void SaveLocalSetting(string key,  string value)
        {      
            try
            {                           
                if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
                {
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values.Remove(key);
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values[key] = value;
                }
                else
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values[key] = value;
            } 
            catch(Exception ex) { Reporting.DisplayMessageExemption(ex); }    
        }
        public static bool SettingContainsKey(string key)
        {
            return Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey(key);
        }
        /// <summary>
        /// Get string from local setting
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetLocalSetting(string key)
        {
            string str = "";
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
            {              
               str =  Windows.Storage.ApplicationData.Current.LocalSettings.Values[key].ToString();              
            }
            return str;

        }
        public static object GetLocalObjSetting(string key)
        {
            object obj = null;
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
            {
                obj = Windows.Storage.ApplicationData.Current.LocalSettings.Values[key];
            }
            return obj;

        }
        public static void SaveLocalSetting(string key, object value)
        {
            try
            {
                if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
                {
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values.Remove(key);
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values[key] = value;
                }
                else
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values[key] = value;
            }
            catch (Exception ex) { Reporting.DisplayMessageExemption(ex); }
        }
        //Save to Roaming Settings
        public static void SaveRoamingSetting(string key, object value)
        {
            try
            {
                if (Windows.Storage.ApplicationData.Current.RoamingSettings.Values.ContainsKey(key))
                {
                    Windows.Storage.ApplicationData.Current.RoamingSettings.Values.Remove(key);
                    Windows.Storage.ApplicationData.Current.RoamingSettings.Values[key] = value;
                }
                else
                    Windows.Storage.ApplicationData.Current.RoamingSettings.Values[key] = value;
            }
            catch (Exception ex) { Reporting.DisplayMessageExemption(ex); }
        }
        /// <summary>
        /// Get string from Roaming setting
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetRoamingSetting(string key)
        {
            object obj = null;
            if (Windows.Storage.ApplicationData.Current.RoamingSettings.Values.ContainsKey(key))
            {
                obj = Windows.Storage.ApplicationData.Current.RoamingSettings.Values[key].ToString();
            }
            return obj;

        }
        #endregion
        #region Json
        /// <summary>
        /// Convert ObjectModel to json string
        /// </summary>
        /// <param name="ModelValue"></param>
        /// <returns></returns>
        public static string SerializeObjectToString(object ModelValue)
        {
            string fileName = string.Empty;
            var viewModel = ModelValue;
            fileName =  JsonConvert.SerializeObject(viewModel);   
                     
            return  fileName;
        }
       

        /// <summary>
        /// Convert json string to ObjectModel.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T DeserializeStringToObject<T>(string value)  
        {
            var obj =  JsonConvert.DeserializeObject<T>(value);
            return obj;
        }
        #endregion
        #region xml
        /// <summary>
        /// Serialize a [Serializable] object of type T into an XML-formatted string using XmlSerializer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">any T object</param>
        /// <returns>an XML-formatted string</returns>
        public static string SerializeToXML<T>(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                using (var stringWriter = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(stringWriter))
                    {
                        xmlserializer.Serialize(writer, value);
                        return stringWriter.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }
        /// <summary>
        /// De-serialize a [Serializable] object of type T into an XML-formatted string using XmlSerializer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">any T object</param>
        /// <returns>an XML-formatted string</returns>
        public static T DeserializeFromXML<T>(string xml)
        {
            if (String.IsNullOrEmpty(xml)) throw new NotSupportedException("ERROR: input string cannot be null.");
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                using (var stringReader = new StringReader(xml))
                {
                    using (var reader = XmlReader.Create(stringReader))
                    {
                        return (T)xmlserializer.Deserialize(reader);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }
        #endregion

        #region Deleting Files
        public static async Task DeleteFileFromCustomFolderAsync(string fileNameWithExtension, StorageFolder currentFolder)
        {
            var file = await currentFolder.GetFileAsync(fileNameWithExtension);
            await file.DeleteAsync();
        }
        #endregion
        #region Getting or Saving File
        /// <summary>
        /// @"Images\Sample\CollectionImageNull.png"
        /// </summary>
        /// <param name="fileNameWithExtension"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<StorageFile> GetFileFromInstalledFolderAsync(string path)
        {
            var file = await StorageFile.GetFileFromPathAsync(Path.Combine(Package.Current.InstalledLocation.Path, path));
            Windows.Storage.StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            return file;
        }
        /// <summary>
        /// Save file using FileSavePicker
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>  
        public static async Task<StorageFile> FileSavePickerAsync(StorageFile fileToSave)
        {

            byte[] buffer;
            Stream stream = await fileToSave.OpenStreamForReadAsync();
            buffer = new byte[stream.Length];
            await stream.ReadAsync(buffer, 0, (int)stream.Length);
            var savePicker = new FileSavePicker();
            savePicker.FileTypeChoices.Add(fileToSave.Name, new List<string>() { fileToSave.FileType });
            savePicker.SuggestedSaveFile = fileToSave;
            savePicker.SuggestedFileName = fileToSave.Name;
            var file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteBytesAsync(file, buffer);
                await CachedFileManager.CompleteUpdatesAsync(file);
            }
            return file;

        }
        //Local folder
        public async static void SaveFileToCustomFolder(StorageFile storageFile, StorageFolder destinationFolder)
        {
            try
            {             
                await storageFile.CopyAsync(destinationFolder, storageFile.Name, NameCollisionOption.ReplaceExisting);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public static async Task<StorageFile> GetFileFromCustomFolderAsync(string fileNameWithExtension, StorageFolder folder)
        {
            var file = await folder.GetFileAsync(fileNameWithExtension);
            return file;
        }
        /// <summary>
        /// Save string to local Folder as Text.txt File. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fileName"></param>
        /// <param name="ModelValue"></param>
        public async static Task<StorageFile> SaveStringAsTextFileToFolderAsync(string desiredNameWithExtenstion, string contentValue, StorageFolder destinationFolder = null)
        {
            StorageFile TextFile = null;
            if(destinationFolder == null) destinationFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            TextFile =
              await destinationFolder.CreateFileAsync(desiredNameWithExtenstion, CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(TextFile, contentValue);
            return TextFile;
        }
       
        public async static Task<string> GetTextFileAsStringFromFolderAsync(string desiredNameWithExtenstion, StorageFolder destinationFolder = null)
        {
            if (destinationFolder == null) destinationFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile TextFile =
                await destinationFolder.GetFileAsync(desiredNameWithExtenstion);
            return await Windows.Storage.FileIO.ReadTextAsync(TextFile);
        }
        public static StorageFolder LocalFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        public static StorageFolder TemporaryFolder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
        public static StorageFolder RoamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;     
        #endregion
        #region File Picker or Saver       
        public static async Task<StorageFile> PickSingleFileAsync(params string[] fileExtensions)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            foreach(var str in fileExtensions)
            {
                openPicker.FileTypeFilter.Add(str.ToLower());
            }
            StorageFile imgFile = await openPicker.PickSingleFileAsync(); 
            if(imgFile == null)
            {                
            }          
            return imgFile;

        }
        public static async Task<IReadOnlyList<StorageFile>> PickMultipleFilesAsync(params string[] fileExtensions)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            foreach (var str in fileExtensions)
            {
                openPicker.FileTypeFilter.Add(str.ToLower());
            }
            var FileList = await openPicker.PickMultipleFilesAsync();
            if (FileList == null)
            {
            }
            return FileList;

        }
        public async static Task<StorageFolder> PickSingleFolderAsync()
        {
            FolderPicker GetFolder = new FolderPicker();
            GetFolder.FileTypeFilter.Add(".File folder");
            var folder = await GetFolder.PickSingleFolderAsync();
            return folder;         
        }
        #endregion
        #region Camera
        public static async Task<StorageFile> CameraCaptureAsync(bool CropEnabled, CameraCaptureUIMode cameraCaptureUIMode)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.VideoSettings.Format = CameraCaptureUIVideoFormat.Mp4;
            captureUI.VideoSettings.AllowTrimming = true;
            captureUI.PhotoSettings.AllowCropping = CropEnabled;
            if (CropEnabled)
            {
                captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);
            }
            StorageFile imgFile = await captureUI.CaptureFileAsync(cameraCaptureUIMode);
            return imgFile;
        }
        #endregion
        #region Camera w size
        public static async Task<StorageFile> CameraCaptureAsync(bool CropEnabled, CameraCaptureUIMode cameraCaptureUIMode, Size size)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.VideoSettings.Format = CameraCaptureUIVideoFormat.Mp4;
            captureUI.VideoSettings.AllowTrimming = true;
            captureUI.PhotoSettings.AllowCropping = CropEnabled;
            if (CropEnabled)
            {
                captureUI.PhotoSettings.CroppedSizeInPixels = size;
            }
            StorageFile imgFile = await captureUI.CaptureFileAsync(cameraCaptureUIMode);
            return imgFile;
        }
        #endregion
        #region StorageFile Sizes 
        public async static Task<double> GetFileSizeAsync(StorageFile file)
        {
            Windows.Storage.FileProperties.BasicProperties basicProperties = await file.GetBasicPropertiesAsync();
            ulong fileSize = basicProperties.Size;        
            return fileSize;

        }
        public static  bool IsValidFilename(string fileName)
        {
            return fileName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;            
        }
        public static string GetValidFileName(string fileName)
        {
            if (IsValidFilename(fileName))
                return fileName;
            foreach (var ch in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(ch.ToString(), "");
            }
            return fileName;
        }
        #endregion
        #region Get Free Space
        public static async Task<UInt64> GetFreeSpaceBytesAsync(StorageFolder folder)
        {
            var retrivedProperties = await folder.Properties.RetrievePropertiesAsync(new string[] { "System.FreeSpace" });
            return (UInt64)retrivedProperties["System.FreeSpace"];
        }
        public static async Task<double> GetFreeSpaceKilobytesAsync(StorageFolder folder)
        {
            var retrivedProperties = await folder.Properties.RetrievePropertiesAsync(new string[] { "System.FreeSpace" });
            var folderSize = (UInt64)retrivedProperties["System.FreeSpace"];
            var KBSize = folderSize / 1024.0;
            return KBSize;
        }
        public async static Task<double> GetfreeSpaceGigabytesAsync(StorageFolder folder)
        {
            var KBSize = await GetFreeSpaceKilobytesAsync(folder);
            var GBSize = KBSize / (1024.0 * 1024.0);
            return GBSize; 
        }
        #endregion
        #region CheckFileExist
        public static async Task<bool> IsFileExistAsync(string fileName, StorageFolder folder)
        {
            try
            {
                var item = await folder.TryGetItemAsync(fileName);
                return item != null;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
