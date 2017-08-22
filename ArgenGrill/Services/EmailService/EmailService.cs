using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace ArgenGrill.Services.EmailService
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage iMessage)

        {
            var client = new SendGridClient("YOUR SENDGRID API KEY"); // https://app.sendgrid.com

            var msg = new SendGridMessage()

            {
                From = new EmailAddress("Tier@Outlook.com", "Tier One"),

                Subject = iMessage.Subject,

                PlainTextContent = iMessage.Body,

                HtmlContent = "<strong>" + iMessage.Body + "</strong>"
            };

            msg.AddTo(new EmailAddress(iMessage.Destination));

            var response = await client.SendEmailAsync(msg);
        }
    }
}