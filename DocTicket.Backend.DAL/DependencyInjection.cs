using DocTicket.Backend.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using DocTicket.Backend.DAL.Models;

namespace DocTicket.Backend.DAL
{
    public static class DependencyInjection
    {
        public static void AddDocTicketDAL(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DocTicketDBContext>(options
                => options.UseSqlServer(connectionString));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<DocTicketDBContext>();
        }
    }
}
