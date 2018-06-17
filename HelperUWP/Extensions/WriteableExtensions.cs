using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace HelperUWP.Extensions
{
    public static class WriteableExtensions
    {
       
        public async static Task<WriteableBitmap> ToWriteableBitmapAsync(string imageUrl, string ReplaceLocalFileNameWithExtensione)
        {
            StorageFile storageFile = null;
            WriteableBitmap bmp = new WriteableBitmap(1, 1);
            HttpClient client = new HttpClient();

            using (var response = await HttpWebRequest.CreateHttp(imageUrl).GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    StorageFile file = null;
                    StorageFolder mainFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                    file = await ApplicationData.Current.LocalFolder.CreateFileAsync(ReplaceLocalFileNameWithExtensione, CreationCollisionOption.ReplaceExisting);

                    using (var filestream = await file.OpenStreamForWriteAsync())
                    {
                        await stream.CopyToAsync(filestream);
                        storageFile = await mainFolder.GetFileAsync(file.Name);
                    }
                }
            }
            await bmp.SetSourceAsync(await storageFile.OpenAsync(FileAccessMode.Read));
            return bmp;
        }

    }
}
