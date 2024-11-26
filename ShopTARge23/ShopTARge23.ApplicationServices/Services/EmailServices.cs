using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace ShopTARge23.ApplicationServices.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _config;

        //konstruktor
        public EmailServices(IConfiguration config)
        {
            _config = config;
        }
        public void SendEmail(EmailDto dto)
        {
            var email = new MimeMessage();

            //otsida üles config asukoht (s.o appsettings.json) ja sinna sisestada muutujad
            //"EmailHost": "smtp.gmail.com",
            //"EmailUserName": "Sinu kasutajanimi"
            //"EmailPassword": "Sinu parool"
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(dto.To));
            email.Subject = dto.Subject;
            
            // Loome e-kirja sisu
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = dto.Body
            };

            // Lisame manused
            if (dto.Attachments != null && dto.Attachments.Any())
            {
                foreach (var attachment in dto.Attachments)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        attachment.Content.CopyTo(memoryStream); // Kopeerime sisu voost
                        bodyBuilder.Attachments.Add(attachment.FileName, memoryStream.ToArray(), ContentType.Parse(attachment.ContentType));
                    }
                }
            }

            email.Body = bodyBuilder.ToMessageBody();

            //kindlasti kasutada mailkit.net.smtp
            using var smtp = new SmtpClient();

            //valida õige port ja kasutada securesocketoptioni
            //(SecureSocketOptions.StartTls: 587 v SecureSocketOptions.SslOnConnect: 465)
            smtp.Connect(_config.GetSection("EmailHost").Value,
                587, SecureSocketOptions.StartTls);

            //autentida
            smtp.Authenticate(
                _config.GetSection("EmailUserName").Value,
                _config.GetSection("EmailPassword").Value);
            //saada email
            smtp.Send(email);
            //vabasta ressurss
            smtp.Disconnect(true);
        }
    }
}