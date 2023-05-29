using DocTicket.Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DocTicket.Backend.EF
{
    public class DocTicketDBContext : IdentityDbContext<AppUser>
    {
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Polyclinic> Polyclinics { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<RegistryNumber> RegistryNumbers { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Offer> Offers { get; set; }

        //public DbSet<AppointmentTime> AppointmentTimes { get; set; }

        public DbSet<WorkingHour> WorkingHours { get; set; }


        public DocTicketDBContext(DbContextOptions<DocTicketDBContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new AppUserConfig());

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Polyclinic>().HasData(
            //    new Polyclinic[]
            //    {
            //        new Polyclinic 
            //        { 
            //            Address = "г. Минск, ул. Сурганова, д. 45, корп. 4",
            //            Title = "33-я городская студенческая поликлиника г. Минска",
            //            GeneralInformation = "test info",
            //            //RegistryNumbers = new List<RegistryNumber>
            //            //{
            //            //    new RegistryNumber { Number = "+375336712435" },
            //            //    new RegistryNumber { Number = "+375336712436" }
            //            //},
            //            //WorkingHours = new List<WorkingHour>
            //            //{
            //            //    new WorkingHour { DayOfWeek = DayOfWeek.Monday, StartTime = DateTime.Now, EndTime = DateTime.Now },
            //            //}
            //        },

            //        new Polyclinic
            //        {
            //            Address = "г. Гродно, ул. Кленовая, д. 25",
            //            Title = "5-я городская поликлиника г. Гродно",
            //            GeneralInformation = "test info"
            //        }
            //    });

            //modelBuilder.Entity<RegistryNumber>().HasData(
            //    new RegistryNumber[]
            //    {
            //        new RegistryNumber
            //        {
            //            Number = "+375336712435",
            //            PolyclinicId = Polyclinics.Single(p => p.Title == "33-я городская студенческая поликлиника г. Минска").Id
            //        },

            //        new RegistryNumber
            //        {
            //            Number = "+375336712436",
            //            PolyclinicId = Polyclinics.Single(p => p.Title == "33-я городская студенческая поликлиника г. Минска").Id
            //        },

            //        new RegistryNumber
            //        {
            //            Number = "+375336712437",
            //            PolyclinicId = Polyclinics.Single(p => p.Title == "5-я городская поликлиника г. Гродно").Id
            //        }
            //    });



        }
    }
}
