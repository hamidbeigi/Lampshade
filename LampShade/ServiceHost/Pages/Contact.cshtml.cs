using _0_Framework.Application.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ServiceHost.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;


        public ContactModel(IEmailSender emailSender, IConfiguration configuration)
        {
            _emailSender = emailSender;
            _configuration = configuration;

        }

        [BindProperty]
        public ContactFormModel ContactForm { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // ارسال ایمیل
            var emailBody = $"<p><b>نام:</b> {ContactForm.Name}</p>" +
                            $"<p><b>ایمیل:</b> {ContactForm.Email}</p>" +
                            $"<p><b>موضوع:</b> {ContactForm.Subject}</p>" +
                            $"<p><b>پیام:</b> {ContactForm.Message}</p>";

            await _emailSender.SendEmailAsync(_configuration["EmailSettings:FromEmail"], ContactForm.Subject, emailBody);

            ViewData["SuccessMessage"] = "پیام شما با موفقیت ارسال شد.";
            ModelState.Clear();
            ContactForm = new ContactFormModel();

            return Page();
        }
    }
}
