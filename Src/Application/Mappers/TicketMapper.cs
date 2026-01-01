using Application.Dtos.Tickets;
using Domain.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public static class TicketMapper
    {
        public static Ticket MapTo(this CreateTicketDto dto, Guid currentUserId)
        {
            return new Ticket(dto.Title, dto.Description, dto.Status, dto.Priority, currentUserId, dto.AssignedToUserId);
        }

        public static TicketDto MapTo(this Ticket ticket) 
        {
            return new TicketDto
            {
                AssignedToUserId = ticket.AssignedToUserId,
                CreatedByUserId = ticket.CreatedByUserId,
                Description = ticket.Description,
                Id = ticket.Id,
                Priority = ticket.Priority,
                Status = ticket.Status,
                Title = ticket.Title,
            };
        }

        public static List<TicketDto> MapToList(this IEnumerable<Ticket> tickets)
        {
            return tickets.Select(a=>a.MapTo()).ToList();
        }
    }
}
