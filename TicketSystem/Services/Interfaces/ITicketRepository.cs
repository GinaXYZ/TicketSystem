using TicketSystem.Models;

namespace TicketSystem.Services.Interfaces
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task<Ticket?> GetByIdAsync(int id);
        Task<Ticket> CreateAsync(Ticket ticket);
        Task<Ticket?> UpdateAsync(Ticket ticket);
        Task<bool> DeleteAsync(int id);

    }
}
