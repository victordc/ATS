using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Data.Model;
using ATS.Data;
using ATS.MVC.UI;
using ATS.MVC.UI.Common;

namespace ATS.MVC.UI.Controllers
{
    [ATSAuthorizeAttribute]
    public class CodeTableController : BaseController
    {
        //
        // GET: /CodeTable/

        public ActionResult Index()
        {
            ViewBag.Message = "List Of CodeTable";
            IEnumerable<CodeTable> model = TimesheetRepository.Instance.GetCodeTables();
            return View(model);
        }

        //
        // GET: /CodeTable/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CodeTable/Create

        [HttpPost]
        public ActionResult Create(CodeTable codetable)
        {
            if (ModelState.IsValid)
            {
                CodeTable newCodeTable = codetable;
                newCodeTable.Save();
                return RedirectToAction("Index");
            }

            return View(codetable);
        }

        //
        // GET: /CodeTable/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CodeTable model = TimesheetRepository.Instance.GetCodeTableById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        //
        // POST: /CodeTable/Edit/5

        [HttpPost]
        public ActionResult Edit(CodeTable codetable)
        {
            CodeTable editCodetable = codetable;
            if (ModelState.IsValid)
            {
                editCodetable.Save();
                return RedirectToAction("Index");
            }
            return View(codetable);
        }

        //
        // GET: /CodeTable/Delete/5

        public ActionResult Delete(int id = 0)
        {
            //CodeTable codetable = db.CodeTables.Find(id);
            //if (codetable == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(codetable);
            return View();
        }

        //
        // POST: /CodeTable/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //CodeTable codetable = db.CodeTables.Find(id);
            //db.CodeTables.Remove(codetable);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            base.Dispose(disposing);
        }
    }
}