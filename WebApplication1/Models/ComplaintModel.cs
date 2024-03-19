using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ComplaintModel
    {
        [Display(Name = "Message")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        public string MailBody { get; set; } = default!;
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        //[EmailAddress(ErrorMessage = "{0} is invalid email")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Invalid Email Address.")]
        public string MailTo { get; set; } = default!;
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        public string MailFrom { get; set; } = default!;
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        public string MailSubject { get; set; } = default!;
        [Display(Name = "Issue To")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        public DateTime IssueTo { get; set; } = DateTime.Now;
        [Display(Name = "Issue From")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        public DateTime IssueFrom { get; set; } = DateTime.Now;
        public List<ComplaintTypeSubject> complaintTypesList { get; set; } = new();
        public string subject { get; set; } = default!;
    }
    public class ComplaintTypeSubject
    {
        public int id { get; set; }
        public string ComplainTypeSubjectName { get; set; } = default!;
        public bool IsSelected { get; set; } = false;
        public string GroupName { get; set; } = default!;
    }
}
