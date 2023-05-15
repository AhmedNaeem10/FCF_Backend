using MimeKit;
using System.Net.Mail;
using FCF.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit;
using FCF.Models.Requests.UserDtos;

namespace FCF.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly MailKit.Net.Smtp.SmtpClient smtp;
        public EmailService()
        {
            smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);

            smtp.Authenticate("gamecon5575@gmail.com", "kadqodmsxzdazxhg");
        }

        public void sendEmail(UserDto user, string subject, string body)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Gamecon", "gamecon5575@gmail.com"));
            email.To.Add(new MailboxAddress(user.Name, user.Email));

            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = body
            };
            smtp.Send(email);
        }

        public void dispose()
        {
            smtp.Disconnect(true);
        }
    }
}
