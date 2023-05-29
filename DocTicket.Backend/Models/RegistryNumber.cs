namespace DocTicket.Backend.Models
{
    public class RegistryNumber : BaseEntity
    {
        public string Number { get; set; } = null!;


        public virtual int PolyclinicId { get; set; }

        public virtual Polyclinic Polyclinic { get; set; }
    }
}
