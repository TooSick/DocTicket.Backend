namespace DocTicket.Backend.Models
{
    public class WorkingHour : BaseEntity
    {
        public DateTime DayOfWeek { get; set; }

        public int StartTime { get; set; }

        public int EndTime { get; set; }


        public virtual int PolyclinicId { get; set; }

        public virtual Polyclinic Polyclinic { get; set; }
    }
}
