using AutoMapper;
using DocTicket.Backend.Common.Mappings;
using DocTicket.Backend.Models;

namespace DocTicket.Backend.ViewModels
{
    public class ShortDoctorInfoViewModel : IMapWith
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string Specialization { get; set; }

        public string FullName => $"{LastName} {FirstName} {Patronymic}";

        public string? PolyclinicName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(Doctor), GetType());
        }
    }
}
