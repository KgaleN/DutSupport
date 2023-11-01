using System.ComponentModel.DataAnnotations;

namespace TicketSupport.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        [Display(Name = "Ticket Type")]
        public TicketType Type { get; set; }
        [Display(Name = "Attach File")]
        public List<TicketAttachment> Attachments { get; set; }
        [ScaffoldColumn(false)]
        public TicketStatus Status { get; set; }
        [ScaffoldColumn(false)]
        public DateTime TimeStamp { get; set; }
        [ScaffoldColumn(false)]
        public string UserEmail { get; set; }
        [ScaffoldColumn(false)]
        public Priority TicketPriority { get; set; }
    }
}
