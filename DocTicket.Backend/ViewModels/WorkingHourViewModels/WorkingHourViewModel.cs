using DocTicket.Backend.BusinessLogic.Common.Mappings;
using DocTicket.Backend.DataAccess.Models;

namespace DocTicket.Backend.BusinessLogic.ViewModels.WorkingHourViewModels
{
    public class WorkingHourViewModel : IMapWith<WorkingHour>
    {
        public DayOfWeek DayOfWeek { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
