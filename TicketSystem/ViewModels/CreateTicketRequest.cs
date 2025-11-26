using Microsoft.AspNetCore.Mvc;

namespace TicketSystem.ViewModels
{
    public class CreateTicketRequest 
    {
        public string Titel { get; set; } = string.Empty;
        public string Beschreibung { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;
        public int? Status_Id { get; set; }

        public int? Kategorie_Id { get; set; }

        public int? Ticket_Id { get; set; } = int.MaxValue;

        public int? Priorität_Id { get; set; }

        public int? Ersteller_Id { get; set; }

    }
}
