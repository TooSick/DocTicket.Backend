using DocTicket.Backend.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace DocTicket.Backend.BLL
{
    public static class DependencyInjection
    {
        public static void AddDocTicketBLL(this IServiceCollection services, string connectionString)
        {
            services.AddDocTicketDAL(connectionString);


        }
    }
}
