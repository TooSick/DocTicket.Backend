namespace DocTicket.Backend.Models
{
    public class Offer : BaseEntity
    {
        public string AppUserIdThatSendOffer { get; set; }


        public virtual List<Ticket> Tickets { get; set; } = new(2);

        public virtual List<AppUser> AppUsers { get; set; } = new(2);
    }
}
