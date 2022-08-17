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

        public Task<int> AddOrUpdate(AppointmentViewModel appointmentVM);

        public List<AppointmentViewModel> PatientsEventsById(string patientId);
        public AppointmentViewModel GetById(int id);
        public List<AppointmentViewModel> DoctorsEventsById(string doctorId);

        public Task<int> ConfirmEvent(int appointmentId);
        public Task<int> DeleteEvent(int appointmentId);

        //public List<AppointmentViewModel> DoctorsEventsById(string doctorId);
        //public List<AppointmentViewModel> PatientsEventsById(string patientId);
        //public AppointmentViewModel EventById(int eventId);

    }
}
