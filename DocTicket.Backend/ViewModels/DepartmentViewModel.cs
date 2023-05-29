using AutoMapper;
using DocTicket.Backend.Common.Mappings;
using DocTicket.Backend.Models;

namespace DocTicket.Backend.ViewModels
{
    public class DepartmentViewModel : BaseViewModel, IMapWith
    {
        public string Name { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(Department), GetType());
        }
    }
}
