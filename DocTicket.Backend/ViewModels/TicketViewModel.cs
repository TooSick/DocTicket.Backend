using AutoMapper;
using DocTicket.Backend.Common.Mappings;
using DocTicket.Backend.Models;

namespace DocTicket.Backend.ViewModels
{
    public class TicketViewModel : IMapWith
    {
        public int Id { get; set; }

        public string? AppUserId { get; set; }

        public DateTime ReceptionTime { get; set; }

        public bool CanUserSendOffer { get; set; }

        public bool CanUserOrderTicket { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(Ticket), GetType());
        }
    }
}
