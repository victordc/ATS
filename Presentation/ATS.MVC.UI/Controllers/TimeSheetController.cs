﻿using System;
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

            if (master.Status == 1)
            {
                subject = "Reminder to submit timesheet for month of " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(master.Month);
                message = "Please submit your timesheet to your supervisor as soon as possible. From your friendly agent";
            }
            else if (master.Status == 2)
            {
                subject = "Reminder to approve timesheet for month of " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(master.Month);
                message = "Please evaluate the timesheet from your staff as soon as possible. From your friendly agent";
            }
            //send email
            EmailManager.SendReminder("nusissdotnetagent01@gmail.com", "nusissdotnet", "vinaykasireddy@gmail.com", subject, message);
            return RedirectToAction("Reminder");
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

        public ActionResult Create()
        {
            PersonRepository personRepository = new PersonRepository(new ATSCEEntities());
            Staff staff = personRepository.GetStaffByID(UserSetting.Current.PersonId);
            TimeSheetMaster master = TimeSheetMasterRepository.CreateTimeSheetMasterTemplate(DateTime.Today, staff);
            ViewBag.StatusList = TimeSheetMasterRepository.GetStatusList();
            return View(master);
        }

        //
        // POST: /TimeSheet/Create
        [MultipleButton(Name = "action", Argument = "Save")]
        [HttpPost]
        public ActionResult Save(TimeSheetMaster master)
        {
            if (ModelState.IsValid)
            {
                SaveTimeSheet(master);
                return RedirectToAction("Index");
            }

            return View(master);
        }

        [MultipleButton(Name = "action", Argument = "Submit")]
        [HttpPost]
        public ActionResult Submit(TimeSheetMaster master)
        {
            if (ModelState.IsValid)
            {
                master.Status = Convert.ToInt32(TimeSheetStatus.Submitted);
                SaveTimeSheet(master);
                return RedirectToAction("Index");
            }

            return View(master);
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

            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        //
        // POST: /TimeSheet/Edit/5

        [HttpPost]
        public ActionResult Edit(TimeSheetMaster master)
        {
            TimeSheetMaster newMaster = master;
            if (ModelState.IsValid)
            {
                newMaster.Save();
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
