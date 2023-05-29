namespace DocTicket.Backend.DAL.Models
{
    public class AppointmentTime : BaseEntity
    {
        public DayOfWeek DayOfWeek { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }


        public virtual string DoctorId { get; set; } = null!;

        public virtual Doctor Doctor { get; set; } = null!;
    }
}
