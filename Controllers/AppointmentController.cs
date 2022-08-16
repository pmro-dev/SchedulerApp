using Microsoft.AspNetCore.Mvc;
using SchedulerApp.Services;
using SchedulerApp.Utility;

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
            ViewBag.DoctorsList = _appointmentService.GetDoctorsList();
            ViewBag.PatientsList = _appointmentService.GetPatientsList();
            ViewBag.Duration = Helper.GetTimeDropDown();

            return View();
        }
    }
}
