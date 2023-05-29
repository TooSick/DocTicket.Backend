using DocTicket.Backend.Services;
using DocTicket.Backend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocTicket.Backend.Controllers
{
    [Authorize(Roles = "User")]
    public class OfferController : Controller
    {
        private readonly OfferService _offerService;


        public OfferController(OfferService offerService)
        {
            _offerService = offerService;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] List<OrderTicketViewModel> tickets)
        {
            await _offerService.CreateOffer(tickets);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Outgoing()
        {
            var offers = await _offerService.GetUserOffersAsync();
            return View(offers);
        }

        public async Task<IActionResult> Incoming()
        {
            var offers = await _offerService.GetIncominOffers();
            return View(offers);
        }

        public async Task<IActionResult> Accept(int id)
        {
            await _offerService.AcceptOffer(id);
            return RedirectToAction("Incoming");
        }

        public async Task<IActionResult> Cancel(int id)
        {
            await _offerService.CancelOfferAsync(id);
            return RedirectToAction("Outgoing");
        }
    }
}
