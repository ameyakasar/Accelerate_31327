using Accelerate_31327.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accelerate_31327.Repository
{
   public interface IEmployee
    {
       IEnumerable<EmployeeDetailModel> GetEmployeeList();
       bool AddEmployeeDetails(EmployeeDetailModel objEmployeeDetailModel);
       bool UpdateEmployeeDetails(EmployeeDetailModel objEmployeeDetailModel);
       EmployeeDetailModel GetEmployeeDetailsByID(int id);
       bool DeleteEmployeeByID(int id);

       IEnumerable<EmployeeDetailModel> GetEmployeeListBySearchText(string searchText);
    }
}
