using Microsoft.AspNetCore.Mvc;
using SchedulerApp.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using SchedulerApp.Models.ViewModels;
using SchedulerApp.Utility;
using System;
using System.Collections.Generic;

namespace SchedulerApp.Controllers.Api
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentApiController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string loginUserId;
        private readonly string role;

        public AppointmentApiController(IAppointmentService appointmentService, IHttpContextAccessor httpContextAccessor)
        {
            _appointmentService = appointmentService;
            _httpContextAccessor = httpContextAccessor;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData(AppointmentViewModel data)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();

            try
            {
                commonResponse.status = _appointmentService.AddOrUpdate(data).Result;
                if (commonResponse.status == 1)
                {
                    commonResponse.message = Helper.appointmentUpdated;
                }
                if (commonResponse.status == 2)
                {
                    commonResponse.message = Helper.appointmentAdded;
                }

            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }
            return View();
        }

        [HttpGet]
        [Route("GetCalendarData")]
        public IActionResult GetCalendarData(string userId)
        {
            CommonResponse <List<AppointmentViewModel>> commonResponse = new CommonResponse <List<AppointmentViewModel>>();

            try
            {
                if (role == Helper.Doctor)
                {
                    commonResponse.dataenum = _appointmentService.DoctorsEventsById(loginUserId);
                    commonResponse.status = Helper.success_code;
                }
                else if (role == Helper.Patient)
                {
                    commonResponse.dataenum = _appointmentService.PatientsEventsById(loginUserId);
                    commonResponse.status = Helper.success_code;
                }
                else
                {
                    commonResponse.dataenum = _appointmentService.DoctorsEventsById(userId);
                    commonResponse.status = Helper.success_code;
                }
            } 
            catch(Exception e)
            {
                commonResponse.status = Helper.failure_code;
                commonResponse.message = e.Message;
            }

            return Ok(commonResponse);
        }
    }
}
