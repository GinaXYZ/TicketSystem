using Microsoft.AspNetCore.Mvc;

namespace TicketSystem.ViewModels
{
    public class UpdateTicketRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}
