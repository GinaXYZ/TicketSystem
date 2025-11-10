using Microsoft.AspNetCore.Mvc;

namespace TicketSystem.Data
{
    public class ApplicationDbContext : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
