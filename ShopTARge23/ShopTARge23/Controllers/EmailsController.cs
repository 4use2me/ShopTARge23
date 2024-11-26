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

            return RedirectToAction(nameof(Index));
        }
    }
}
