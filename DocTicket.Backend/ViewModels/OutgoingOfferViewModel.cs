using AutoMapper;
using DocTicket.Backend.Common.Mappings;
using DocTicket.Backend.Models;

namespace DocTicket.Backend.ViewModels
{
    public class OutgoingOfferViewModel : IMapWith
    {
        public int Id { get; set; }

        public List<OrderTicketViewModel> Tickets { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(Offer), GetType());
        }
    }
}
