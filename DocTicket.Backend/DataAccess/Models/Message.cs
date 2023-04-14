namespace DocTicket.Backend.DataAccess.Models
{
    public class Message : BaseEntity
    {
        public string Msg { get; set; } = null!;

        public DateTime SendingTime { get; set; }


        public virtual AppUser AppUser { get; set; } = null!;

        public virtual Chat Chat { get; set; } = null!;
    }
}
