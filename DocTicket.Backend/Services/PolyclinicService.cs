using AutoMapper;
using DocTicket.Backend.EF;
using DocTicket.Backend.Models;
using DocTicket.Backend.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DocTicket.Backend.Services
{
    public class PolyclinicService
    {
        private readonly DocTicketDBContext _dbContext;
        private readonly IMapper _mapper;


        public PolyclinicService(DocTicketDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ShortPolyclinicInfoViewModel>> GetAllPoliclinics()
        {
            var polinics = await _dbContext.Polyclinics
                .Include(p => p.RegistryNumbers)
                .Include(p => p.WorkingHours)
                .OrderBy(p => p.Title).ToListAsync();

            return _mapper.Map<IEnumerable<Polyclinic>, IEnumerable<ShortPolyclinicInfoViewModel>>(polinics);
        }

        public async Task<Polyclinic> GetById(int id)
        {
            return await _dbContext.Polyclinics
                .Include(p => p.RegistryNumbers)
                .Include(p => p.WorkingHours)
                .Include(p => p.Departments)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetPolyclinicDepartments(int id)
        {
            var departments = await _dbContext.Departments
                .Where(p => p.PolyclinicId == id)
                .ToListAsync();

            return _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
        }
    }
}
