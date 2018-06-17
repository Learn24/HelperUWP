using HelperUWP.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HelperUWP.Controls.UI
{
    public sealed partial class AdaptiveImageControl : UserControl
    {
        public AdaptiveImageControl()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;
            Loaded += AdaptiveImageControl_Loaded;
        }
        private int DesiredTotalItem = 3;
        private int DesiredTotalItemSix = 6;
        private int SplitViewCompactPaneLength = 15;
        private int ItemMargin = 6;
        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            AdaptiveTrigger adapt = new AdaptiveTrigger();
            var totalSize = e.Size.Width;

            if (totalSize >= 0 && totalSize < 400)
            {
                var size = (totalSize / DesiredTotalItem - ItemMargin);              
                GridCons.Width = size;
                GridCons.Height = size;

            }
            else if (totalSize >= 400 && totalSize < 480)
            {
                var size = (totalSize / DesiredTotalItem - ItemMargin);              
                GridCons.Width = size;
                GridCons.Height = size;
            }
            else if (totalSize >= 480 && totalSize < 500)
            {
                var size = (totalSize / DesiredTotalItem - ItemMargin);                
                GridCons.Width = size;
                GridCons.Height = size;
              
            }
            else if (totalSize >= 500 && totalSize < 550)
            {
                var size = (totalSize / DesiredTotalItem - ItemMargin);               
                GridCons.Width = size;
                GridCons.Height = size;
               
            }
            else if (totalSize >= 550 && totalSize < 600)
            {
                var size = (totalSize / DesiredTotalItem - ItemMargin);
                GridCons.Width = size;
                GridCons.Height = size;
             
            }
            else if (totalSize >= 600 && totalSize < 650)
            {
                var size = (totalSize / DesiredTotalItem - ItemMargin);
                GridCons.Width = size;
                GridCons.Height = size;

            }
            else if (totalSize >= 650 && totalSize < 700)
            {
                var size = (totalSize / DesiredTotalItem - ItemMargin);
                GridCons.Width = size;
                GridCons.Height = size ;

            }
            else if (totalSize >= 700/* && totalSize < 700*/)
            {
                var size = (totalSize / DesiredTotalItemSix - ItemMargin) - SplitViewCompactPaneLength;
                GridCons.Width = size;
                GridCons.Height = size;
               
            }

        }


        private void AdaptiveImageControl_Loaded(object sender, RoutedEventArgs e)
        {
            //When control is loaded
            var totalSize = Window.Current.Bounds.Width;
            if (DeviceInfo.IsMobile)
            {
                var size = (totalSize / DesiredTotalItem - ItemMargin);               
                GridCons.Width = size;
                GridCons.Height = size;
              
            }
            else
            {
                if (totalSize >= 700/* && totalSize < 700*/)
                {
                    var size = (totalSize / DesiredTotalItemSix - ItemMargin) - SplitViewCompactPaneLength;
                    GridCons.Width = size;
                    GridCons.Height = size;
                   
                }
                else
                {
                    var size = (totalSize / DesiredTotalItem - ItemMargin);
                    GridCons.Width = size;
                    GridCons.Height = size;
                  
                }
            }




        }

        public static readonly DependencyProperty SetImageSourceProperty = DependencyProperty.Register("SetImageSource", typeof(ImageSource),
            typeof(AdaptiveImageControl), new PropertyMetadata(null, new PropertyChangedCallback(OnSetImageSourceChanged)));

        public ImageSource SetImageSource
        {
            get { return (ImageSource)GetValue(SetImageSourceProperty); }
            set { SetValue(SetImageSourceProperty, value); }
        }
        private static void OnSetImageSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AdaptiveImageControl borderControl = d as AdaptiveImageControl;
            borderControl.OnSetImageSourceChanged(e);
        }
        private void OnSetImageSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            var source = (ImageSource)e.NewValue;
            var bmp = source as BitmapImage;
            bmp.DecodePixelHeight = Convert.ToInt32(GridCons.Height);
            bmp.DecodePixelWidth = Convert.ToInt32(GridCons.Width);
            MyImage.Source = (ImageSource)e.NewValue;
        }
        //SetDesiredCount;
        public static readonly DependencyProperty SetDesiredCountProperty = DependencyProperty.Register("SetDesiredCount", typeof(int),
           typeof(AdaptiveImageControl), new PropertyMetadata(3, new PropertyChangedCallback(OnSetDesiredCountChanged)));

        public int SetDesiredCount
        {
            get { return (int)GetValue(SetImageSourceProperty); }
            set { SetValue(SetImageSourceProperty, value); }
        }
        private static void OnSetDesiredCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AdaptiveImageControl borderControl = d as AdaptiveImageControl;
            borderControl.OnSetDesiredCountChanged(e);
        }
        private void OnSetDesiredCountChanged(DependencyPropertyChangedEventArgs e)
        {

            TotalItem.Text = (string)e.NewValue;
            DesiredTotalItem = (int)e.NewValue;
        }
    }
}
