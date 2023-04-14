using DocTicket.Backend.BusinessLogic.Common.Mappings;
using DocTicket.Backend.BusinessLogic.ViewModels.RegistryNumberViewModels;
using DocTicket.Backend.BusinessLogic.ViewModels.WorkingHourViewModels;
using DocTicket.Backend.DataAccess.Models;

namespace DocTicket.Backend.BusinessLogic.ViewModels.PolyclinicViewModels
{
    public class PolyclinicViewModel : IMapWith<Polyclinic>
    {
        public string PolyclinicId { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Address { get; set; } = null!;

        public List<RegistryNumberViewModel> RegistryNumbers { get; set; } = new();

        public List<WorkingHourViewModel> WorkingHours { get; set; } = new();
    }
}
