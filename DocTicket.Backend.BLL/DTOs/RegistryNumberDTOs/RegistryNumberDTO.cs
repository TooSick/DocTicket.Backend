using DocTicket.Backend.BLL.Common.Mappings;
using DocTicket.Backend.DAL.Models;

namespace DocTicket.Backend.BLL.DTOs.RegistryNumberDTOs
{
    public class RegistryNumberDTO : IMapWith<RegistryNumber>
    {
        public string Number { get; set; } = null!;
    }
}
