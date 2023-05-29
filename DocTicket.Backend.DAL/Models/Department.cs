namespace DocTicket.Backend.DAL.Models
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = null!;


        public virtual string PolyclinicId { get; set; } = null!;

        public virtual Polyclinic Polyclinic { get; set; } = null!;

        public virtual List<Doctor> Doctors { get; set; } = new();
    }
}
