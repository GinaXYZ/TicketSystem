using Microsoft.AspNetCore.Mvc;
using TicketSystem.Models;
using TicketSystem.ViewModels;

namespace TicketSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult GetAllTicketsAsync()
        {
            return View();
        }

        public IActionResult CreateTicketAsync()
        {
            return View();
        }

        public IActionResult UpdateTicketAsync()
        {
            return View();
        }

        public IActionResult DeleteTicketAsync()
        {
            return View();
        }
    }
}
 