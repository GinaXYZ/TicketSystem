using TicketSystem.Models;
using TicketSystem.ViewModels;

namespace TicketSystem.Services
{
    public class TicketService
    {

        private readonly List<Ticket> _tickets = [];
        public IEnumerable<Ticket> GetAllTickets()
        {
            return _tickets;
        }

        public Ticket CreateTicket(CreateTicketRequest request)
        {
            var ticket = new Ticket
            {
                Id = _tickets.Count + 1,
                Title = request.Title,
                Description = request.Description,
                Status = "Open",
                CreatedAt = DateTime.UtcNow
            };
            _tickets.Add(ticket);
            return ticket;
        }

        /*public Ticket GetTicketById(int id)
        {
            var ticket = _tickets.FirstOrDefault(t => t.Id == id);
            if (ticket == null) return null;
            return new TicketViewModel
            {
                Id= ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
           */
            }
        }
   

