using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Accelerate_31327.Models
{
    public class EmployeeDetailModel
    {
        [Display(Name = "Employee ID")]
        public int Emp_ID { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage="*")]
        public string Emp_FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "*")]
        public string Emp_LastName { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "*")]
        public string Emp_Email { get; set; }
        [Display(Name = "Designation")]
        [Required(ErrorMessage = "*")]
        public string Emp_Designation { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime UpdateDate { get; set; }

        public List<TechnologyModel> TechnologyList { get; set; }

        public string SeletedTechnology { get; set; }

        public string SeletedTechnologyText { get; set; }
    }

    public class TechnologyModel
    {
        public int Technology_ID { get; set; }
        [Display(Name = "Technology Name")]
         [Required(ErrorMessage = "*")]
        public string  Technology_Name { get; set; }

        public bool CheckedStatus { get; set; }


        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
   
}