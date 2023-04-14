﻿using DocTicket.Backend.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DocTicket.Backend.DAL
{
    public static class DependencyInjection
    {
        public static void AddDocTicketDAL(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DocTicketDBContext>(options
                => options.UseSqlServer(connectionString));
        }
    }
}