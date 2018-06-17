using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;

namespace HelperUWP.Common
{
    public class ToastManager
    {
       
        public ToastManager()
        {
            
        }
        public static void PopToastWithSelectionBoxSnoose(string AdaptiveTitle, string Adaptive2, string Adaptive3)
        {
            // Generate the toast notification content and pop the toast
            ToastContent content = GenerateToastContentSelectionBoxSnoose(AdaptiveTitle, Adaptive2, Adaptive3);
            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml()));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AdaptiveTitle"></param>
        /// <param name="Adaptive2"></param>
        /// <param name="Adaptive3"></param>
        public static void PopToastAlarm(string AdaptiveTitle, string Adaptive2, string Adaptive3, bool IsLooping)
        {
            // Generate the toast notification content and pop the toast
            ToastContent content = GenerateToastAlarm(AdaptiveTitle, Adaptive2, Adaptive3, IsLooping);
            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml()));
        }
        public static void PopToastSimpleAutoHide(string AdaptiveTitle, string Adaptive2, string Adaptive3)
        {
            // Generate the toast notification content and pop the toast
            ToastContent content = GenerateToastContentSimpleAutoHide(AdaptiveTitle, Adaptive2, Adaptive3);
            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml()));
        }
        public static void PopToastSimpleAutoHide(string AdaptiveTitle, string Description)
        {
            // Generate the toast notification content and pop the toast
            ToastContent content = GenerateToastContentSimpleAutoHide(AdaptiveTitle, Description, "");
            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml()));
        }


        #region All Method Group




        internal static ToastContent GenerateToastContentSelectionBoxSnoose(string AdaptiveTitle, string Adaptive2, string Adaptive3)
        {
            return new ToastContent()
            {
                ActivationType = ToastActivationType.Background,
                Launch = "action=viewEvent&eventId=1983",
                Scenario = ToastScenario.Reminder,

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                               Text = AdaptiveTitle
                            },

                            new AdaptiveText()
                            {
                                Text = Adaptive2
                            },

                            new AdaptiveText()
                            {
                               Text = Adaptive3
                            }
                        }
                    }
                },

                Actions = new ToastActionsCustom()
                {
                    Inputs =
                {
                new ToastSelectionBox("snoozeTime")
                {
                    DefaultSelectionBoxItemId = "1",
                    Items =
                    {
                        new ToastSelectionBoxItem("1", "1 minute"),
                        new ToastSelectionBoxItem("15", "15 minutes"),
                        new ToastSelectionBoxItem("60", "1 hour"),
                        new ToastSelectionBoxItem("240", "4 hours"),
                        new ToastSelectionBoxItem("1440", "1 day")
                    }
                }
            },

                    Buttons =
            {
                new ToastButtonSnooze()
                {
                    SelectionBoxId = "snoozeTime"
                },

                new ToastButtonDismiss()
            }
                }
            };
        }
        internal static ToastContent GenerateToastAlarm(string AdaptiveTitle, string Adaptive2, 
            string Adaptive3, bool IsLooping)
        {

            return new ToastContent()
            {

                Launch = "action=viewEvent&eventId=1983",
                Scenario = ToastScenario.Alarm,
                Duration = ToastDuration.Short,
                Audio = new ToastAudio() { Loop = IsLooping},   
                ActivationType = ToastActivationType.Background,        
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                               Text = AdaptiveTitle
                            },

                            new AdaptiveText()
                            {
                                Text = Adaptive2
                            },

                            new AdaptiveText()
                            {
                               Text = Adaptive3
                            }
                        }
                    }
                },

                Actions = new ToastActionsCustom()
                {
                    Inputs =
                {
                new ToastSelectionBox("snoozeTime")
                {
                    DefaultSelectionBoxItemId = "5",
                    Items =
                    {                       
                        new ToastSelectionBoxItem("5", "5 minutes"),
                        new ToastSelectionBoxItem("10", "10 minutes"),
                        new ToastSelectionBoxItem("20", "20 minutes"),
                        new ToastSelectionBoxItem("30", "30 minutes"),
                        new ToastSelectionBoxItem("60", "1 hour"),                      
                    }
                }


            },

                    Buttons =
            {
                new ToastButtonSnooze()
                {
                    SelectionBoxId = "snoozeTime"
                },

                new ToastButtonDismiss()
            }
                }
            };
        }
        internal static ToastContent GenerateToastContentSimpleAutoHide(string AdaptiveTitle, string Adaptive2, string Adaptive3)
        {
            // Start by constructing the visual portion of the toast
            ToastBindingGeneric binding = new ToastBindingGeneric();      
            // We'll always have this summary text on our toast notification
            // (it is required that your toast starts with a text element)
            binding.Children.Add(new AdaptiveText()
            {
                Text = AdaptiveTitle
            });

            // We'll just add two simple lines of text
            binding.Children.Add(new AdaptiveText()
            {
                Text = Adaptive2
            });

            binding.Children.Add(new AdaptiveText()
            {
                Text = Adaptive3
            });

            // Construct the entire notification
            return new ToastContent()
            {
                ActivationType = ToastActivationType.Foreground,
                Visual = new ToastVisual()
                {
                    // Use our binding from above
                    BindingGeneric = binding,
                },
                // Include launch string so we know what to open when user clicks toast
                Launch = "action=viewForecast&zip=98008"
                //The argument is = action=viewForecast&zip=98008
            };
        }
        #endregion
       
        
    }
}
