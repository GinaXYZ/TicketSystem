using Microsoft.EntityFrameworkCore;
using TicketSystem.Models;
using TicketSystem.Services.Interfaces;
using TicketSystem.Data;

namespace TicketSystem.Data.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Ticket> CreateAsync(Ticket ticket)
        {
            _context.TICKET.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<bool> DeleteAsync(int id)
        {
           var ticket = await _context.TICKET.FindAsync(id);
              if (ticket == null)
              {
                return false;
            }
              _context.TICKET.Remove(ticket);
                await _context.SaveChangesAsync();
                return true;

        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        { 
            return await _context.TICKET.ToListAsync();
        }

        public async Task<Ticket?> GetByIdAsync(int id)
        {
           return await _context.TICKET.FindAsync(id);
        }

        public async Task<Ticket?> UpdateAsync(Ticket ticket)
        {
            _context.TICKET.Update(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }
    }
}
