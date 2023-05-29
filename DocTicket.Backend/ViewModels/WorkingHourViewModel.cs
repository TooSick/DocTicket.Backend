using AutoMapper;
using DocTicket.Backend.Common.Mappings;
using DocTicket.Backend.Models;

namespace DocTicket.Backend.ViewModels
{
    public class WorkingHourViewModel : IMapWith
    {
        public DateTime DayOfWeek { get; set; }

        public int StartTime { get; set; }

        public int EndTime { get; set; }

        
        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(WorkingHour), GetType());
        }
    }
}
