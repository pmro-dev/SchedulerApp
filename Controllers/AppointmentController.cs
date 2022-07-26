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
           //ViewBag.DoctorsList =  _appointmentService.GetDoctorsList();
           ViewBag.DoctorsList =  _appointmentService.GetDoctorsSelectListItem();
            return View();
        }
    }
}
