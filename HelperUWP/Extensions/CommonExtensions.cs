using AppStudio.DataProviders.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace HelperUWP.Extensions
{
    public static class CommonExtensions
    {
        /// <summary>
        /// Extenstion for Converting StorageFile to ByteArray. 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static async Task<byte[]> AsByteArrayAsync(this StorageFile file)
        {
            IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
            var reader = new Windows.Storage.Streams.DataReader(fileStream.GetInputStreamAt(0));
            await reader.LoadAsync((uint)fileStream.Size);

            byte[] pixels = new byte[fileStream.Size];

            reader.ReadBytes(pixels);

            return pixels;
        }
        public static async Task<byte[]> AsByteArrayAsync(this IRandomAccessStream iRandomAccessStream)
        {
            IRandomAccessStream fileStream = iRandomAccessStream;
            var reader = new Windows.Storage.Streams.DataReader(fileStream.GetInputStreamAt(0));
            await reader.LoadAsync((uint)fileStream.Size);
            byte[] pixels = new byte[fileStream.Size];
            reader.ReadBytes(pixels);
            return pixels;
        }
        public static async Task<byte[]> AsByteArrayAsync(this StorageFile file, string ReplaceLocalFileNameWithExtension, uint ImageWidthHeight)
        {         
            var imgThumbnail = await file.GetThumbnailAsync(ThumbnailMode.PicturesView, ImageWidthHeight, ThumbnailOptions.ResizeThumbnail);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.DecodePixelHeight = (int)ImageWidthHeight;
            bitmapImage.DecodePixelWidth = (int)ImageWidthHeight;
            bitmapImage.SetSource(imgThumbnail);
          
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(imgThumbnail.CloneStream());
            SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();
            var LocalFolder = ApplicationData.Current.LocalFolder;
            StorageFile file_Save = await LocalFolder.CreateFileAsync(ReplaceLocalFileNameWithExtension, CreationCollisionOption.ReplaceExisting);
            BitmapEncoder encoder = await BitmapEncoder.CreateAsync(Windows.Graphics.Imaging.BitmapEncoder.JpegEncoderId, await file_Save.OpenAsync(FileAccessMode.ReadWrite));

            encoder.SetSoftwareBitmap(softwareBitmap);
            encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Linear;
            encoder.BitmapTransform.ScaledHeight = ImageWidthHeight;
            encoder.BitmapTransform.ScaledWidth = ImageWidthHeight;
            encoder.BitmapTransform.Bounds = new BitmapBounds()
            {
                Width = ImageWidthHeight,
                Height = ImageWidthHeight,
                
            };

            await encoder.FlushAsync();

            IRandomAccessStream fileStream = await file_Save.OpenAsync(FileAccessMode.ReadWrite);
            var reader = new Windows.Storage.Streams.DataReader(fileStream.GetInputStreamAt(0));
            await reader.LoadAsync((uint)fileStream.Size);

            byte[] pixels = new byte[fileStream.Size];

            reader.ReadBytes(pixels);

            return pixels;
        }
        public static byte[] AsByteArray(this WriteableBitmap bitmap)
        {
            using (Stream stream = bitmap.PixelBuffer.AsStream())
            {
                MemoryStream memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Extenstion for Converting ByteArray to BitmapImage or as ImageSource. 
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static BitmapImage AsBitmapImage(this byte[] byteArray)
        {
            if (byteArray != null)
            {
                using (var stream = new InMemoryRandomAccessStream())
                {
                    stream.WriteAsync(byteArray.AsBuffer()).GetResults(); // I made this one synchronous on the UI thread; this is not a best practice.
                    var image = new BitmapImage();
                    stream.Seek(0);
                    image.SetSource(stream);
                    return image;
                }
            }

            return null;
        }
        public static BitmapImage AsBitmapImage(this byte[] byteArray, int ImageWidthHeight)
        {
            if (byteArray != null)
            {
                using (var stream = new InMemoryRandomAccessStream())
                {
                    stream.WriteAsync(byteArray.AsBuffer()).GetResults(); // I made this one synchronous on the UI thread; this is not a best practice.
                    var image = new BitmapImage();
                    image.DecodePixelWidth = ImageWidthHeight;
                    image.DecodePixelHeight = ImageWidthHeight;
                    stream.Seek(0);
                    image.SetSource(stream);
                    return image;
                }
            }

            return null;
        }
        /// <summary>
        /// Extenstion for Converting StorageFile to BitmapImage or as ImageSource. 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static async Task<BitmapImage> AsBitmapImageAsync(this StorageFile file)
        {
            var stream = await file.OpenAsync(FileAccessMode.Read);
            var bitmapImage = new BitmapImage();
            await bitmapImage.SetSourceAsync(stream);
            return bitmapImage;
        }
        public static async Task<BitmapImage> AsBitmapImageAsync(this StorageFile file, int ImageWidthHeight)
        {
            var stream = await file.OpenAsync(FileAccessMode.Read);
            var bitmapImage = new BitmapImage();
            bitmapImage.DecodePixelWidth = ImageWidthHeight;
            bitmapImage.DecodePixelHeight = ImageWidthHeight;
            await bitmapImage.SetSourceAsync(stream);
            return bitmapImage;
        }
       
        /// <summary>
        ///  Extenstion for Converting Byte Array to StorageFile ready to save to Picture Library. 
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static async Task<StorageFile> AsStorageFileAsync(this byte[] byteArray, string ReplaceLocalFileNameWithExtension)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync(ReplaceLocalFileNameWithExtension, CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteBytesAsync(sampleFile, byteArray);
            return sampleFile;
        }
       

        public static async Task<string> StorageFileToBase64String(this StorageFile fileToBase64String)
        {
            string Base64String = "";

            if (fileToBase64String != null)
            {
                IRandomAccessStream fileStream = await fileToBase64String.OpenAsync(FileAccessMode.Read);
                var reader = new DataReader(fileStream.GetInputStreamAt(0));
                await reader.LoadAsync((uint)fileStream.Size);
                byte[] byteArray = new byte[fileStream.Size];
                reader.ReadBytes(byteArray);
                Base64String = Convert.ToBase64String(byteArray);
            }

            return Base64String;
        }
        public static async Task<StorageFile> Base64StringToStorageFile(this string base64String, string fileName)
        {
            byte[] byteArray = Convert.FromBase64String(base64String);
            var file = await byteArray.AsStorageFileAsync(fileName);
            return file;
        }

        /// <summary>
        /// Get file size in KB
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async static Task<double> GetFileKBSizeAsync(this StorageFile file)
        {
            Windows.Storage.FileProperties.BasicProperties basicProperties = await file.GetBasicPropertiesAsync();
            ulong fileSize = basicProperties.Size;
            var KBSize = fileSize / 1024.0;
            return KBSize;

        }
        public async static Task<string> GetFileSizeToStringAsync(this StorageFile file)
        {
            Windows.Storage.FileProperties.BasicProperties basicProperties = await file.GetBasicPropertiesAsync();
            ulong bytes = basicProperties.Size;

            double byteCount = (double)bytes;
            string size = "0 Bytes";
            if (byteCount >= 1073741824.0)
                size = String.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
            else if (byteCount >= 1048576.0)
                size = String.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
            else if (byteCount >= 1024.0)
                size = String.Format("{0:##.##}", byteCount / 1024.0) + " KB";
            else if (byteCount > 0 && byteCount < 1024.0)
                size = byteCount.ToString() + " Bytes";

            return size;

        }
        public static string AsHtmlFormatFromString(this string content)
        {
            var data = HtmlFormatHelper.CreateHtmlFormat(content);
            return data;
        }

        public static string AsStringFromHtmlFormat(this string HtmlFormatString)
        {
            var data = HtmlFormatString.DecodeHtml();
            return data;
        }
        //list extension
        /// <summary>
        ///  list.WhereAtleastOneProperty((string s) => s=="stringSample"); or var filterList = list..WhereAtleastOneProperty((string s) => s.Contains("stringSample")).ToList();
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="PropertyType"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> WhereAtleastOneProperty<T, PropertyType>(this IEnumerable<T> source,
            Predicate<PropertyType> predicate)
        {
            var properties = typeof(T).GetProperties().Where(prop => prop.CanRead && prop.PropertyType == typeof(PropertyType)).ToArray();
            return source.Where(item => properties.Any(prop => PropertySatisfiesPredicate(predicate, item, prop)));
        }
        private static bool PropertySatisfiesPredicate<T, PropertyType>(Predicate<PropertyType>
            predicate, T item, System.Reflection.PropertyInfo pro)
        {
            try
            {
                return predicate((PropertyType)pro.GetValue(item));
            }
            catch
            {
                return false;
            }
        }
        public static async Task<StorageFile> AsUIScreenShotFileAsync(this UIElement elememtName, string ReplaceLocalFileNameWithExtension)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(ReplaceLocalFileNameWithExtension, CreationCollisionOption.ReplaceExisting);
            try
            {
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap();
                InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
                // Render to an image at the current system scale and retrieve pixel contents 
                await renderTargetBitmap.RenderAsync(elememtName);
                var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();
                // Encode image to an in-memory stream 
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)renderTargetBitmap.PixelWidth, (uint)renderTargetBitmap.PixelHeight,
                    DisplayInformation.GetForCurrentView().LogicalDpi,
                    DisplayInformation.GetForCurrentView().LogicalDpi, pixelBuffer.ToArray());
                await encoder.FlushAsync();

                //CreatingFolder
               // var folder = Windows.Storage.ApplicationData.Current.LocalFolder;

                RandomAccessStreamReference rasr = RandomAccessStreamReference.CreateFromStream(stream);
                var streamWithContent = await rasr.OpenReadAsync();
                byte[] buffer = new byte[streamWithContent.Size];
                await streamWithContent.ReadAsync(buffer.AsBuffer(), (uint)streamWithContent.Size, InputStreamOptions.None);


                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))

                {

                    using (IOutputStream outputStream = fileStream.GetOutputStreamAt(0))

                    {

                        using (DataWriter dataWriter = new DataWriter(outputStream))

                        {

                            dataWriter.WriteBytes(buffer);

                            await dataWriter.StoreAsync(); // 

                            dataWriter.DetachStream();
                        }
                        // write data on the empty file:
                        await outputStream.FlushAsync();

                    }
                    await fileStream.FlushAsync();

                }
               // await file.CopyAsync(folder, "tempFile.jpg", NameCollisionOption.ReplaceExisting);
            }
            catch (Exception ex)
            {
                Reporting.DisplayMessageDebugExemption(ex);
            }
            return file;

        }

        //All about protecting data

        public static async Task<StorageFile> EncryptStorageFileLocalUserAsync(this StorageFile FileForEncryption)
        {
            //"LOCAL = user"
            IBuffer data = await FileIO.ReadBufferAsync(FileForEncryption);
            IBuffer SecuredData = await DataProtectionStream("LOCAL = user", data);
            var  EncryptedFile = await ApplicationData.Current.TemporaryFolder.CreateFileAsync(
              "EncryptedFile" + FileForEncryption.FileType, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteBufferAsync(EncryptedFile, SecuredData);
            return EncryptedFile;
           // Reporting.DisplayMessage( "File encryption successfull. File stored at " + EncryptedFile.Path + "\n\n");

        }
        public static async Task<StorageFile> DecryptStorageFileLocalUserAsync(this StorageFile EncryptedFile)
        {

            IBuffer data = await FileIO.ReadBufferAsync(EncryptedFile);
            IBuffer UnSecuredData = await DataUnprotectStream(data);

             var DecryptedFile = await ApplicationData.Current.TemporaryFolder.CreateFileAsync(
              "DecryptedFile" + EncryptedFile.FileType, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteBufferAsync(DecryptedFile, UnSecuredData);
           // Reporting.DisplayMessage("File decryption successfull. File stored at " + DecryptedFile.Path + "\n\n");
            return DecryptedFile;

        }
       
        /// <summary>
        /// All about WritableBitmap
        /// </summary>
        /// <param name="writeableBitmap"></param>
        /// <param name="storageFile"></param>
        /// <returns></returns>
        public static async Task SaveWriteableBitmapToStorageFileAsync(this WriteableBitmap writeableBitmap, IStorageFile destinationEmptyStorageFile)
        {
            using (var writestream = await destinationEmptyStorageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, writestream);

                using (var pixelStream = writeableBitmap.PixelBuffer.AsStream())
                {
                    var pixels = new byte[pixelStream.Length];
                    await pixelStream.ReadAsync(pixels, 0, pixels.Length);

                    encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
                        (uint)writeableBitmap.PixelWidth, (uint)writeableBitmap.PixelHeight, 96.0, 96.0, pixels);

                    await encoder.FlushAsync();
                }
            }
        }
       
        /// <summary>
        /// Convert WriteableBitmap to StorageFile.
        /// </summary>
        /// <param name="writeableBitmap"></param>
        /// <returns></returns>
        public static async Task<StorageFile> AsStorageFileAsync(this WriteableBitmap writeableBitmap, string ReplaceLocalFileNameWithExtension)
        {
            var destinationEmptyStorageFile =
                   await
                       ApplicationData.Current.LocalFolder.CreateFileAsync(ReplaceLocalFileNameWithExtension,
                           CreationCollisionOption.ReplaceExisting);
            using (var writestream = await destinationEmptyStorageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, writestream);

                using (var pixelStream = writeableBitmap.PixelBuffer.AsStream())
                {
                    var pixels = new byte[pixelStream.Length];
                    await pixelStream.ReadAsync(pixels, 0, pixels.Length);

                    encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
                        (uint)writeableBitmap.PixelWidth, (uint)writeableBitmap.PixelHeight, 96.0, 96.0, pixels);

                    await encoder.FlushAsync();
                }
            }
            return destinationEmptyStorageFile;
        }
        #region DataProtect
        private static async Task<IBuffer> DataProtectionStream(String descriptor, IBuffer buffMsg)
        {
            
            // Create a DataProtectionProvider object for the specified descriptor.
            DataProtectionProvider Provider = new DataProtectionProvider(descriptor);
           
            // Create a random access stream to contain the plaintext message.
            InMemoryRandomAccessStream inputData = new InMemoryRandomAccessStream();

            // Create a random access stream to contain the encrypted message.
            InMemoryRandomAccessStream protectedData = new InMemoryRandomAccessStream();

            // Retrieve an IOutputStream object and fill it with the input (plaintext) data.
            IOutputStream outputStream = inputData.GetOutputStreamAt(0);
            DataWriter writer = new DataWriter(outputStream);
            writer.WriteBuffer(buffMsg);
            await writer.StoreAsync();
            await outputStream.FlushAsync();

            // Retrieve an IInputStream object from which you can read the input data.
            IInputStream source = inputData.GetInputStreamAt(0);

            // Retrieve an IOutputStream object and fill it with encrypted data.
            IOutputStream dest = protectedData.GetOutputStreamAt(0);
            await Provider.ProtectStreamAsync(source, dest);
            await dest.FlushAsync();

            //Verify that the protected data does not match the original
            DataReader reader1 = new DataReader(inputData.GetInputStreamAt(0));
            DataReader reader2 = new DataReader(protectedData.GetInputStreamAt(0));
            await reader1.LoadAsync((uint)inputData.Size);
            await reader2.LoadAsync((uint)protectedData.Size);
            IBuffer buffOriginalData = reader1.ReadBuffer((uint)inputData.Size);
            IBuffer buffProtectedData = reader2.ReadBuffer((uint)protectedData.Size);

            if (CryptographicBuffer.Compare(buffOriginalData, buffProtectedData))
            {
                throw new Exception("ProtectStreamAsync returned unprotected data");
            }

            // Return the encrypted data.
            return buffProtectedData;
        }

        public static async Task<IBuffer> DataUnprotectStream(IBuffer buffProtected)
        {
            // Create a DataProtectionProvider object.
            DataProtectionProvider Provider = new DataProtectionProvider();

            // Create a random access stream to contain the encrypted message.
            InMemoryRandomAccessStream inputData = new InMemoryRandomAccessStream();

            // Create a random access stream to contain the decrypted data.
            InMemoryRandomAccessStream unprotectedData = new InMemoryRandomAccessStream();

            // Retrieve an IOutputStream object and fill it with the input (encrypted) data.
            IOutputStream outputStream = inputData.GetOutputStreamAt(0);
            DataWriter writer = new DataWriter(outputStream);
            writer.WriteBuffer(buffProtected);
            await writer.StoreAsync();
            await outputStream.FlushAsync();

            // Retrieve an IInputStream object from which you can read the input (encrypted) data.
            IInputStream source = inputData.GetInputStreamAt(0);

            // Retrieve an IOutputStream object and fill it with decrypted data.
            IOutputStream dest = unprotectedData.GetOutputStreamAt(0);
            await Provider.UnprotectStreamAsync(source, dest);
            await dest.FlushAsync();

            // Write the decrypted data to an IBuffer object.
            DataReader reader2 = new DataReader(unprotectedData.GetInputStreamAt(0));
            await reader2.LoadAsync((uint)unprotectedData.Size);
            IBuffer buffUnprotectedData = reader2.ReadBuffer((uint)unprotectedData.Size);

            // Return the decrypted data.
            return buffUnprotectedData;
        }
        #endregion
    }
}
