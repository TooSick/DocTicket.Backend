namespace DocTicket.Backend.DAL.Models
{
    public class AppUser
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;


        public virtual List<Ticket>? Tickets { get; set; }

        public virtual List<Chat>? Chats { get; set; }

        public virtual List<Message>? Messages { get; set; }
    }
}
