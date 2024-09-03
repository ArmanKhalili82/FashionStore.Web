using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SmtpClient("smtp.your-email-provider.com", 587)
        {
            Credentials = new NetworkCredential("your-email@example.com", "your-password"),
            EnableSsl = true
        };

        return client.SendMailAsync(
            new MailMessage(from: "your-email@example.com", to: email, subject, body: message)
            {
                IsBodyHtml = true
            });
    }
}
