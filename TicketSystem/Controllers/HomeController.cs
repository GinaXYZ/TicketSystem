using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TicketSystem.Data;
using TicketSystem.Models;
using TicketSystem.Services;
using TicketSystem.ViewModels;

namespace TicketSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly TicketService _ticketService;
        private readonly ApplicationDbContext _context;

        public HomeController(TicketService ticketService, ApplicationDbContext context)
        {
            _ticketService = ticketService;
            _context = context;
        }

        public enum TicketStatus
        {
            Open = 1,
            InProgress = 2,
            Closed = 3
        }
        
        public async Task<IActionResult> Index(int page = 1, string? sortTickets = null)
        {
            IEnumerable<Ticket>? tickets = null;
            const int pageSize = 10;
            
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                var allTickets = await _ticketService.GetAllTickets();
                
                tickets = sortTickets switch
                {
                    "1" => allTickets.OrderBy(t => t.Status_Id).ThenBy(t => t.Priorität_Id),
                    "2" => allTickets.OrderByDescending(t => t.Status_Id).ThenByDescending(t => t.Priorität_Id),
                    "3" => allTickets.OrderBy(t => t.Id),
                    "4" => allTickets.OrderByDescending(t => t.Id),
                    _ => allTickets
                };
            }
            
            ViewBag.OpenTicketsCount = tickets?.Count(t => t.Status_Id == 1) ?? 0;
            ViewBag.ClosedTicketsCount = tickets?.Count(t => t.Status_Id == 3) ?? 0;
            ViewBag.InProgressCount = tickets?.Count(t => t.Status_Id == 2) ?? 0;

            if (tickets != null)
            {
                var totalCount = tickets.Count();
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
                tickets = tickets.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            
            ViewBag.CurrentSort = sortTickets;
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
            // Suche nach Email oder Name und vergleiche mit PasswordHash
            var benutzer = await _context.BENUTZER
                .FirstOrDefaultAsync(b => (b.Email == username || b.Name == username) 
                                       && b.PasswordHash == password);

            if (benutzer != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, benutzer.Name),
                    new Claim(ClaimTypes.Email, benutzer.Email),
                    new Claim(ClaimTypes.NameIdentifier, benutzer.Id.ToString()),
                    new Claim(ClaimTypes.Role, benutzer.Rolle)
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

        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return RedirectToAction("Error");
            }

            var tickets = new List<Ticket> { ticket };

            ViewBag.OpenTicketsCount = tickets.Count(t => t.Status_Id == 1);
            ViewBag.ClosedTicketsCount = tickets.Count(t => t.Status_Id == 3);
            ViewBag.InProgressCount = tickets.Count(t => t.Status_Id == 2);
            ViewBag.CurrentPage = 1;
            ViewBag.TotalPages = 1;

            return View("Index", tickets);
        }
    }
}