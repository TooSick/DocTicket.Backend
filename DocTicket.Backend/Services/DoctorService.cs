using AutoMapper;
using DocTicket.Backend.EF;
using DocTicket.Backend.Models;
using DocTicket.Backend.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Security.Claims;

namespace DocTicket.Backend.Services
{
    public class DoctorService
    {
        private readonly DocTicketDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public DoctorService(DocTicketDBContext dbContext, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<IEnumerable<ShortDoctorInfoViewModel>> GetAllAsync(int? departmentId)
        {
            List<Doctor> doctors = await _dbContext.Doctors.ToListAsync();

            if (departmentId == null)
            {
                var doctorViewModels = _mapper.Map<IEnumerable<Doctor>, IEnumerable<ShortDoctorInfoViewModel>>(doctors).ToList();

                await Task.Run(() =>
                {
                    for (int i = 0; i < doctors.Count; i++)
                    {
                        doctorViewModels[i].PolyclinicName = _dbContext.Departments.Include(dep => dep.Polyclinic)
                            .First(dep => dep.Id == doctors[i].DepartmentId).Polyclinic.Title;
                    }
                });

                return doctorViewModels;
            }

            return _mapper.Map<IEnumerable<Doctor>, IEnumerable<ShortDoctorInfoViewModel>>(doctors.Where(d => d.DepartmentId == departmentId));
        }

        public async Task<DoctorViewModel> GetByIdAsync(int id)
        {
            var doctor = await _dbContext.Doctors
                .Include(d => d.Tickets).ThenInclude(t => t.Offers)
                .Include(d => d.Department).ThenInclude(d => d.Polyclinic)
                .FirstOrDefaultAsync(d => d.Id == id);
            var doctorViewModel = _mapper.Map<Doctor, DoctorViewModel>(doctor);

            doctorViewModel.PolyclinicName = doctor.Department.Polyclinic.Title;

            var cultureInfo = new CultureInfo("ru-Ru");
            string userId = string.Empty;


            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var user = await _dbContext.AppUsers.Include(u => u.Tickets).Include(u => u.Offers)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var offer = await _dbContext.Offers.Include(o => o.AppUsers).Include(o => o.Tickets)
                .FirstOrDefaultAsync(o => o.AppUsers.Any(u => u.Id == userId));

            await Task.Factory.StartNew(() =>
            {
                foreach (var ticket in doctor.Tickets)
                {
                    string key = $"{ticket.ReceptionTime.ToString("M", cultureInfo)}";
                    var ticketViewModel = _mapper.Map<Ticket, TicketViewModel>(ticket);
                    bool userHasOfferForTicket = ticket.Offers.Any(o => o.AppUsers.Any(u => u.Id == userId));

                    ticketViewModel.CanUserOrderTicket = true;

                    if (ticket.AppUserId != null && !userId.IsNullOrEmpty() && ticket.AppUserId != userId
                        && user.Tickets.Any(t => t.ReceptionTime.ToString("M", cultureInfo) == ticket.ReceptionTime.ToString("M", cultureInfo)
                            && t.DoctorId == doctor.Id) && !userHasOfferForTicket)
                    {
                        ticketViewModel.CanUserSendOffer = true;
                    }

                    if (ticket.AppUserId != null || (user?.Tickets.Any(t => t.ReceptionTime.ToString("M", cultureInfo) == ticket.ReceptionTime.ToString("M", cultureInfo)
                        && t.DoctorId == doctor.Id) ?? false))
                        ticketViewModel.CanUserOrderTicket = false;

                    if (doctorViewModel.Tickets.ContainsKey(key))
                    {
                        doctorViewModel.Tickets[key].Add(ticketViewModel);
                    }
                    else if (ticket.ReceptionTime.Month == DateTime.Now.Month)
                    {
                        doctorViewModel.Tickets.Add(key, new List<TicketViewModel> { ticketViewModel });
                    }
                }
            });

            return doctorViewModel;
        }
    }
}
