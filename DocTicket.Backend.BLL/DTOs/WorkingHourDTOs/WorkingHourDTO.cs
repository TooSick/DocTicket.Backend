using DocTicket.Backend.BLL.Common.Mappings;
using DocTicket.Backend.DAL.Models;

namespace DocTicket.Backend.BLL.DTOs.WorkingHourDTOs
{
    public class WorkingHourDTO : IMapWith<WorkingHour>
    {
        public DayOfWeek DayOfWeek { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
