using Microsoft.AspNetCore.Mvc.Rendering;
using SchedulerApp.Models;
using SchedulerApp.Models.ViewModels;
using SchedulerApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _db;
        public AppointmentService(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<DoctorVM> GetDoctorsList()
        {
            var doctors = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Helper.Doctor) on userRoles.RoleId equals roles.Id
                           select new DoctorVM
                           {
                               Id = user.Id,
                               Name = user.Name
                           }
                           ).ToList();

            return doctors;
        }

        public List<PatientVM> GetPatientsList()
        {
            var patients = (from user in _db.Users
                            join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                            join roles in _db.Roles.Where(x => x.Name == Helper.Patient) on userRoles.RoleId equals roles.Id
                            select new PatientVM
                            {
                                Id = user.Id,
                                Name = user.Name
                            }
                           ).ToList();

            return patients;
        }


        public IEnumerable<SelectListItem> GetDoctorsSelectListItem()
        {
            //var doctors = _db.Users.Select(user => new SelectListItem
            //{
            //    Text = user.Name,
            //    Value = user.Id
            //}).Where();

            var doctors = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Helper.Doctor) on userRoles.RoleId equals roles.Id
                           select new SelectListItem
                           {
                               Text = new StringBuilder(new String(user.Name + " " + roles.Name)).ToString(),
                               Value = user.Id
                           }
                           ).ToList();

            return doctors;
        }

        public IEnumerable<SelectListItem> GetPatientsSelectListItem()
        {
            var patients = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Helper.Doctor) on userRoles.RoleId equals roles.Id
                           select new SelectListItem
                           {
                               Text = new StringBuilder(new String(user.Name + " " + roles.Name)).ToString(),
                               Value = user.Id
                           }
                           ).ToList();

            return patients;
        }

        public async Task<int> AddOrUpdate(AppointmentViewModel appointmentVM)
        {
            var startDate = DateTime.Parse(appointmentVM.StartDate);
            var endDate = DateTime.Parse(appointmentVM.StartDate).AddMinutes(Convert.ToDouble(appointmentVM.Duration));

            if (appointmentVM != null && appointmentVM.Id > 0)
            {
                // UPDATE
                return 1;
            }
            else
            {
                // CREATE
                Appointment appointment = new Appointment()
                {
                    Title = appointmentVM.Title,
                    Description = appointmentVM.Description,
                    StartDate = startDate,
                    EndDate = endDate,
                    Duration = appointmentVM.Duration,
                    DoctorId = appointmentVM.DoctorId,
                    PatientId = appointmentVM.PatientId,
                    IsDoctorApproved = false,
                    AdminId = appointmentVM.AdminId,
                };

                _db.Appointments.Add(appointment);
                await _db.SaveChangesAsync();
                return 2;
            }
        }

        public List<AppointmentViewModel> DoctorsEventsById(string doctorId)
        {
            return _db.Appointments.Where(x => x.DoctorId == doctorId).ToList().Select(c => new AppointmentViewModel()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorApproved = c.IsDoctorApproved
            }).ToList();
        }

        public AppointmentViewModel GetById(int id)
        {
            return _db.Appointments.Where(x => x.Id == id).ToList().Select(c => new AppointmentViewModel()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorApproved = c.IsDoctorApproved,
                PatientId = c.PatientId,
                DoctorId = c.DoctorId,
                PatientName = _db.Users.Where(x => x.Id == c.PatientId).Select(x => x.Name).FirstOrDefault(),
                DoctorName = _db.Users.Where(x => x.Id == c.DoctorId).Select(x => x.Name).FirstOrDefault(),
            }).SingleOrDefault();
        }

        public List<AppointmentViewModel> PatientsEventsById(string patientId)
        {
            return _db.Appointments.Where(x => x.PatientId == patientId).ToList().Select(c => new AppointmentViewModel()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorApproved = c.IsDoctorApproved
            }).ToList();
        }

        //public List<AppointmentViewModel> DoctorsEventsById(string doctorId)
        //{
        //    var doctorEvents = (from appointments in _db.Appointments.Where(x => x.DoctorId == doctorId).ToList()
        //                        select new AppointmentViewModel
        //                        {
        //                            Id = appointments.Id,
        //                            Title = appointments.Title,
        //                            Description = appointments.Description,
        //                            StartDate = appointments.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
        //                            EndDate = appointments.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
        //                            Duration = appointments.Duration,
        //                            IsDoctorApproved = appointments.IsDoctorApproved
        //                        }
        //       ).ToList();

        //    return doctorEvents;
        //}

        //public List<AppointmentViewModel> PatientsEventsById(string patientId)
        //{
        //    var patientEvents = (from appointments in _db.Appointments.Where(x => x.PatientId == patientId).ToList()
        //                        select new AppointmentViewModel
        //                        {
        //                            Id = appointments.Id,
        //                            Title = appointments.Title,
        //                            Description = appointments.Description,
        //                            StartDate = appointments.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
        //                            EndDate = appointments.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
        //                            Duration = appointments.Duration,
        //                            IsDoctorApproved = appointments.IsDoctorApproved
        //                        }
        //       ).ToList();

        //    return patientEvents;
        //}

        //public AppointmentViewModel EventById(int eventId)
        //{
        //    var eventData = _db.Appointments.Where(x => x.Id == eventId).Select(appointmentData => new AppointmentViewModel()
        //    {
        //        Id = appointmentData.Id,
        //        Title = appointmentData.Title,
        //        Description = appointmentData.Description,
        //        StartDate = appointmentData.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
        //        EndDate = appointmentData.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
        //        Duration = appointmentData.Duration,
        //        IsDoctorApproved = appointmentData.IsDoctorApproved,
        //        PatientId = appointmentData.PatientId,
        //        DoctorId = appointmentData.DoctorId,
        //        PatientName = _db.Users.Where(x => x.Id == appointmentData.PatientId).Select(u => u.Name).FirstOrDefault(),
        //        DoctorName = _db.Users.Where(x => x.Id == appointmentData.DoctorId).Select(u => u.Name).FirstOrDefault(),
        //    }).SingleOrDefault();

        //    return eventData;
        //}
    }
}
