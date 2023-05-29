namespace DocTicket.Backend.Models
{
    public class AppointmentTime : BaseEntity
    {
        public DayOfWeek DayOfWeek { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }


        public virtual int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; } = null!;
    }
}
