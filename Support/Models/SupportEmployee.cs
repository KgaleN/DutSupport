using System.ComponentModel.DataAnnotations.Schema;

namespace Support.Models
{
    public class SupportEmployee : ISupportUser
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }
        public List<Ticket>? AssignedTickets { get; set; }
        public List<Chat> Chats { get; set; }
    }
}
