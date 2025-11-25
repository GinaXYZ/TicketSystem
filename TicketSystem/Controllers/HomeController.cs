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

        public async Task<IActionResult> Index()
        {
            IEnumerable<Ticket>? tickets = null;

            if (User?.Identity?.IsAuthenticated ?? false)
            {
                tickets = await _ticketService.GetAllTickets();
            }

            return View(tickets);       
        }

        public IActionResult Error()
        {
            return View("Error");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string? returnUrl = null)
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

                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Ungültiger Benutzername oder Passwort.");
            ViewData["ReturnUrl"] = returnUrl;
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Account()
        {
            var username = User.Identity?.Name;
            ViewBag.Username = username;
            return View("Account");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketRequest model)
        {
            if (ModelState.IsValid)
            {
                await _ticketService.CreateTicketAsync(model);
                return RedirectToAction("Index");
            }
            return View("CreateTicket", model);
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
            return View("UpdateTicket", model);
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

        public IActionResult AccessDenied()
        {
            return View("Error");
        }
    }
}