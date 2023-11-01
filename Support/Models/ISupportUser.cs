using System.ComponentModel.DataAnnotations;

namespace Support.Models
{
    public interface ISupportUser
    {
        [Key]
        string Id { get; }
        string Email { get; }
        string Name { get; }
        UserRole Role { get; }
    }
}
