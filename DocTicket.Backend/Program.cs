using DocTicket.Backend.Common.Mappings;
using DocTicket.Backend.EF;
using DocTicket.Backend.Models;
using DocTicket.Backend.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

const string ConnectionName = "DocTicketConnection";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
AddDbConfiguration();
AddAutoMapperConfiguration();
AddServices();
AddIdentity();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStatusCodePages();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

await DbInitializer.SeedDataAsync(app);

app.Run();


void AddDbConfiguration()
{
    string connectionString = builder.Configuration.GetConnectionString(ConnectionName);

    builder.Services.AddDbContext<DocTicketDBContext>(options
                => options.UseSqlServer(connectionString));
}

void AddAutoMapperConfiguration()
{
    builder.Services.AddAutoMapper(config =>
    {
        config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    });
}

void AddServices()
{
    builder.Services.AddTransient<PolyclinicService>();
    builder.Services.AddTransient<DoctorService>();
    builder.Services.AddTransient<TicketService>();
    builder.Services.AddTransient<OfferService>();
}

void AddIdentity()
{
    builder.Services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<DocTicketDBContext>()
                    .AddDefaultTokenProviders();

    builder.Services.AddMemoryCache();
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession(options =>
    {
        options.Cookie.Name = ".DocTicketWebApplication.Session";
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.IsEssential = true;
        options.Cookie.HttpOnly = true;
    });

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    });

    builder.Services.Configure<IdentityOptions>(options =>
    {
        // Password settings
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 7;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredUniqueChars = 6;
        options.Password.RequireUppercase = false;

        // Lockout settings
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
        options.Lockout.AllowedForNewUsers = true;

        // User settings
        options.User.RequireUniqueEmail = true;
    });

    builder.Services.ConfigureApplicationCookie(options =>
    {
        // Cookie settings
        options.Cookie.Name = ".DocTicketWebApplication";
        options.Cookie.IsEssential = true;
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        // If the LoginPath isn't set, ASP.NET Core defaults 
        // the path to /Account/Login.
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/LogOut";
    });

}