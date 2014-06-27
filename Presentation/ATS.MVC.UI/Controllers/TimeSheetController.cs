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
            string subject = "";
            string message = "";

            if (master.Status == Convert.ToInt32(TimeSheetStatus.Draft))
            {
                subject = "Reminder to submit timesheet for month of " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(master.Month);
                message = "Please submit your timesheet to your supervisor as soon as possible. From your friendly agent";
            }
            else if (master.Status == Convert.ToInt32(TimeSheetStatus.Submitted))
            {
                subject = "Reminder to approve timesheet for month of " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(master.Month);
                message = "Please evaluate the timesheet from your staff as soon as possible. From your friendly agent";
            }
            //send email
            EmailManager.SendReminder("nusissdotnetagent01@gmail.com", "nusissdotnet", "vinaykasireddy@gmail.com", subject, message);
            return RedirectToAction("Reminder");
        }

        public ActionResult SupervisorEdit()
        {
            //var timeSheetMasters = TimeSheetMasterRepository.GetAllTimeSheetMastersBySupervisorId(UserSetting.Current.PersonId);
            var timeSheetMasters = TimeSheetMasterRepository.GetAllTimeSheetMasters();

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
            if(Request.Form["approve"] != null)
            {
                viewModel.Status = Convert.ToInt32(TimeSheetStatus.Approved);
            }
            else if(Request.Form["reject"] != null)
            {
                viewModel.Status = Convert.ToInt32(TimeSheetStatus.Rejected);
            }

            viewModel.Remarks = string.IsNullOrEmpty(viewModel.Remarks) ? string.Empty : viewModel.Remarks;

            TimeSheetMaster master = Mapper.Map<TimeSheetMasterViewModel, TimeSheetMaster>(viewModel);
            master.SaveMasterOnly();
            return RedirectToAction("SupervisorEdit");
        }

        //
        // GET: /TimeSheet/

        public ActionResult Index()
        {
            var timeSheetMasters = TimeSheetMasterRepository.GetAllTimeSheetsByPersonId(UserSetting.Current.PersonId);
            //var timeSheetMasters = TimeSheetMasterRepository.GetAllTimeSheetMasters();
            return View(timeSheetMasters.ToList());
        }

        //
        // GET: /TimeSheet/Details/5

        public ActionResult Details(int id)
        {
            TimeSheetMaster master = TimeSheetMasterRepository.GetTimeSheetMasterById(id);
            var detail = master.TimeSheetDetail;
            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail.ToList());
        }

        //
        // GET: /TimeSheet/Create
        [HttpGet]
        public ActionResult Create()
        {
            PersonRepository personRepository = new PersonRepository(new ATSCEEntities());
            Staff staff = personRepository.GetStaffByID(UserSetting.Current.PersonId);
            TimeSheetMaster master = TimeSheetMasterRepository.CreateTimeSheetMasterTemplate(DateTime.Today, staff);
            ViewBag.StatusList = TimeSheetMasterRepository.GetStatusList();
            TimeSheetMasterViewModel viewModel = Mapper.Map<TimeSheetMaster, TimeSheetMasterViewModel>(master);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(TimeSheetMasterViewModel masterViewModel)
        {
            if (ModelState.IsValid)
            {
                masterViewModel.Status = Convert.ToInt32(TimeSheetStatus.Submitted);
                TimeSheetMaster master = Mapper.Map<TimeSheetMasterViewModel, TimeSheetMaster>(masterViewModel);
                if(master.ValidateWhenCreate())
                {
                    SaveTimeSheet(master);
                }
                return RedirectToAction("Index");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                
            }
            ViewBag.StatusList = TimeSheetMasterRepository.GetStatusList();
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
            ViewBag.StatusList = TimeSheetMasterRepository.GetStatusList();

            foreach (var item in master.TimeSheetDetail)
            {
                item.LeaveCategories = new SelectList(TimesheetRepository.GetLeaveCategories(), "LeaveCategoryId", "LeaveCategoryDesc", item.LeaveCategoryId);
            }

            TimeSheetMasterViewModel viewModel = Mapper.Map<TimeSheetMaster, TimeSheetMasterViewModel>(master);
            return View(viewModel);
        }

        //
        // POST: /TimeSheet/Edit/5

        [HttpPost]
        public ActionResult Edit(TimeSheetMaster master)
        {
            TimeSheetMaster newMaster = master;
            if (ModelState.IsValid)
            {
                SaveTimeSheet(master);
                return RedirectToAction("Index");
            }
            return View(master);
        }

        //
        // GET: /TimeSheet/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TimeSheetDetail detail = TimeSheetMasterRepository.GetTimeSheetDetailById(id);
            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        //
        // POST: /TimeSheet/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeSheetDetail.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
