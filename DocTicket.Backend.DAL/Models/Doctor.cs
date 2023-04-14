namespace DocTicket.Backend.DAL.Models
{
    public class Doctor : EntityWithPhoto
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Patronymic { get; set; } = null!;

        public string Specialization { get; set; } = null!;


        public virtual string PolyclinicId { get; set; } = null!;

        public virtual Polyclinic Polyclinic { get; set; } = null!;

        public virtual List<Ticket> Tickets { get; set; } = new();
    }
}
