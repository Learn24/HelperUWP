using LightBuzz.SMTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Email;
using Windows.Foundation;

namespace HelperUWP.Common
{
    public class EmailSender
    {
        private static async Task SendAutoEmailAsync(string emailSenderAddress, string emailSenderPassword, string emailAddressTo, string message, string subject = null)
        {           
            if (!DeviceInfo.IsNetworkAvailable)
                return;
            if (emailSenderAddress.ToLower().EndsWith("@gmail.com"))
            {
                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 465, true, emailSenderAddress, emailSenderPassword))
                {
                    EmailMessage emailMessage = new EmailMessage();
                    emailMessage.To.Add(new EmailRecipient(emailAddressTo));
                    if (string.IsNullOrEmpty(subject))
                    {
                        emailMessage.Subject = Reporting.AppName;
                    }
                    else
                    {
                        emailMessage.Subject = subject;
                    }
                    emailMessage.Body = message + "\n" + DateTime.Now.ToLocalTime().ToString();

                    await client.SendMailAsync(emailMessage);
                }
            }
            else if (emailSenderAddress.ToLower().EndsWith("outlook.com") || emailSenderAddress.ToLower().EndsWith("@hotmail.com"))
            {
                using (SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587, false, emailSenderAddress, emailSenderPassword))
                {
                    EmailMessage emailMessage = new EmailMessage();
                    emailMessage.To.Add(new EmailRecipient(emailAddressTo));                  
                    if (string.IsNullOrEmpty(subject))
                    {
                        emailMessage.Subject = Reporting.AppName;
                    }
                    else
                    {
                        emailMessage.Subject = subject;
                    }
                    emailMessage.Body = message + "\n" + DateTime.Now.ToLocalTime().ToString();

                    await client.SendMailAsync(emailMessage);
                }
            }
            else
            {
                throw new InvalidEmailException("Invalid login email, use gmail or outlook only");
            }
        }
        public static  IAsyncAction SendAutoEmailIAsync(string emailSenderAddress, string emailSenderPassword, string emailAddressTo, string message, string subject = null)
        {
             return  SendAutoEmailAsync(emailSenderAddress, emailSenderPassword, emailAddressTo, message, subject).AsAsyncAction();
        }
    }
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message) : base(message)
        {

        }
    }
}
