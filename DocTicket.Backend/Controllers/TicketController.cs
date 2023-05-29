using DocTicket.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocTicket.Backend.Controllers
{
    [Authorize(Roles = "User")]
    public class TicketController : Controller
    {
        private readonly TicketService _ticketService;


        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }


        public async Task<IActionResult> Order(int id)
        {
            var ticket = await _ticketService.OrderTicket(id);
            
            return View(ticket);
        }

        public async Task<IActionResult> List()
        {
            var tickets = await _ticketService.List();

            return View(tickets);
        }

        public async Task<IActionResult> Cancel(int id)
        {
            await _ticketService.CancelTicket(id);
            return RedirectToAction("List", "Ticket");
        }

        public async Task<IActionResult> Exchange(int id)
        {
            var tickets = await _ticketService.Exchange(id);

            return View(tickets);
        }
    }
}
