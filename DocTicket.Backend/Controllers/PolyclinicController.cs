using DocTicket.Backend.Services;
using DocTicket.Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DocTicket.Backend.Controllers
{
    public class PolyclinicController : Controller
    {
        private readonly PolyclinicService _polyclinicService;


        public PolyclinicController(PolyclinicService polyclinicService)
        {
            _polyclinicService = polyclinicService;
        }

        public async Task<ViewResult> List()
        {
            var polyclinics = await _polyclinicService.GetAllPoliclinics();
            return View(polyclinics);
        }

        public async Task<IActionResult> Details(int id)
        {
            var polyclinic = await _polyclinicService.GetById(id);

            if (polyclinic == null)
                return NotFound();

            return View(polyclinic);
        }

        public async Task<IActionResult> Departments(int id)
        {
            var departments = await _polyclinicService.GetPolyclinicDepartments(id);

            return View(departments);
        }

        public ViewResult Create() => View("Edit");  
    }
}
