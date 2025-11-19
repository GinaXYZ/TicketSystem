using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketSystem.Models;
using TicketSystem.Services;
using TicketSystem.ViewModels;

namespace TicketSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly TicketService _ticketService;

        public HomeController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return View(tickets);
        }
        public IActionResult Error()
        {
            return View("Error");
        }

        public IActionResult GetAllTicketsAsync()
        {
            return View();
        }

        public IActionResult CreateTicket()
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

        public async Task<IActionResult> Login(string username, string password)
        {
            if (username == "admin" && password == "password")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Administrator"),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return View();
            }
            ;
            ViewBag.ErrorMessage = "Ungültiger Benutzername oder Passwort.";
            return View("Login");
        }

        public IActionResult AccessDenied()
        {
            return View("Error");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketRequest model)
        {
            if (ModelState.IsValid)
            {
                await _ticketService.CreateTicketAsync(model);
                return RedirectToAction("Index");
            }
            return View("CreateTicketAsync", model);
        }

        public async Task<IActionResult> UpdateTicket(int id, UpdateTicketRequest model)
        {
            if (ModelState.IsValid)
            {
                var updatedTicket = await _ticketService.UpdateTicketAsync(id, model);
                if (updatedTicket != null)
                {
                    return RedirectToAction("Index");
                }
                return NotFound();
            }
            return View("UpdateTicketAsync", model);
        }

        public async Task<IActionResult> DeleteTicket(int id)
        {
            var success = await _ticketService.DeleteTicketAsync(id);
            if (success)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
 