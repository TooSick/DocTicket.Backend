namespace DocTicket.Backend.DAL.Models
{
    public class Ticket : BaseEntity
    {
        public DateTime ReceptionTime { get; set; }


        public virtual string DoctorId { get; set; } = null!;

        public virtual Doctor Doctor { get; set; } = null!;

        public virtual string? AppUserId { get; set; }

        public virtual AppUser? AppUser { get; set; }

        public virtual List<Offer>? Offers { get; set; }
    }
}
