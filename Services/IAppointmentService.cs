using Microsoft.AspNetCore.Mvc.Rendering;
using SchedulerApp.Models.ViewModels;
using System.Collections.Generic;

namespace SchedulerApp.Services
{
    public interface IAppointmentService
    {
        public List<DoctorVM> GetDoctorsList();
        public List<PatientVM> GetPatientsList();
        public IEnumerable<SelectListItem> GetDoctorsSelectListItem();
        public IEnumerable<SelectListItem> GetPatientsSelectListItem();

    }
}
