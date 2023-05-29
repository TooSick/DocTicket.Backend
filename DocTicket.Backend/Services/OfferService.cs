using AutoMapper;
using DocTicket.Backend.EF;
using DocTicket.Backend.Models;
using DocTicket.Backend.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DocTicket.Backend.Services
{
    public class OfferService
    {
        private readonly DocTicketDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        
        public OfferService(DocTicketDBContext context, UserManager<AppUser> userManager, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        
        public async Task CreateOffer(List<OrderTicketViewModel> orderTickets)
        {
            var userTicket = await _context.Tickets.Include(t => t.AppUser)
                .FirstOrDefaultAsync(t => t.Id == orderTickets[0].Id);
            var anotherUserTicket = await _context.Tickets.Include(t => t.AppUser)
                .FirstOrDefaultAsync(t => t.Id == orderTickets[1].Id);

            await _context.Offers.AddAsync(new Offer
            {
                Tickets = new List<Ticket>
                {
                    userTicket,
                    anotherUserTicket,
                },

                AppUsers = new List<AppUser>
                {
                    userTicket.AppUser,
                    anotherUserTicket.AppUser,
                },

                AppUserIdThatSendOffer = userTicket.AppUserId
            });

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OutgoingOfferViewModel>> GetUserOffersAsync()
        {
            var userId = _userManager.GetUserId(_contextAccessor.HttpContext.User);

            var userOffers = await _context.Offers.Include(o => o.Tickets).ThenInclude(t => t.Doctor)
                .Where(o => o.Tickets.Any(t => t.AppUserId == userId) && o.AppUserIdThatSendOffer == userId).ToListAsync();

            userOffers.ForEach(u => u.Tickets = u.Tickets.OrderBy(t => t.AppUserId != userId).ThenBy(t => t.AppUserId == userId).ToList());

            return _mapper.Map<IEnumerable<Offer>, IEnumerable<OutgoingOfferViewModel>>(userOffers);
        }

        public async Task<IEnumerable<OutgoingOfferViewModel>> GetIncominOffers()
        {
            var userId = _userManager.GetUserId(_contextAccessor.HttpContext.User);

            var userOffers = await _context.Offers.Include(o => o.Tickets).ThenInclude(t => t.Doctor)
                .Where(o => o.Tickets.Any(t => t.AppUserId == userId) && o.AppUserIdThatSendOffer != userId).ToListAsync();

            userOffers.ForEach(u => u.Tickets = u.Tickets.OrderBy(t => t.AppUserId != userId).ThenBy(t => t.AppUserId == userId).ToList());

            return _mapper.Map<IEnumerable<Offer>, IEnumerable<OutgoingOfferViewModel>>(userOffers);
        }

        public async Task AcceptOffer(int offerId)
        {
            var offer = await _context.Offers.Include(o => o.Tickets).FirstOrDefaultAsync(o => o.Id == offerId);
            var userId = _userManager.GetUserId(_contextAccessor.HttpContext.User);

            offer.Tickets[0].AppUserId = offer.Tickets[1].AppUserId;
            offer.Tickets[1].AppUserId = userId;

            _context.Remove(offer);
            await _context.SaveChangesAsync();
        }

        public async Task CancelOfferAsync(int id)
        {
            var userId = _userManager.GetUserId(_contextAccessor.HttpContext.User);
            var userOffer = await _context.Offers.FirstOrDefaultAsync(o => o.Id == id);

            _context.Offers.Remove(userOffer);
            await _context.SaveChangesAsync();
        }
    }
}
