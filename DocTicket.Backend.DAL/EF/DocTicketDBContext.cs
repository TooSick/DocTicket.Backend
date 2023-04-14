using DocTicket.Backend.DAL.EF.EntitiesConfig;
using DocTicket.Backend.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DocTicket.Backend.DAL.EF
{
    public class DocTicketDBContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Polyclinic> Polyclinics { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<RegistryNumber> RegistryNumbers { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }


        public DocTicketDBContext(DbContextOptions<DocTicketDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfiguration(new AppUserConfig());
            

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
