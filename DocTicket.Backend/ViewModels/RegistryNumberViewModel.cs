using AutoMapper;
using DocTicket.Backend.Common.Mappings;
using DocTicket.Backend.Models;

namespace DocTicket.Backend.ViewModels
{
    public class RegistryNumberViewModel : IMapWith
    {
        public string Number { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(RegistryNumber), GetType());
        }
    }
}
