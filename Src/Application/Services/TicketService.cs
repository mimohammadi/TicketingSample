using Application.Dtos;
using Application.Dtos.Tickets;
using Application.Interfaces;
using Application.Mappers;
using Domain;
using Domain.Enums;
using Domain.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _repository;
        private readonly ICurrentUserService _currentUserService;

        public TicketService(IRepository<Ticket> repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task CreateTicket(CreateTicketDto dto)
        {
            await _repository.Insert(dto.MapTo(_currentUserService.UserId));
        }

        public async Task Delete(Guid id)
        {
 
             await _repository.Delete(id);
        }

        public async Task<TicketDto> GetByd(Guid Id)
        {
            var ticket = await _repository.Get(Id);
            if (ticket == null) throw new ArgumentNullException("Ticket not found");

            return ticket.MapTo();
        }

        public async Task<List<TicketDto>> GetList(PaginationDto dto)
        {
            Enum.TryParse(_currentUserService.Role, out Role role);
            if (role == Role.Admin)                
            {
                var tickets = await _repository.GetAll(dto.Skip, dto.Take);  
                return tickets.MapToList();
            }

            var ticket = await _repository.GetAll(dto.Skip, dto.Take, p => p.CreatedByUserId == _currentUserService.UserId);
            return ticket.MapToList();
        }

        public async Task<int> TicketCounts(Status status)
        {
            var tickets = await _repository.GetAll(skip: 0, take:int.MaxValue, p => p.Status == status);
            return tickets.Count();
        }

        public async Task Update(TicketDto dto)
        {
            var ticket = await _repository.Get(dto.Id);
            if (ticket == null) throw new ArgumentNullException("Ticket not found");

            ticket.Update(dto.Title, dto.Description, dto.Status, dto.Priority, dto.CreatedByUserId, dto.AssignedToUserId);
            await _repository.Update(ticket);
        }
    }
}
