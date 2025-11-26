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
    public class HomeController(TicketService ticketService) : Controller
    {
        private readonly TicketService _ticketService = ticketService;

        public IEnumerable<Ticket> Tickets { get; set; } = Enumerable.Empty<Ticket>();
        public int? OpenTicketsCount { get; set; }
        public int? ClosedTicketsCount { get; set; }
        public int? InProgressCount { get; set; }


        public async Task<IActionResult> Index(int page =1)
        {
            IEnumerable<Ticket>? tickets = null;
            const int pageSize = 10;
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                tickets = await _ticketService.GetAllTickets();
            }
            ViewBag.OpenTicketsCount = tickets?.Count(t => t.Status_Id == 1) ?? 0;
            ViewBag.ClosedTicketsCount = tickets?.Count(t => t.Status_Id == 3) ?? 0;
            ViewBag.InProgressCount = tickets?.Count(t => t.Status_Id == 2) ?? 0;


            if (tickets != null)
            {
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = (int)Math.Ceiling(tickets.Count() / (double)pageSize);
                tickets = tickets.Skip((page - 1) * pageSize).Take(pageSize);
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