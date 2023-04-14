using DocTicket.Backend.BusinessLogic.Common.Mappings;
using DocTicket.Backend.DataAccess.Models;

namespace DocTicket.Backend.BusinessLogic.ViewModels.RegistryNumberViewModels
{
    public class RegistryNumberViewModel : IMapWith<RegistryNumber>
    {
        public string Number { get; set; } = null!;
    }
}
