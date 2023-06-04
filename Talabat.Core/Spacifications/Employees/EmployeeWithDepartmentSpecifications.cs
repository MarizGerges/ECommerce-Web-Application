using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Spacifications.Employees
{
    public class EmployeeWithDepartmentSpecifications : BaseSpacification<Employee>
    {
        public EmployeeWithDepartmentSpecifications()
        {
            Includes.Add(e => e.Department);
        }
        public EmployeeWithDepartmentSpecifications(int id) : base(e=>e.Id==id)
        {
            Includes.Add(e => e.Department);
        }
    }
}
