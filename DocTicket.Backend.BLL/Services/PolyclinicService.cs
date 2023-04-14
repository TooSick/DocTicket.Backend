using AutoMapper;
using DocTicket.Backend.BLL.DTOs.PolyclinicDTOs;
using DocTicket.Backend.DAL.EF;
using DocTicket.Backend.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DocTicket.Backend.BLL.Services
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


        public IEnumerable<PolyclinicDTO> GetAllPoliclinics()
        {
            var pollinics = _dbContext.Polyclinics
                .Include(p => p.RegistryNumbers)
                .Include(p => p.WorkingHours)
                .OrderBy(p => p.Title);

            return _mapper.Map<IEnumerable<Polyclinic>, IEnumerable<PolyclinicDTO>>(pollinics);
        }
    }
}
