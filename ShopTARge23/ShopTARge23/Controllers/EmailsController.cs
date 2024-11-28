using Microsoft.AspNetCore.Mvc;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Models.Emails;

namespace ShopTARge23.Controllers
{
    public class EmailsController : Controller
    {
        private readonly IEmailServices _emailServices;
        public EmailsController(IEmailServices emailServices)
        {
            _emailServices = emailServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        //meetod teha
        [HttpPost]
        public IActionResult SendEmail(EmailViewModel vm)
        {
            // Kontrollime e-posti formaati enne saatmist
            if (!IsValidEmail(vm.To))
            {
                TempData["Message"] = "Invalid email address. Please check and try again.";
                return RedirectToAction(nameof(Index));
            }

            var attachments = new List<EmailAttachmentDto>();

            if (vm.Attachments != null && vm.Attachments.Any())
            {
                attachments = vm.Attachments.Select(file => new EmailAttachmentDto
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    Content = file.OpenReadStream()
                }).ToList();
            }

            var dto = new EmailDto()
            {
                To = vm.To,
                Subject = vm.Subject,
                Body = vm.Body,
                Attachments = attachments
            };

            _emailServices.SendEmail(dto);

            TempData["Message"] = "The email was sent successfully.";

            return RedirectToAction(nameof(Index));
        }
        // E-posti aadressi valideerimise meetod
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                // Kontrollime lisaks, et aadress sisaldab vähemalt üht punkti peale '@'
                var parts = email.Split('@');
                return addr.Address == email && parts.Length == 2 && parts[1].Contains('.');
            }
            catch
            {
                return false;
            }
        }
    }
}