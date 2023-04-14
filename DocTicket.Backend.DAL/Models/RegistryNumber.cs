namespace DocTicket.Backend.DAL.Models
{
    public class RegistryNumber : BaseEntity
    {
        public string Number { get; set; } = null!;


        public virtual string PolyclinicId { get; set; } = null!;

        public virtual Polyclinic Polyclinic { get; set; } = null!;
    }
}
