namespace DocTicket.Backend.DAL.Models
{
    public class Chat : BaseEntity
    {
        public string Title { get; set; } = null!;


        public virtual string TicketId { get; set; } = null!;

        public virtual Ticket Ticket { get; set; } = null!;

        public virtual List<AppUser>? AppUsers { get; set; } = new();

        public virtual List<Message>? Messages { get; set; } = new();
    }
}
