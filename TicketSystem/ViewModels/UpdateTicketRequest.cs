using Microsoft.AspNetCore.Mvc;

namespace TicketSystem.ViewModels
{
    public class UpdateTicketRequest
    {
        public string? Titel { get; set; }
        public string? Beschreibung { get; set; }
        public string? Status_Id { get; set; }
    }
}
