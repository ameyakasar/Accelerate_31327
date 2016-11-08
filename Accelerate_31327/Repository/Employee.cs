using Accelerate_31327.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accelerate_31327.Repository
{

    public class Employee : IEmployee
    {
        AccelerateEntities ac = new AccelerateEntities();

        public IEnumerable<EmployeeDetailModel> GetEmployeeList()
        {
            try
            {
                var lstEmployee = (from e in ac.GetEmployeeList("")
                                   select new EmployeeDetailModel()
                                   {
                                       Emp_ID = e.Emp_ID,
                                       Emp_FirstName = e.Emp_FirstName,
                                       Emp_LastName = e.Emp_LastName,
                                       Emp_Designation = e.Emp_Designation,

                                       Emp_Email = e.Emp_Email,
                                       SeletedTechnologyText = e.Technologies

                                   }).ToList();
               
                return lstEmployee;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public bool AddEmployeeDetails(EmployeeDetailModel objEmployeeDetailModel)
        {
            try
            {

                var objEmployeeDetail = (from emp in ac.Employee_Details
                                         where emp.Emp_ID == objEmployeeDetailModel.Emp_ID
                                         select emp).FirstOrDefault();

                if (objEmployeeDetail != null)
                {
                    objEmployeeDetail.Emp_FirstName = objEmployeeDetailModel.Emp_FirstName;
                    objEmployeeDetail.Emp_LastName = objEmployeeDetailModel.Emp_LastName;
                    objEmployeeDetail.Emp_Email = objEmployeeDetailModel.Emp_Email;
                    objEmployeeDetail.Emp_Designation  = objEmployeeDetailModel.Emp_Designation ;
                    objEmployeeDetail.UpdateDate = DateTime.UtcNow;
                    objEmployeeDetail.UpdatedBy = objEmployeeDetailModel.UpdatedBy;

           
                }
                else
                {
                    var objemployee = new Employee_Details()
                    {
                        Emp_ID = objEmployeeDetailModel.Emp_ID,    
                        Emp_FirstName = objEmployeeDetailModel.Emp_FirstName,
                        Emp_LastName = objEmployeeDetailModel.Emp_LastName,
                        Emp_Email = objEmployeeDetailModel.Emp_Email,
                        Emp_Designation = objEmployeeDetailModel.Emp_Designation,
                        CreatedBy = objEmployeeDetailModel.CreatedBy,
                        CreateDate = DateTime.UtcNow,
                        UpdatedBy = objEmployeeDetailModel.UpdatedBy,
                        UpdateDate = DateTime.UtcNow
                    };
                    ac.Employee_Details.Add(objemployee);
                }

                ac.SaveChanges();

                //Add/edit technology details
                if (objEmployeeDetailModel != null && objEmployeeDetailModel.SeletedTechnology!= null)
                {
                    var selectedIds = objEmployeeDetailModel.SeletedTechnology.Split(',');
                    var lstEmployeeSkills = ac.Employee_Skill_Details.Where(eml => eml.Emp_ID == objEmployeeDetailModel.Emp_ID).ToList();
                    foreach (var objEmployeeSkill in lstEmployeeSkills)
                    {
                        ac.Employee_Skill_Details.Remove(objEmployeeSkill);
                    }
                    ac.SaveChanges();

                    foreach (var objTechnologyID in selectedIds)
                    {
                        var tid = Convert.ToInt32(objTechnologyID);
                        var objempskill = new Employee_Skill_Details()
                        {
                            Emp_ID = objEmployeeDetailModel.Emp_ID,
                            Technology_ID = tid,
                            UpdateDate = DateTime.UtcNow,
                            CreateDate = DateTime.UtcNow,
                            CreatedBy = objEmployeeDetailModel.CreatedBy,
                            UpdatedBy = objEmployeeDetailModel.UpdatedBy

                        };
                        ac.Employee_Skill_Details.Add(objempskill);
                    }
                    ac.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateEmployeeDetails(EmployeeDetailModel objEmployeeDetailModel)
        {
            try
            {


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmployeeDetailModel GetEmployeeDetailsByID(int id)
        {
            try
            {
                var seletedId = string.Join(",", ac.Employee_Skill_Details.Where(eid=>eid.Emp_ID==id).Select(x => x.Technology_ID.ToString()).ToArray());
              var objEmployee= (from e in ac.Employee_Details
                      
                        where e.Emp_ID == id
                        select new EmployeeDetailModel()
                        {
                            Emp_ID = e.Emp_ID,
                            Emp_FirstName = e.Emp_FirstName,
                            Emp_LastName = e.Emp_LastName,
                            Emp_Email = e.Emp_Email,
                            Emp_Designation = e.Emp_Designation,
                            SeletedTechnology = seletedId

                        }).FirstOrDefault();

              return objEmployee;
                   


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteEmployeeByID(int id)
        {
            try
            {
                var deleteskill=ac.Employee_Skill_Details.Where(empid=>empid.Emp_ID==id).ToList();
                foreach (var objSkills in deleteskill)
                {
                    ac.Employee_Skill_Details.Remove(objSkills);
                }
                ac.SaveChanges();
                var objEmployeeDetail = (from emp in ac.Employee_Details
                                         where emp.Emp_ID == id
                                         select emp).FirstOrDefault();

                
                ac.Employee_Details.Remove(objEmployeeDetail);
                ac.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public IEnumerable<EmployeeDetailModel> GetEmployeeListBySearchText(string searchText)
        {
            try
            {
                var lstEmployee = (from e in ac.GetEmployeeList(searchText)
                                   select new EmployeeDetailModel()
                                   {
                                       Emp_ID = e.Emp_ID,
                                       Emp_FirstName = e.Emp_FirstName,
                                       Emp_LastName = e.Emp_LastName,
                                       Emp_Designation = e.Emp_Designation,

                                       Emp_Email = e.Emp_Email,
                                       SeletedTechnologyText=e.Technologies

                                   }).ToList();
                

                return lstEmployee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}