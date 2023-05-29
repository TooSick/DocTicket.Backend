namespace DocTicket.Backend.Models
{
    public class Ticket : BaseEntity
    {
        public DateTime ReceptionTime { get; set; }


        public virtual int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; } = null!;

        public virtual string? AppUserId { get; set; }

        public virtual AppUser? AppUser { get; set; }

        public virtual List<Offer>? Offers { get; set; }
    }
}
