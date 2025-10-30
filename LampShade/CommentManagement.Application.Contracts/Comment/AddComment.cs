using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace CommentManagement.Application.Contracts.Comment
{
    public class AddComment
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست")]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Email { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Message { get; set; }
        public string Website { get; set; }
        public long OwnerRecordId { get; set; }
        public int Type { get; set; }
        public long ParentId { get; set; }
    }
}
