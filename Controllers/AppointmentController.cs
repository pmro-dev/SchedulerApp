using Microsoft.AspNetCore.Mvc;
using SchedulerApp.Services;

namespace SchedulerApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        public IActionResult Index()
        {
            _appointmentService.GetDoctorsList();
            return View();
        }
    }
}
