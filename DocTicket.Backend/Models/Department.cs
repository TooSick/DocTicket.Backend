namespace DocTicket.Backend.Models
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = null!;


        public virtual int PolyclinicId { get; set; }

        public virtual Polyclinic Polyclinic { get; set; }

        public virtual List<Doctor> Doctors { get; set; } = new();
    }
}
