using Microsoft.AspNetCore.Mvc.Rendering;
using SchedulerApp.Models;
using SchedulerApp.Models.ViewModels;
using SchedulerApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
