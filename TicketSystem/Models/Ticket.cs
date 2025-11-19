namespace TicketSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int? Ersteller_Id { get; set; }
        public int? Status_Id { get; set; }
        public int? Priorität_Id { get; set; }
        public int? Kategorie_Id { get; set; }
        public string? Titel { get; set; }
        public string? Beschreibung { get; set; }
        public DateTime? Erstellungsdatum { get; set; }
        public DateTime? Fälligkeitsdatum { get; set; }
    }
}
