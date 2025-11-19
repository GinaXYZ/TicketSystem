using Microsoft.AspNetCore.Mvc;

namespace TicketSystem.ViewModels
{
    public class TicketViewModel
    {

        public int Id { get; set; }
        public string Titel { get; set; } = string.Empty;
        public string Beschreibung { get; set; } = string.Empty;
        public string? Status_Id { get; set; } = string.Empty;
    }
}
