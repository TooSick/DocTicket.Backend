using DocTicket.Backend.Extensions;
using DocTicket.Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace DocTicket.Backend.EF
{
    public class DbInitializer
    {
        public static async Task SeedDataAsync(IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<DocTicketDBContext>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                context.Database.EnsureCreated();

                await SeedPolyclinicsAsync(context);
                await SeedRegistryNumbersAsync(context);
                await SeedWorkingHoursASync(context);
                await SeedDepartmentsAsync(context);
                await SeedDoctorsAsync(context);
                await SeedTicketsAsync(context);
                await SeedAccountsAsync(context, roleManager, userManager);
            }
        }

        private static async Task SeedWorkingHoursASync(DocTicketDBContext context)
        {
            if (!context.WorkingHours.Any())
            {
                await context.WorkingHours.AddRangeAsync(new List<WorkingHour>
                {
                    new WorkingHour { DayOfWeek = DayOfWeek.Monday.ToDateTime(), StartTime = 9, EndTime = 18, PolyclinicId = 1 },
                    new WorkingHour { DayOfWeek = DayOfWeek.Tuesday.ToDateTime(), StartTime = 9, EndTime = 18, PolyclinicId = 1 },
                    new WorkingHour { DayOfWeek = DayOfWeek.Wednesday.ToDateTime(), StartTime = 9, EndTime = 18, PolyclinicId = 1 },
                    new WorkingHour { DayOfWeek = DayOfWeek.Thursday.ToDateTime(), StartTime = 9, EndTime = 18, PolyclinicId = 1 },
                    new WorkingHour { DayOfWeek = DayOfWeek.Friday.ToDateTime(), StartTime = 9, EndTime = 18, PolyclinicId = 1 },
                    new WorkingHour { DayOfWeek = DayOfWeek.Monday.ToDateTime(), StartTime = 9, EndTime = 18, PolyclinicId = 2 },
                    new WorkingHour { DayOfWeek = DayOfWeek.Tuesday.ToDateTime(), StartTime = 9, EndTime = 18, PolyclinicId = 2 },
                    new WorkingHour { DayOfWeek = DayOfWeek.Wednesday.ToDateTime(), StartTime = 9, EndTime = 18, PolyclinicId = 2 },
                    new WorkingHour { DayOfWeek = DayOfWeek.Thursday.ToDateTime(), StartTime = 9, EndTime = 18, PolyclinicId = 2 },
                    new WorkingHour { DayOfWeek = DayOfWeek.Friday.ToDateTime(), StartTime = 9, EndTime = 18, PolyclinicId = 2 },
                });

                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedRegistryNumbersAsync(DocTicketDBContext context)
        {
            if (!context.RegistryNumbers.Any())
            {
                await context.RegistryNumbers.AddRangeAsync(new List<RegistryNumber>
                {
                    new RegistryNumber { Number = "+375336712435", PolyclinicId = 1 },
                    new RegistryNumber { Number = "+375336712436", PolyclinicId = 1 },
                    new RegistryNumber { Number = "+375336712437", PolyclinicId = 1 },
                    new RegistryNumber { Number = "+375336712435", PolyclinicId = 2 },
                    new RegistryNumber { Number = "+375336712436", PolyclinicId = 2 },
                    new RegistryNumber { Number = "+375336712437", PolyclinicId = 2 },
                });

                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedPolyclinicsAsync(DocTicketDBContext context)
        {
            if (!context.Polyclinics.Any())
            {
                await context.Polyclinics.AddRangeAsync(new List<Polyclinic>
                {
                    new Polyclinic
                    {
                        Address = "г. Минск, ул. Сурганова, д. 45, корп. 4",
                        Title = "33-я городская студенческая поликлиника г. Минска",
                        GeneralInformation = "33-я городская студенческая поликлиника г. Минска оказывает амбулаторную помощь иногородним студентам государственных и коммерческих ВУЗов. В учреждениях образования функционирует 23 фельдшерских здравпункта, на которых оказывается скорая и неотложная медицинская помощь, проводится прием больных и противоэпидемические мероприятия. В поликлинике работает аптека.\r\n\r\nНа платной основе в поликлинике оказывается медицинская помощь иностранным гражданам, обучающимся в учреждениях образования г. Минска.",
                    },

                    new Polyclinic
                    {
                        Address = "г. Гродно, ул. Кленовая, д. 25",
                        Title = "5-я городская поликлиника г. Гродно",
                        GeneralInformation = "test info",
                    }
                });

                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedDepartmentsAsync(DocTicketDBContext context)
        {
            if (!context.Departments.Any())
            {
                await context.Departments.AddRangeAsync(new List<Department>
                {
                    new Department { Name = "Терапевтическое", PolyclinicId = 1 },
                    new Department { Name = "Cтоматологическое", PolyclinicId = 1 },
                    new Department { Name = "Хирургическое", PolyclinicId = 1 },
                    new Department { Name = "Терапевтическое", PolyclinicId = 2 },
                    new Department { Name = "Cтоматологическое", PolyclinicId = 2 },
                    new Department { Name = "Хирургическое", PolyclinicId = 2 },
                });

                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedDoctorsAsync(DocTicketDBContext context)
        {
            if (!context.Doctors.Any())
            {
                await context.Doctors.AddRangeAsync(new List<Doctor>
                {
                    new Doctor
                    {
                        FirstName = "Антон",
                        LastName = "Иванов",
                        Patronymic = "Иванович",
                        Specialization = "Врач общей практики",
                        DepartmentId = 1,
                    },

                    new Doctor
                    {
                        FirstName = "Иван",
                        LastName = "Смирнов",
                        Patronymic = "Иванович",
                        Specialization = "Хирург",
                        DepartmentId = 2,
                    },

                    new Doctor
                    {
                        FirstName = "Александр",
                        LastName = "Кузнецов",
                        Patronymic = "Александрович",
                        Specialization = "Стоматолог",
                        DepartmentId = 3,
                    },

                    new Doctor
                    {
                        FirstName = "Дмитрий",
                        LastName = "Попов",
                        Patronymic = "Дмитриевич",
                        Specialization = "Врач общей практики",
                        DepartmentId = 4,
                    },

                    new Doctor
                    {
                        FirstName = "Иван",
                        LastName = "Васильев",
                        Patronymic = "Иванович",
                        Specialization = "Хирург",
                        DepartmentId = 5,
                    },

                    new Doctor
                    {
                        FirstName = "Владислав",
                        LastName = "Васильев",
                        Patronymic = "Владиславович",
                        Specialization = "Стоматолог",
                        DepartmentId = 6,   
                    },
                });

                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedTicketsAsync(DocTicketDBContext context)
        {
            if (!context.Tickets.Any())
            {
                await context.Tickets.AddRangeAsync(new List<Ticket>
                {
                    new Ticket { ReceptionTime = DateTime.Now, DoctorId = 1 },
                    new Ticket { ReceptionTime = DateTime.Now.AddMinutes(10), DoctorId = 1 },
                    new Ticket { ReceptionTime = DateTime.Now.AddMinutes(20), DoctorId = 1 },
                    new Ticket { ReceptionTime = DateTime.Now.AddDays(1), DoctorId = 1 },
                    new Ticket { ReceptionTime = DateTime.Now.AddDays(2), DoctorId = 1 },
                    new Ticket { ReceptionTime = DateTime.Now, DoctorId = 2 },
                    new Ticket { ReceptionTime = DateTime.Now.AddMinutes(10), DoctorId = 2 },
                    new Ticket { ReceptionTime = DateTime.Now.AddMinutes(20), DoctorId = 2 },
                    new Ticket { ReceptionTime = DateTime.Now.AddDays(1), DoctorId = 2 },
                    new Ticket { ReceptionTime = DateTime.Now.AddDays(2), DoctorId = 2 },
                    new Ticket { ReceptionTime = DateTime.Now, DoctorId = 3 },
                    new Ticket { ReceptionTime = DateTime.Now.AddMinutes(10), DoctorId = 3 },
                    new Ticket { ReceptionTime = DateTime.Now.AddMinutes(20), DoctorId = 3 },
                    new Ticket { ReceptionTime = DateTime.Now.AddDays(1), DoctorId = 3 },
                    new Ticket { ReceptionTime = DateTime.Now.AddDays(2), DoctorId = 3 },
                    new Ticket { ReceptionTime = DateTime.Now, DoctorId = 4 },
                    new Ticket { ReceptionTime = DateTime.Now.AddMinutes(10), DoctorId = 4 },
                    new Ticket { ReceptionTime = DateTime.Now.AddMinutes(20), DoctorId = 4 },
                    new Ticket { ReceptionTime = DateTime.Now.AddDays(1), DoctorId = 4 },
                    new Ticket { ReceptionTime = DateTime.Now.AddDays(2), DoctorId = 4 },
                    new Ticket { ReceptionTime = DateTime.Now, DoctorId = 5 },
                    new Ticket { ReceptionTime = DateTime.Now.AddMinutes(10), DoctorId = 5 },
                    new Ticket { ReceptionTime = DateTime.Now.AddMinutes(20), DoctorId = 5 },
                    new Ticket { ReceptionTime = DateTime.Now.AddDays(1), DoctorId = 5 },
                    new Ticket { ReceptionTime = DateTime.Now.AddDays(2), DoctorId = 5 },
                    new Ticket { ReceptionTime = DateTime.Now, DoctorId = 6 },
                    new Ticket { ReceptionTime = DateTime.Now.AddMinutes(10), DoctorId = 6 },
                    new Ticket { ReceptionTime = DateTime.Now.AddMinutes(20), DoctorId = 6 },
                    new Ticket { ReceptionTime = DateTime.Now.AddDays(1), DoctorId = 6 },
                    new Ticket { ReceptionTime = DateTime.Now.AddDays(2), DoctorId = 6 },
                });

                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedAccountsAsync(DocTicketDBContext context, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new IdentityRole("User"));

            const string adminEmail = "admin@docticket.com";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var newAdmin = new AppUser()
                {
                    FirstName = "Антон",
                    LastName = "Мещеня",
                    Patronymic = "Николаевич",
                    Email = adminEmail,
                    EmailConfirmed = true,
                    UserName = "admin-user",
                };

                var result = await userManager.CreateAsync(newAdmin, "Coding@1234!");
                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }

            const string userEmail = "meshchenya@mail.ru";
            var user = await userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                var newUser = new AppUser
                {
                    FirstName = "Антон",
                    LastName = "Мещеня",
                    Patronymic = "Николаевич",
                    Email = userEmail,
                    EmailConfirmed = true,
                    UserName = "app-user"
                };

                await userManager.CreateAsync(newUser, "Coding@1234?");
                await userManager.AddToRoleAsync(newUser, "User");
                await context.SaveChangesAsync();
            }
        }
    }
}
