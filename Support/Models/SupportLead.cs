using System.ComponentModel.DataAnnotations;

namespace Support.Models
{
    public class SupportLead : ISupportUser
    {
        [Key]
        public string Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }
        public List<Ticket> TicketInbox { get; set; }
    }
}
