using Microsoft.EntityFrameworkCore;
using Support.Models;

namespace Support.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .ToTable(tb => tb.HasTrigger("SomeTrigger"));
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<SupportEmployee> SupportEmployees { get; set; }
        public DbSet<SupportLead> SupportLead { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketAttachment> TicketAttachments { get; set; }
        public DbSet<TicketResponse> TicketResponses { get; set; }



    }
}
