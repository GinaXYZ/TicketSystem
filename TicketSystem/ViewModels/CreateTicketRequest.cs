using Microsoft.AspNetCore.Mvc;

namespace TicketSystem.ViewModels
{
    public class CreateTicketRequest 
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
