using Microsoft.AspNetCore.Mvc;

namespace TicketSystem.ViewModels
{
    public class TicketViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
