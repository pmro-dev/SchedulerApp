using Microsoft.AspNetCore.Mvc.Rendering;
using SchedulerApp.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchedulerApp.Services
{
    public interface IAppointmentService
    {
        public List<DoctorVM> GetDoctorsList();
        public List<PatientVM> GetPatientsList();
        public IEnumerable<SelectListItem> GetDoctorsSelectListItem();
        public IEnumerable<SelectListItem> GetPatientsSelectListItem();

        public Task<int> AddOrUpdate(AppointmentViewModel appointmentVM);

        public List<AppointmentViewModel> DoctorsEventsById(string doctorId);
        public List<AppointmentViewModel> PatientsEventsById(string patientId);

    }
}
