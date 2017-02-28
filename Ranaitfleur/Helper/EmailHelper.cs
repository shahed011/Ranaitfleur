using System;
using MimeKit;
using MimeKit.Utils;

namespace Ranaitfleur.Helper
{
    public static class EmailHelper
    {
        public static BodyBuilder GetSubscribeEmailBody(string webrootPath)
        {
            // Set the plain-text version of the message text
            var builder = new BodyBuilder {TextBody = @"Hello,\n\n
Thank you for subscribing to Ranaitfleur. We will keep you up to date with Ranaitfleur news and product updates.\n\n
If you wish to unsubscribe, please go to our website and use the ""Unsubscribe"" option.\n\n\n\n\n\n
Warm wishes,\n\n
Ranaitfleur\n\n
www.ranaitfleur.com"};

            // In order to reference selfie.jpg from the html text, we'll need to add it
            // to builder.LinkedResources and then use its Content-Id value in the img src.
            var image = builder.LinkedResources.Add($"{webrootPath}\\img\\GOLD_LOGO.jpg");
            image.ContentId = MimeUtils.GenerateMessageId();

            // Set the html version of the message text
            builder.HtmlBody =
                string.Format(
                    @"<p><img style=""display: block; margin - left: auto; margin - right: auto;"" src=""cid:{0}"" 
                    alt=""Logo"" /></p><p> Hello,</p><p>Thank you for subscribing to Ranaitfleur.We will keep you up 
                    to date with Ranaitfleur news and product updates.</p><p>If you wish to unsubscribe, please go to 
                    our website and use the ""Unsubscribe"" option.</p><p>&nbsp;</p><p>Warm wishes,
                    </p><p>Ranaitfleur</p><p><a href = ""http://www.ranaitfleur.com""> www.ranaitfleur.com </a></p>",
                    image.ContentId);

            // Now we just need to set the message body and we're done
            return builder;
        }

        public static BodyBuilder GetContactEmailBody(string senderName, string senderEmail, string messageBody, string webrootPath)
        {
            var builder = new BodyBuilder
            {
                TextBody = $@"From {senderName}{Environment.NewLine}{senderEmail},{Environment.NewLine}
                        {Environment.NewLine}{messageBody}"
            };

            //var image = builder.LinkedResources.Add($"{webrootPath}\\img\\GOLD_LOGO.jpg");
            //image.ContentId = MimeUtils.GenerateMessageId();

            //builder.HtmlBody =
            //    string.Format(
            //        $@"<p><img style=""display: block; margin - left: auto; margin - right: auto;"" src=""cid:{0}"" 
            //        alt=""Logo"" /></p><p>{senderEmail}</p><p>{messageBody}</p>",
            //        image.ContentId);

            return builder;
        }
    }
}
