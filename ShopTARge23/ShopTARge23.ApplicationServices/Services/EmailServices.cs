using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using MailKit.Security;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

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
			email.Body = new TextPart(TextFormat.Html) { Text = dto.Body };

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
