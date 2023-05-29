using Microsoft.AspNetCore.Identity;

namespace DocTicket.Backend.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Patronymic { get; set; } = null!;


        public virtual List<Ticket>? Tickets { get; set; }

        public virtual List<Offer>? Offers { get; set; }
    }
}
