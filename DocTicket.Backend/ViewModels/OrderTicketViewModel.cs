using AutoMapper;
using DocTicket.Backend.Common.Mappings;
using DocTicket.Backend.Models;

namespace DocTicket.Backend.ViewModels
{
    public class OrderTicketViewModel : IMapWith
    {
        public int Id { get; set; }

        public DateTime ReceptionTime { get; set; }

        public ShortDoctorInfoViewModel Doctor { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(Ticket), GetType());
        }
    }
}
