using DocTicket.Backend.BLL.Common.Mappings;
using DocTicket.Backend.BLL.DTOs.RegistryNumberDTOs;
using DocTicket.Backend.BLL.DTOs.WorkingHourDTOs;
using DocTicket.Backend.DAL.Models;

namespace DocTicket.Backend.BLL.DTOs.PolyclinicDTOs
{
    public class PolyclinicDTO : IMapWith<Polyclinic>
    {
        public string PolyclinicId { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Address { get; set; } = null!;

        public List<RegistryNumberDTO> RegistryNumbers { get; set; } = new();

        public List<WorkingHourDTO> WorkingHours { get; set; } = new();
    }
}
