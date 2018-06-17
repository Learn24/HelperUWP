using AppStudio.DataProviders.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

namespace HelperUWP.Common
{
    public class ShareManager
    {
        private static StorageFile file = null;
        private static string TitleText = "Title";
        private static string DescriptionText = "Message";
        private static string WebLinkText = string.Empty;
        private static string ImageLinkSource = string.Empty;



        public static void ShareContent(RequestModel requestModel, bool HtmlSupport = false)
        {
            //string whatever = "\" "; 
            DataTransferManager transferManager = DataTransferManager.GetForCurrentView();
            transferManager.DataRequested += (s, e) =>
            {
                if (requestModel != null)
                {
                    if (!string.IsNullOrEmpty(requestModel.Title))
                    {
                        e.Request.Data.Properties.Title = requestModel.Title;
                    }
                    if (!string.IsNullOrEmpty(requestModel.Description))
                    {
                        e.Request.Data.Properties.Description = requestModel.Description;
                    }
                    if (HtmlSupport)
                    {                       
                        string html = @"<body><h2>"+requestModel.Header+"</h2><img src="+"\""+requestModel.ImageUrl+"\""+" " + "alt="+"\""+requestModel.ImageUrl+"\""+ " "+ /*"width="104" height="142"*/"><p>"+requestModel.Paragraph+"</p><a href="+"\""+requestModel.WebLink+"\""+">"+requestModel.WebLinkHeader+"</a></body>";
                        e.Request.Data.SetHtmlFormat(
                        HtmlFormatHelper.CreateHtmlFormat(html));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(requestModel.ImageUrl))
                        {
                            e.Request.Data.SetBitmap(Windows.Storage.Streams.RandomAccessStreamReference.CreateFromUri(new Uri(requestModel.ImageUrl)));
                        }
                        if (!string.IsNullOrEmpty(requestModel.Paragraph))
                        {
                            e.Request.Data.SetText(requestModel.Paragraph);
                        }
                        
                        if (requestModel.Files.Count() > 0)
                        {
                            List<IStorageItem> imageItems = new List<IStorageItem>();

                            foreach(var file in requestModel.Files)
                            {
                                imageItems.Add(file);
                                e.Request.Data.SetStorageItems(imageItems);
                                RandomAccessStreamReference imageStreamRef = RandomAccessStreamReference.CreateFromFile(file);
                                e.Request.Data.Properties.Thumbnail = imageStreamRef;
                                e.Request.Data.SetBitmap(imageStreamRef);
                            }

                           

                        }
                        if (!string.IsNullOrEmpty(requestModel.WebLink))
                        {
                            e.Request.Data.SetWebLink(new Uri(requestModel.WebLink));
                        }
                    }

                }
            };
            DataTransferManager.ShowShareUI();
        }
    }
      
    public class RequestModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string Header { get; set; }
        public string Paragraph { get; set; }
        public string WebLink { get; set; }
        public string WebLinkHeader { get; set; }
        public string ImageUrl { get; set; }
        public List<StorageFile> Files { get; set; } = new List<StorageFile>();
        
    }
}
