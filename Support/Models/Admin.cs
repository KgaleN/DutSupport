namespace Support.Models
{
    public class Admin : ISupportUser
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public UserRole Role { get; set; }
    }
}
