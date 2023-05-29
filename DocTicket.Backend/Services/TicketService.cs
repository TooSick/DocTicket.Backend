using AutoMapper;
using DocTicket.Backend.EF;
using DocTicket.Backend.Models;
using DocTicket.Backend.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DocTicket.Backend.Services
{
    public class TicketService
    {
        private readonly DocTicketDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;


        public TicketService(DocTicketDBContext context, UserManager<AppUser> userManager, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }


        public async Task<OrderTicketViewModel> OrderTicket(int ticketId)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Doctor).ThenInclude(d => d.Department).ThenInclude(d => d.Polyclinic)
                .Include(t => t.AppUser)
                .FirstOrDefaultAsync(t => t.Id == ticketId);

            var userId = _userManager.GetUserId(_contextAccessor.HttpContext.User);
            ticket.AppUserId = userId;

            await _context.SaveChangesAsync();

            return _mapper.Map<Ticket, OrderTicketViewModel>(ticket);
        }

        public async Task<IEnumerable<OrderTicketViewModel>> List()
        {
            var userId = _userManager.GetUserId(_contextAccessor.HttpContext.User);
            var tickets = await _context.Tickets.Include(t => t.Doctor).Where(t => t.AppUserId == userId).ToListAsync();

            return _mapper.Map<IEnumerable<Ticket>, IEnumerable<OrderTicketViewModel>>(tickets);
        }

        public async Task CancelTicket(int ticketId)
        {
            var ticket = await _context.Tickets.Include(t => t.Offers).FirstOrDefaultAsync(t => t.Id == ticketId);

            if (ticket.Offers.Any())
                _context.Offers.RemoveRange(ticket.Offers);

            ticket.AppUserId = null;
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderTicketViewModel>> Exchange(int anotherUserTicketId)
        {
            var anotherUserTicket = await _context.Tickets
                .Include(t => t.Doctor)
                .FirstOrDefaultAsync(t => t.Id == anotherUserTicketId);
            var userId = _userManager.GetUserId(_contextAccessor.HttpContext.User);

            CultureInfo cultureInfo = new CultureInfo("ru-Ru");
            var currentUserTickets = await _context.Tickets
                .Include(t => t.Doctor).Where(t => t.AppUserId == userId).ToListAsync();
            var currentUserTicket = currentUserTickets
                .FirstOrDefault(t => t.DoctorId == anotherUserTicket.DoctorId
                    && t.ReceptionTime.ToString("M", cultureInfo) == anotherUserTicket.ReceptionTime.ToString("M", cultureInfo));

            return _mapper.Map<List<Ticket>, List<OrderTicketViewModel>>(new List<Ticket>
            {
                currentUserTicket,
                anotherUserTicket,
            });
        }
    }
}
