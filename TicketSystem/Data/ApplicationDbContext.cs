using Microsoft.EntityFrameworkCore;
using TicketSystem.Models;

namespace TicketSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Ticket> TICKET { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("TICKET");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Titel).HasColumnName("Titel");
                entity.Property(e => e.Beschreibung).HasColumnName("Beschreibung");
                entity.Property(e => e.Erstellungsdatum).HasColumnName("Erstellungsdatum");
                entity.Property(e => e.Ersteller_Id).HasColumnName("Ersteller_Id");
                entity.Property(e => e.Fälligkeitsdatum).HasColumnName("Fälligkeitsdatum");
                entity.Property(e => e.Kategorie_Id).HasColumnName("Kategorie_Id");
                entity.Property(e => e.Priorität_Id).HasColumnName("Priorität_Id");
                entity.Property(e => e.Status_Id).HasColumnName("Status_Id");
                entity.Property(e => e.Email).HasColumnName("Email");
            });
        }
    }
}
