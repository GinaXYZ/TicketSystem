using Microsoft.AspNetCore.Mvc;

namespace TicketSystem.ViewModels
{
    public class CreateTicketRequest 
    {
        public string Titel { get; set; } = string.Empty;
        public string Beschreibung { get; set; } = string.Empty;
    }
}
