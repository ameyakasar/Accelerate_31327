using Accelerate_31327.Models;
using Accelerate_31327.Repository;
using Accelerate_31327.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Accelerate_31327.Controllers
{
    public class TechnologyController : Controller
    {
        ITechnology objTechnology = new Technology();


        [OnAuthorize(Roles = "Administrator,Guest")]
        public ActionResult Index()
        {
            var empList = objTechnology.GetTechnologyList();
            return View(empList);
        }

        //
        // GET: /Technology/Details/5
        [OnAuthorize(Roles = "Administrator")]
        public ActionResult AddEditTechnology(int id = 0)
        {
            var objTechnologyModel = new TechnologyModel();
            if (id > 0)
            {
                objTechnologyModel = objTechnology.GetTechnologyDetailsByID(id);
            }
            return PartialView(objTechnologyModel);
        }

        [OnAuthorize(Roles = "Administrator")]
        public ActionResult TechnologyDetail(int id = 0)
        {
            var objTechnologyModel = new TechnologyModel();
            if (id > 0)
            {
                objTechnologyModel = objTechnology.GetTechnologyDetailsByID(id);
            }
            ViewBag.TechnologyDetail = true;
            return PartialView("AddEditTechnology", objTechnologyModel);
        }

        [OnAuthorize(Roles = "Administrator")]
        public ActionResult TechnologyDelete(int id = 0)
        {
            var objTechnologyModel = new TechnologyModel();
            if (id > 0)
            {
                objTechnologyModel = objTechnology.GetTechnologyDetailsByID(id);
            }
            ViewBag.TechnologyDelete = true;
            return PartialView("AddEditTechnology", objTechnologyModel);
        }

        [HttpPost]
        [OnAuthorize(Roles = "Administrator")]
        public ActionResult AddEditDetail(TechnologyModel objTechnologyModel)
        {
            objTechnologyModel.CreatedBy = objTechnologyModel.UpdatedBy = User.Identity.Name;
            var IsSaved = objTechnology.AddTechnologyDetails(objTechnologyModel);
            return RedirectToAction("Index");
        }
  
         //
        // GET: /Technology/Delete/5
         [OnAuthorize(Roles = "Administrator")]
        public ActionResult DeleteTechnology(int id)
        {
            var IsDelete = false;
            if (id > 0)
            {
                IsDelete = objTechnology.DeleteTechnologyByID(id);
            }
            return RedirectToAction("Index");
        }

       
    }
}
