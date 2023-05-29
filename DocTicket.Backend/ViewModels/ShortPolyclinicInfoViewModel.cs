using AutoMapper;
using DocTicket.Backend.Common.Mappings;
using DocTicket.Backend.Models;

namespace DocTicket.Backend.ViewModels
{
    public class ShortPolyclinicInfoViewModel : BaseViewModel, IMapWith
    {
        public string Title { get; set; }

        public string Address { get; set; }

        public List<WorkingHourViewModel> WorkingHours { get; set; }

        public List<RegistryNumberViewModel> RegistryNumbers { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(Polyclinic), GetType());
        }
    }
}
