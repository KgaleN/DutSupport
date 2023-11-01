using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using TicketSupport.Models;

namespace TicketSupport.Data
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


        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketAttachment> TicketAttachments { get; set; }
        

       }
}
