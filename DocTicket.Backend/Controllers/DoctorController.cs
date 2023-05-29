using DocTicket.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocTicket.Backend.Controllers
{
    public class DoctorController : Controller
    {
        private readonly DoctorService _doctorService;


        public DoctorController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public async Task<IActionResult> List(int? id = null)
        {
            var doctors = await _doctorService.GetAllAsync(id);

            return View(doctors);
        }

        public async Task<IActionResult> Details(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);

            if (doctor == null)
                return NotFound();

            return View(doctor);
        }
    }
}
