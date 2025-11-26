using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        
        [Required]
        public int Ersteller_Id { get; set; }
        
        [Required]
        public int Status_Id { get; set; }
        
        [Required]
        public int Priorität_Id { get; set; }
        
        [Required]
        public int Kategorie_Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Titel { get; set; } = string.Empty;
        
        public string? Beschreibung { get; set; }
        
        [EmailAddress]
        [MaxLength(255)]
        public string? Email { get; set; }
        
        public DateTime Erstellungsdatum { get; set; } = DateTime.Now;
        
        public DateTime? Fälligkeitsdatum { get; set; }
    }
}
