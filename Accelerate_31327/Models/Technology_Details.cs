//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Accelerate_31327.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Technology_Details
    {
        public Technology_Details()
        {
            this.Employee_Skill_Details = new HashSet<Employee_Skill_Details>();
        }
    
        public int Technology_ID { get; set; }
        public string Technology_Name { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime UpdateDate { get; set; }
    
        public virtual ICollection<Employee_Skill_Details> Employee_Skill_Details { get; set; }
    }
}
