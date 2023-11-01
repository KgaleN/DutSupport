using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Support.Models
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public string UserEmail { get; set; }
        [ForeignKey(nameof(TicketId))]
        public int TicketId { get; set; }
        public List<TicketResponse> Responses { get; set; }
    }
}
