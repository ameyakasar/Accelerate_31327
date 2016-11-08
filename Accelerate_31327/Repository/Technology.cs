using Accelerate_31327.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accelerate_31327.Repository
{

    public class Technology : ITechnology
    {
        AccelerateEntities ac = new AccelerateEntities();

        public List<TechnologyModel> GetTechnologyList()
        {
            return (from e in ac.Technology_Details
                    select new TechnologyModel() { Technology_ID = e.Technology_ID, Technology_Name = e.Technology_Name }).ToList();
            throw new NotImplementedException();
        }

        public bool AddTechnologyDetails(TechnologyModel objTechnologyModel)
        {
            try
            {
                var objTechnologyDetail = (from emp in ac.Technology_Details
                                         where emp.Technology_ID == objTechnologyModel.Technology_ID
                                         select emp).FirstOrDefault();

                if (objTechnologyDetail != null)
                {
                    objTechnologyDetail.Technology_Name = objTechnologyModel.Technology_Name;
                    objTechnologyDetail.UpdateDate = DateTime.UtcNow;
                    objTechnologyDetail.UpdatedBy = objTechnologyModel.UpdatedBy;
                }
                else
                {
                    var objTechnology = new Technology_Details()
                    {
                        Technology_Name = objTechnologyModel.Technology_Name,
                        CreateDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        UpdatedBy = objTechnologyModel.UpdatedBy,
                        CreatedBy = objTechnologyModel.CreatedBy

                    };

                    ac.Technology_Details.Add(objTechnology);
                }
                ac.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TechnologyModel GetTechnologyDetailsByID(int id)
        {
         
            try
            {
               
              var objTechnology= (from e in ac.Technology_Details
                      
                        where e.Technology_ID == id
                                select new TechnologyModel()
                        {
                            Technology_ID = e.Technology_ID,
                            Technology_Name = e.Technology_Name,
                           

                        }).FirstOrDefault();

              return objTechnology;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }

        public bool DeleteTechnologyByID(int id)
        {
            try
            {
                var deleteskill = ac.Employee_Skill_Details.Where(tid => tid.Technology_ID == id).ToList();
                foreach (var objSkills in deleteskill)
                {
                    ac.Employee_Skill_Details.Remove(objSkills);
                }
                ac.SaveChanges();
                var objTechnologyDetail = (from emp in ac.Technology_Details
                                         where emp.Technology_ID == id
                                         select emp).FirstOrDefault();


                ac.Technology_Details.Remove(objTechnologyDetail);
                ac.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}