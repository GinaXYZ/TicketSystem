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
        public Task<Ticket> CreateAsync(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Ticket>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Ticket?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket?> UpdateAsync(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
