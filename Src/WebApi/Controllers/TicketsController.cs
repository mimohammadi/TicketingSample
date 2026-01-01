using Application.Dtos;
using Application.Dtos.Tickets;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }



        [Authorize(Roles = "Employee")]
        [HttpPost("/tickets")]
        public async Task<IActionResult> CreateTicket(CreateTicketDto dto) 
        {
            await _ticketService.CreateTicket(dto);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("/tickets")]
        public async Task<IActionResult> GetAllTickets() 
        {
            return Ok(await _ticketService.GetList(new PaginationDto
            {
                Skip = 0,
                Take = int.MaxValue
            }));
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut("/tickets/{id}")]
        public async Task<IActionResult> Update(TicketDto dto) 
        {
            await _ticketService.Update(dto);
            return Ok();
        }
        
        
        [Authorize(Roles = "Admin")]
        [HttpDelete("/tickets/{id}")]
        public async Task<IActionResult> Delete(Guid id) 
        {
            await _ticketService.Delete(id);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("/tickets/stats")]
        public async Task<IActionResult> Getstats(Status status)
        {
            return Ok(await _ticketService.TicketCounts(status));
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("/tickets/my")]
        public async Task<IActionResult> GetMyTickets(PaginationDto dto)
        {
            return Ok(await _ticketService.GetList(dto));
        }
    }
}
