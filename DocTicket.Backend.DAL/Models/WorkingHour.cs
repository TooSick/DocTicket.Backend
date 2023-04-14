namespace DocTicket.Backend.DAL.Models
{
    public class WorkingHour : BaseEntity
    {
        public DayOfWeek DayOfWeek { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }


        public string PolyclinicId { get; set; } = null!;

        public virtual Polyclinic Polyclinic { get; set; } = null!;
    }
}
