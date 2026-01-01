using Application.Dtos;
using Application.Dtos.Tickets;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITicketService
    {
        Task CreateTicket(CreateTicketDto dto);
        Task<List<TicketDto>> GetList(PaginationDto dto);
        Task<TicketDto> GetByd(Guid Id);
        Task<int> TicketCounts(Status status);
        Task Delete(Guid id);
        Task Update(TicketDto dto);
    }
}
