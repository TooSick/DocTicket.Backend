using AutoMapper;
using DocTicket.Backend.BusinessLogic.ViewModels.PolyclinicViewModels;
using DocTicket.Backend.DataAccess.EF;
using DocTicket.Backend.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DocTicket.Backend.BusinessLogic.Services
{
    public class PolyclinicService
    {
        private readonly DocTicketDBContext _dbContext;
        private readonly Mapper _mapper;


        public PolyclinicService(DocTicketDBContext dbContext, Mapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public IEnumerable<PolyclinicViewModel> GetAllPoliclinics()
        {
            var pollinics = _dbContext.Polyclinics
                .Include(p => p.RegistryNumbers)
                .Include(p => p.WorkingHours)
                .OrderBy(p => p.Title);

            return _mapper.Map<IEnumerable<Polyclinic>, IEnumerable<PolyclinicViewModel>>(pollinics);
        }
    }
}
