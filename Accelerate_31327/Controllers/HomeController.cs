using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Accelerate_31327.Models;
using Accelerate_31327.Security;
using Accelerate_31327.Repository;
//main controller
namespace Accelerate_31327.Controllers
{
    public class HomeController : Controller
    {

        IEmployee objEmployee = new Employee();
        ITechnology objTechnology = new Technology();

       
        [OnAuthorize(Roles = "Administrator,Guest")]
        public ActionResult Index()
        {

            var empList = objEmployee.GetEmployeeList(); 
            return View(empList);
        }


        [OnAuthorize(Roles = "Administrator,Guest")]
        public ActionResult _PartialEmployeeDetails(FormCollection form)
        {
            var searchText = Request.Form["SerachEmployeeDetails"];
            var empList = objEmployee.GetEmployeeListBySearchText(searchText);
            return PartialView(empList);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View("About");
        }
        [OnAuthorize(Roles = "Administrator,Guest")]
        public ActionResult OperationDialogBox(int id = 0)
        {
            var objEmployeeDetailModel = new EmployeeDetailModel();
            if (id > 0)
            {
                objEmployeeDetailModel = objEmployee.GetEmployeeDetailsByID(id);
            }


            var techList = objTechnology.GetTechnologyList();
            objEmployeeDetailModel.TechnologyList = techList;
            return PartialView(objEmployeeDetailModel);
        }

        [OnAuthorize(Roles = "Administrator")]
        public ActionResult OperationDialogBoxForDetails(int id = 0)
        {
            var objEmployeeDetailModel = new EmployeeDetailModel();
            if (id > 0)
            {
                objEmployeeDetailModel = objEmployee.GetEmployeeDetailsByID(id);
            }


            var techList = objTechnology.GetTechnologyList();
            objEmployeeDetailModel.TechnologyList = techList;

            ViewBag.detailsFlag = true;
            return PartialView("OperationDialogBox", objEmployeeDetailModel);
        }

        [OnAuthorize(Roles = "Administrator")]
        public ActionResult OperationDialogBoxForDelete(int id = 0)
        {
            var objEmployeeDetailModel = new EmployeeDetailModel();
            if (id > 0)
            {
                objEmployeeDetailModel = objEmployee.GetEmployeeDetailsByID(id);
            }


            var techList = objTechnology.GetTechnologyList();
            objEmployeeDetailModel.TechnologyList = techList;
            ViewBag.deleteFlag = true;
            return PartialView("OperationDialogBox", objEmployeeDetailModel);
        }

        [OnAuthorize(Roles = "Administrator")]
        public ActionResult DeleteEmployee(int id = 0)
        {
            var IsDelete=false;
            if (id > 0)
            {
                IsDelete = objEmployee.DeleteEmployeeByID(id);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [OnAuthorize(Roles = "Administrator")]
        public ActionResult AddEditDetail(EmployeeDetailModel objEmployeeDetailModel)
        {
            objEmployeeDetailModel.CreatedBy = objEmployeeDetailModel.UpdatedBy = User.Identity.Name;
            var IsSaved = objEmployee.AddEmployeeDetails(objEmployeeDetailModel);
            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}