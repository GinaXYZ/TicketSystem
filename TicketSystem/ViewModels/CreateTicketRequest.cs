using Microsoft.AspNetCore.Mvc;

namespace TicketSystem.ViewModels
{
    public class CreateTicketRequest : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
