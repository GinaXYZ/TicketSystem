using Microsoft.AspNetCore.Mvc;

namespace TicketSystem.ViewModels
{
    public class UpdateTicketRequest : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
