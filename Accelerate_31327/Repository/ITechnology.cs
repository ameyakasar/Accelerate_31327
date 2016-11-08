using Accelerate_31327.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accelerate_31327.Repository
{
    public interface ITechnology
    {
        List<TechnologyModel> GetTechnologyList();
        bool AddTechnologyDetails(TechnologyModel objTechnologyModel);

        TechnologyModel GetTechnologyDetailsByID(int id);
         bool DeleteTechnologyByID(int id);
    }
}
