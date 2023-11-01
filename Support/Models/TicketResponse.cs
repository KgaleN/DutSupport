using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Support.Models
{
    public class TicketResponse
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        public string Body { get; set; }
        [Display(Name = "Attach File")]
        public List<TicketAttachment> Attachments { get; set; } 
        [ScaffoldColumn(false)]
        public string EmailSender { get; set; }
        public string EmailRecipient { get; set; }
        [ForeignKey(nameof(TicketId))]
        public int TicketId { get; set; }   
    }
}