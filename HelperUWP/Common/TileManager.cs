using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;

namespace HelperUWP.Common
{
    public class TileManager
    {
        public static void UpdateTileText(string text,  int expirationTimeSeconds)        {
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text04);
            XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");
            tileTextAttributes[0].InnerText = text;
            XmlDocument squareTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text04);
            XmlNodeList squareTileTextAttributes = squareTileXml.GetElementsByTagName("text");
            squareTileTextAttributes[0].AppendChild(squareTileXml.CreateTextNode(text));
            IXmlNode node = tileXml.ImportNode(squareTileXml.GetElementsByTagName("binding").Item(0), true);
            tileXml.GetElementsByTagName("visual").Item(0).AppendChild(node);

            TileNotification tileNotification = new TileNotification(tileXml);
            tileNotification.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(expirationTimeSeconds);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);        }
        public static void UpdateTileText(string text)        {
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text04);
            XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");
            tileTextAttributes[0].InnerText = text;
            XmlDocument squareTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text04);
            XmlNodeList squareTileTextAttributes = squareTileXml.GetElementsByTagName("text");
            squareTileTextAttributes[0].AppendChild(squareTileXml.CreateTextNode(text));
            IXmlNode node = tileXml.ImportNode(squareTileXml.GetElementsByTagName("binding").Item(0), true);
            tileXml.GetElementsByTagName("visual").Item(0).AppendChild(node);

            TileNotification tileNotification = new TileNotification(tileXml);            
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);        }
        public static void UpdateTileComplete(string title, string description, bool IsTileClear = false)
        {
            if (IsTileClear)
                Clear();
           var content = GenerateTile(title, description);
           TileUpdateManager.CreateTileUpdaterForApplication().Update(new TileNotification(content.GetXml()));
        }
        /// <summary>
        /// With title and discription
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        private static TileContent GenerateTile(string title, string description)
        {

            return new TileContent()
            {
                Visual = new TileVisual()
                {
                    TileSmall = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            Children =
                            {
                               new AdaptiveText()
                               {
                                  Text = title,
                                  HintStyle = AdaptiveTextStyle.Subtitle
                               },



                               new AdaptiveText()
                               {
                                  Text = description,
                                  HintStyle = AdaptiveTextStyle.CaptionSubtle,
                                  HintWrap = true,
                                  HintMaxLines = 10,
                               }
                            }
                        }
                    },
                    TileMedium = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            Children =
                            {
                               new AdaptiveText()
                               {
                                  Text = title,
                                  HintStyle = AdaptiveTextStyle.Subtitle
                               },



                               new AdaptiveText()
                               {
                                  Text = description,
                                  HintStyle = AdaptiveTextStyle.CaptionSubtle,
                                  HintWrap = true,
                                  HintMaxLines = 10,
                               }
                            }
                        }
                    },
                    TileWide = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            Children =
                            {
                               new AdaptiveText()
                               {
                                  Text = title,
                                  HintStyle = AdaptiveTextStyle.Subtitle
                               },



                               new AdaptiveText()
                               {
                                  Text = description,
                                  HintStyle = AdaptiveTextStyle.CaptionSubtle,
                                  HintWrap = true,
                                  HintMaxLines = 10,
                               }
                            }
                        }
                    },
                    TileLarge = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            Children =
                            {
                               new AdaptiveText()
                               {
                                  Text = title,
                                  HintStyle = AdaptiveTextStyle.Subtitle
                               },



                               new AdaptiveText()
                               {
                                  Text = description,
                                  HintStyle = AdaptiveTextStyle.CaptionSubtle,
                                  HintWrap = true,
                                  HintMaxLines = 10,
                               }
                            }
                        }
                    },
                }

                        
            };

        }
        //private 
        /// <summary>
        /// Picture sample source ms-appx:///Images/Icon.png".
        /// </summary>
        /// <param name="text"></param>
        /// <param name="expirationTimeSeconds"></param>
        /// <param name="pictureSource"></param>
        public static void UpdateTileImageAndText(string text, int expirationTimeSeconds, string pictureSource)        {
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150ImageAndText01);
            XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");
            tileTextAttributes[0].InnerText = text;
            XmlNodeList tileImageAttributes = tileXml.GetElementsByTagName("image");
            ((XmlElement)tileImageAttributes[0]).SetAttribute("src", pictureSource);
            ((XmlElement)tileImageAttributes[0]).SetAttribute("alt", "red graphic");

            XmlDocument squareTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150PeekImageAndText04);
            XmlNodeList squareTileTextAttributes = squareTileXml.GetElementsByTagName("text");
            XmlNodeList squareImageAttribute = squareTileXml.GetElementsByTagName("image");
            ((XmlElement)squareImageAttribute[0]).SetAttribute("src", pictureSource);
            ((XmlElement)squareImageAttribute[0]).SetAttribute("alt", "red graphic");
            squareTileTextAttributes[0].AppendChild(squareTileXml.CreateTextNode(text));
            IXmlNode node = tileXml.ImportNode(squareTileXml.GetElementsByTagName("binding").Item(0), true);
            tileXml.GetElementsByTagName("visual").Item(0).AppendChild(node);

            TileNotification tileNotification = new TileNotification(tileXml);
            tileNotification.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(100);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);        }
        public static void UpdateTileImageAndText(string text, string pictureSource)        {
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150ImageAndText01);
            XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");
            tileTextAttributes[0].InnerText = text;
            XmlNodeList tileImageAttributes = tileXml.GetElementsByTagName("image");
            ((XmlElement)tileImageAttributes[0]).SetAttribute("src", pictureSource);
            ((XmlElement)tileImageAttributes[0]).SetAttribute("alt", "red graphic");

            XmlDocument squareTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150PeekImageAndText04);
            XmlNodeList squareTileTextAttributes = squareTileXml.GetElementsByTagName("text");
            XmlNodeList squareImageAttribute = squareTileXml.GetElementsByTagName("image");
            ((XmlElement)squareImageAttribute[0]).SetAttribute("src", pictureSource);
            ((XmlElement)squareImageAttribute[0]).SetAttribute("alt", "red graphic");
            squareTileTextAttributes[0].AppendChild(squareTileXml.CreateTextNode(text));
            IXmlNode node = tileXml.ImportNode(squareTileXml.GetElementsByTagName("binding").Item(0), true);
            tileXml.GetElementsByTagName("visual").Item(0).AppendChild(node);

            TileNotification tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);        }
        public static void Clear()
        {
            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
        }
    }
}
