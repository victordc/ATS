using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Data.Model;
using ATS.Data;
using ATS.Data.DAL;
using System.IO;
using ATS.MVC.UI.Helpers;
using ATS.MVC.UI.Common;
using ATS.Framework;
using System.Globalization;
using AutoMapper;
using ATS.Data.ViewModel;

namespace ATS.MVC.UI.Controllers
{
    public class TimeSheetController : BaseController
    {

        public ActionResult Reminder()
        {
            var timeSheetMasters = TimeSheetMasterRepository.GetAllTimeSheetMastersByAgentId(UserSetting.Current.PersonId);
            //var timeSheetMasters = TimeSheetMasterRepository.GetAllTimeSheetMasters();

            return View(timeSheetMasters.ToList());
        }

        public ActionResult SendReminder(int id)
        {
            //do something for the to address
            TimeSheetMaster master = TimeSheetMasterRepository.GetTimeSheetMasterById(id);
            Person person = null;
            string subject = "";
            string message = "";

            if (master.Status == Convert.ToInt32(TimeSheetStatus.Draft))
            {
                subject = "Reminder to submit timesheet for month of " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(master.Month);
                message = "Please submit your timesheet to your supervisor as soon as possible. From your friendly agent";
                person = TimesheetRepository.GetPersonById(master.PersonId);
            }
            else if (master.Status == Convert.ToInt32(TimeSheetStatus.Submitted))
            {
                subject = "Reminder to approve timesheet for month of " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(master.Month);
                message = "Please evaluate the timesheet from your staff as soon as possible. From your friendly agent";
                person = TimesheetRepository.GetPersonById(master.Supervisor.PersonId);
            }
            //send email
            if (person != null)
            {
                EmailManager.SendReminder("nusissdotnetagent01@gmail.com", "nusissdotnet", person.Email, master.Agent.Email, subject, message);
            }
            return RedirectToAction("Reminder");
        }

        public ActionResult SupervisorEdit()
        {
            var timeSheetMasters = TimeSheetMasterRepository.GetAllTimeSheetMastersBySupervisorId(UserSetting.Current.PersonId);
            //var timeSheetMasters = TimeSheetMasterRepository.GetAllTimeSheetMasters();

            return View(timeSheetMasters.ToList());
        }

        public ActionResult StaffDetails(int id)
        {
            TimeSheetMaster master = TimeSheetMasterRepository.GetTimeSheetMasterById(id);
            ViewBag.StatusList = TimeSheetMasterRepository.GetStatusList();

            foreach (var item in master.TimeSheetDetail)
            {
                item.LeaveCategories = new SelectList(TimesheetRepository.GetLeaveCategories(), "LeaveCategoryId", "LeaveCategoryDesc", item.LeaveCategoryId);
            }

            TimeSheetMasterViewModel viewModel = Mapper.Map<TimeSheetMaster, TimeSheetMasterViewModel>(master);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult StaffDetails(TimeSheetMasterViewModel viewModel)
        {
            if (Request.Form["approve"] != null)
            {
                viewModel.Status = Convert.ToInt32(TimeSheetStatus.Approved);
            }
            else if (Request.Form["reject"] != null)
            {
                viewModel.Status = Convert.ToInt32(TimeSheetStatus.Rejected);
            }

            viewModel.Remarks = string.IsNullOrEmpty(viewModel.Remarks) ? string.Empty : viewModel.Remarks;

            TimeSheetMaster master = Mapper.Map<TimeSheetMasterViewModel, TimeSheetMaster>(viewModel);

            Person person = TimesheetRepository.GetPersonById(master.PersonId);
            Person agent = TimesheetRepository.GetPersonById((int)master.AgencyId);
            string subject = "";
            string message = "";

            if (master.Status == Convert.ToInt32(TimeSheetStatus.Approved))
            {
                subject = "Timesheet for month of " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(master.Month) + " Approved";
                message = "Please note that your timesheet has been approved by your supervisor.";
            }
            else if (master.Status == Convert.ToInt32(TimeSheetStatus.Rejected))
            {
                subject = "Timesheet for month of " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(master.Month) + " Rejected";
                message = "Please note that your timesheet has been rejected by your supervisor.";
            }
            //save changes
            master.SaveMasterOnly();

            //send email
            if (person != null)
            {
                EmailManager.SendReminder("nusissdotnetagent01@gmail.com", "nusissdotnet", person.Email, agent.Email, subject, message);
            }

            return RedirectToAction("SupervisorEdit");
        }

        //
        // GET: /TimeSheet/

        public ActionResult Index()
        {
            var timeSheetMasters = TimeSheetMasterRepository.GetAllTimeSheetsByPersonId(UserSetting.Current.PersonId);
            return View(timeSheetMasters.ToList().OrderByDescending(x => x.Month));
        }

        //
        // GET: /TimeSheet/Details/5

        public ActionResult Details(int id)
        {
            TimeSheetMaster master = TimeSheetMasterRepository.GetTimeSheetMasterById(id);
            foreach (var item in master.TimeSheetDetail)
            {
                item.LeaveCategories = new SelectList(TimesheetRepository.GetLeaveCategories(), "LeaveCategoryId", "LeaveCategoryDesc", item.LeaveCategoryId);
            }

            TimeSheetMasterViewModel viewModel = Mapper.Map<TimeSheetMaster, TimeSheetMasterViewModel>(master);
            return View(viewModel);
        }

        
        [HttpGet]
        public ActionResult Create(int year = 0, int month = 0)
        {
            DateTime currentDateTime;
            if(year == 0 && month == 0)
            {
                currentDateTime = DateTime.Today;
            }
            else
            {
                currentDateTime = new DateTime(year, month, 1);
            }
             
            PersonRepository personRepository = new PersonRepository(new ATSCEEntities());
            Staff staff = personRepository.GetStaffByID(UserSetting.Current.PersonId);
            TimeSheetMaster master = TimeSheetMasterRepository.CreateTimeSheetMasterTemplate(currentDateTime, staff);
            ViewBag.LeaveCategories = TimeSheetMasterRepository.GetLeaveCategoriesList();
            ViewBag.StatusList = TimeSheetMasterRepository.GetStatusList();
            ViewBag.YearList = TimeSheetMasterRepository.GetYearList();
            ViewBag.MonthList = TimeSheetMasterRepository.GetMonthList();
            TimeSheetMasterViewModel viewModel = Mapper.Map<TimeSheetMaster, TimeSheetMasterViewModel>(master);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(TimeSheetMasterViewModel masterViewModel)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form["save"] != null)
                {
                    masterViewModel.Status = Convert.ToInt32(TimeSheetStatus.Draft);
                }
                else if (Request.Form["submit"] != null)
                {
                    masterViewModel.Status = Convert.ToInt32(TimeSheetStatus.Submitted);
                }
                TimeSheetMaster master = Mapper.Map<TimeSheetMasterViewModel, TimeSheetMaster>(masterViewModel);

                if (master.ValidateWhenCreate())
                {
                    if(master.ValidateDate())
                    {
                        bool isValid = true;
                        foreach(TimeSheetDetail detail in master.TimeSheetDetail)
                        {
                            if(!detail.ValidateTime())
                            {
                                isValid = false;
                                string error = "The start time is later than the end time for " + detail.StartTime.Date.ToString("dd/MM/yyyy");
                                ModelState.AddModelError("", error);
                            }
                        }
                        if(isValid)
                        {
                            SaveTimeSheet(master);
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Timesheet must only be created for the past 3 months");
                    }
                }
                else
                    ModelState.AddModelError("", "The timesheet has already been created for this month.");
            }
            else
                ModelState.AddModelError("", "Errors in the details. Please correct and resubmit.");

            ViewBag.LeaveCategories = TimeSheetMasterRepository.GetLeaveCategoriesList();
            ViewBag.StatusList = TimeSheetMasterRepository.GetStatusList();
            ViewBag.YearList = TimeSheetMasterRepository.GetYearList();
            ViewBag.MonthList = TimeSheetMasterRepository.GetMonthList();
            return View(masterViewModel);
        }


        public void SaveTimeSheet(TimeSheetMaster master)
        {
            PersonRepository personRepository = new PersonRepository(new ATSCEEntities());

            master.Remarks = string.IsNullOrEmpty(master.Remarks) ? string.Empty : master.Remarks;
            foreach (var item in master.TimeSheetDetail)
            {
                var uploadDir = "~/uploads";
                var filePath = string.Empty;
                var fileUrl = string.Empty;


                if (item.SupportDocumentUpload1 != null && item.SupportDocumentUpload1.ContentLength >= 0)
                {
                    filePath = Path.Combine(Server.MapPath(uploadDir), item.SupportDocumentUpload1.FileName);
                    fileUrl = Path.Combine(uploadDir, item.SupportDocumentUpload1.FileName);
                    item.SupportDocumentUpload1.SaveAs(filePath);
                    item.SupportDocument1 = fileUrl;

                    filePath = string.Empty;
                    fileUrl = string.Empty;
                }

                if (item.SupportDocumentUpload2 != null && item.SupportDocumentUpload2.ContentLength >= 0)
                {
                    filePath = Path.Combine(Server.MapPath(uploadDir), item.SupportDocumentUpload2.FileName);
                    fileUrl = Path.Combine(uploadDir, item.SupportDocumentUpload2.FileName);
                    item.SupportDocumentUpload2.SaveAs(filePath);
                    item.SupportDocument2 = fileUrl;

                    filePath = string.Empty;
                    fileUrl = string.Empty;
                }

                if (item.SupportDocumentUpload3 != null && item.SupportDocumentUpload3.ContentLength >= 0)
                {
                    filePath = Path.Combine(Server.MapPath(uploadDir), item.SupportDocumentUpload3.FileName);
                    fileUrl = Path.Combine(uploadDir, item.SupportDocumentUpload3.FileName);
                    item.SupportDocumentUpload3.SaveAs(filePath);
                    item.SupportDocument3 = fileUrl;

                    filePath = string.Empty;
                    fileUrl = string.Empty;
                }
            }

            master.Save();
        }

        //
        // GET: /TimeSheet/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TimeSheetMaster master = TimeSheetMasterRepository.GetTimeSheetMasterById(id);
            TimeSheetMasterViewModel viewModel = Mapper.Map<TimeSheetMaster, TimeSheetMasterViewModel>(master);
            ViewBag.StatusList = TimeSheetMasterRepository.GetStatusList();
            ViewBag.LeaveCategories = TimeSheetMasterRepository.GetLeaveCategoriesList();
            return View(viewModel);
        }

        //
        // POST: /TimeSheet/Edit/5

        [HttpPost]
        public ActionResult Edit(TimeSheetMasterViewModel masterViewModel)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form["save"] != null)
                {
                    masterViewModel.Status = Convert.ToInt32(TimeSheetStatus.Draft);
                }
                else if (Request.Form["submit"] != null)
                {
                    masterViewModel.Status = Convert.ToInt32(TimeSheetStatus.Submitted);
                }
                TimeSheetMaster master = Mapper.Map<TimeSheetMasterViewModel, TimeSheetMaster>(masterViewModel);

                bool isValid = true;
                foreach (TimeSheetDetail detail in master.TimeSheetDetail)
                {
                    if (!detail.ValidateTime())
                    {
                        isValid = false;
                        string error = "The start time is later than the end time for " + detail.StartTime.Date.ToString();
                        ModelState.AddModelError("", error);
                    }
                }
                if (isValid)
                {
                    SaveTimeSheet(master);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.LeaveCategories = TimeSheetMasterRepository.GetLeaveCategoriesList();
            ViewBag.StatusList = TimeSheetMasterRepository.GetStatusList();
            return View(masterViewModel);
        }

        //
        // GET: /TimeSheet/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TimeSheetMaster master = TimeSheetMasterRepository.GetTimeSheetMasterById(id);
            ViewBag.StatusList = TimeSheetMasterRepository.GetStatusList();

            foreach (var item in master.TimeSheetDetail)
            {
                item.LeaveCategories = new SelectList(TimesheetRepository.GetLeaveCategories(), "LeaveCategoryId", "LeaveCategoryDesc", item.LeaveCategoryId);
            }

            TimeSheetMasterViewModel viewModel = Mapper.Map<TimeSheetMaster, TimeSheetMasterViewModel>(master);
            return View(viewModel);
        }

        //
        // POST: /TimeSheet/Delete/5

        [HttpPost]
        public ActionResult Delete(TimeSheetMaster master)
        {
            if (ModelState.IsValid)
            {
                TimeSheetMaster.Delete(master.TimeSheetMasterId);
                return RedirectToAction("Index");
            }
            return View(master);

        }
    }
}
