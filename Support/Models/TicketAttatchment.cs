using System.ComponentModel.DataAnnotations;

namespace Support.Models
{
    public class TicketAttachment
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public string ContentType { get; set; }
    }
}
