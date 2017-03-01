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
                    @"<p style=""text-align:center;""><img align=""middle"" src =""cid:{0}"" 
                    alt=""Logo"" /></p><p> Hello,</p><p>Thank you for subscribing to Ranaitfleur. We will keep you up 
                    to date with Ranaitfleur news and product updates.</p><p>If you wish to unsubscribe, please go to 
                    our website and use the ""Unsubscribe"" option.</p><p>&nbsp;</p><p>Warm wishes,
                    </p><p>Ranaitfleur</p><p><a href = ""http://www.ranaitfleur.com""> www.ranaitfleur.com </a></p>",
                    image.ContentId);

            AddFooter(builder, webrootPath);

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

        private static void AddFooter(BodyBuilder builder, string webrootPath)
        {
            var image = builder.LinkedResources.Add($"{webrootPath}\\img\\SmallLogo.png");
            image.ContentId = MimeUtils.GenerateMessageId();

            builder.HtmlBody += string.Format(@"<p>&nbsp;</p>
            <div style=""text-align:center;"">
                <p><img class=""logo"" src=""cid:{0}"" alt=""LOGO"" /></p>
                <div><strong>Find more about us</strong>
                    <div><a href = ""http://www.ranaitfleur.com/App/Contact"">Contact us</a></div>
                    <div><a href = ""http://www.ranaitfleur.com/Auth/Unsubscribe"">Email Unsubscribe</a></div>
                </div>
                <div><strong>About us</strong>
                    <div><a href = ""http://www.ranaitfleur.com/App/OurEthics"">Our Ethics</a></div>
                    <div><a href = ""http://www.ranaitfleur.com/App/TermsConditions"">Terms &amp; Conditions</a></div>
                    <div><a href = ""http://www.ranaitfleur.com/App/PrivacyPolicy"">Privacy and Cookie Policy</a></div>
                </div>
                <div><strong>Find us on</strong>
                    <div><a href = ""https://www.facebook.com/ruanefleur/"">Facebook</a></div>
                    <div><a href = ""https://twitter.com/rfleurtwit"">Twitter</a></div>
                    <div><a href = ""https://vine.co/u/1337063062477447168"">Vine</a></div>
                    <div><a href = ""https://foursquare.com/user/175788810"">Foursquare</a></div>
                </div>
            </div>", image.ContentId);
        }
    }
}
