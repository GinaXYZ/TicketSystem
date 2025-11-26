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
                
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Titel).HasColumnName("titel").HasMaxLength(255);
                entity.Property(e => e.Beschreibung).HasColumnName("beschreibung");
                entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(255);
                entity.Property(e => e.Erstellungsdatum)
                      .HasColumnName("erstellungsdatum")
                      .HasColumnType("date")
                      .HasDefaultValueSql("getdate()");
                entity.Property(e => e.Ersteller_Id).HasColumnName("ersteller_id").IsRequired();
                entity.Property(e => e.Fälligkeitsdatum).HasColumnName("fälligkeitsdatum").HasColumnType("date");
                entity.Property(e => e.Kategorie_Id).HasColumnName("kategorie_id").IsRequired();
                entity.Property(e => e.Priorität_Id).HasColumnName("priorität_id").IsRequired();
                entity.Property(e => e.Status_Id).HasColumnName("status_id").IsRequired();
            });
        }
    }
}
