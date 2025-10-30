using System.ComponentModel.DataAnnotations;

namespace _0_Framework.Application.Email
{
    public class ContactFormModel
    {
        [Required(ErrorMessage = "نام را وارد کنید")]
        [Display(Name ="نام")]
        public string Name { get; set; }

        [Required(ErrorMessage = "ایمیل را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "موضوع را وارد کنید")]

        [Display(Name = "موضوع")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "پیام را وارد کنید")]
        [StringLength(2000, ErrorMessage = "پیام نباید بیشتر از 2000 کاراکتر باشد")]

        [Display(Name = "پیام")]
        public string Message { get; set; }
    }

}
