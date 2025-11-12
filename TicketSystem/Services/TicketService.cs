using TicketSystem.Models;
using TicketSystem.ViewModels;
using TicketSystem.Services.Interfaces;

namespace TicketSystem.Services
{
    public class TicketService
    {

        private readonly ITicketRepository _repository;

        public TicketService(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Ticket?> GetTicketByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);

        }

        public async Task<Ticket> CreateTicketAsync(CreateTicketRequest model)
        {
            var ticket = new Ticket
            {
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.UtcNow,
                Status = "Open"
            };
            return await _repository.CreateAsync(ticket);
        }

        public async Task<Ticket?> UpdateTicketAsync(int id, UpdateTicketRequest model)
        {
            var existingTicket = await _repository.GetByIdAsync(id);
            if (existingTicket == null)
            {
                return null;
            }
            existingTicket.Title = model.Title;
            existingTicket.Description = model.Description;
            existingTicket.Status = model.Status;
            return await _repository.UpdateAsync(existingTicket);
        }
        public async Task<bool> DeleteTicketAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
   

