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

        public async Task<IEnumerable<Ticket>> GetAllTickets()
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
                    Titel = model.Titel ?? string.Empty,
                Beschreibung = model.Beschreibung,
                Email = model.Email,
                Erstellungsdatum = DateTime.Now,
                Status_Id = model.Status_Id ?? 1,
                Kategorie_Id = model.Kategorie_Id ?? 1,
                Priorität_Id = model.Priorität_Id ?? 2,
                Ersteller_Id = model.Ersteller_Id ?? 1
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
            
            existingTicket.Titel = model.Titel;
            existingTicket.Beschreibung = model.Beschreibung;

            if (string.IsNullOrWhiteSpace(model.Status_Id))
            {
                existingTicket.Status_Id = 1;
            }
            else if (int.TryParse(model.Status_Id, out var statusId))
            {
                existingTicket.Status_Id = statusId;
            }
            else
            {
                existingTicket.Status_Id = 1;
            }
            
            return await _repository.UpdateAsync(existingTicket);
        }
        
        public async Task<bool> DeleteTicketAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}

