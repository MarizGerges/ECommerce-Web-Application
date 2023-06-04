using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositries;
using Talabat.Core.Spacifications.Employees;

namespace Talabat.Api.Controllers
{
    //i comment this becouse it make error 502 in swager


  
    //public class EmployeesController : BaseApiController
    //{
    //    private readonly IGenaricRepositery<Employee> _employRepo;

    //    public EmployeesController(IGenaricRepositery<Employee> employRepo)
    //    {
    //        _employRepo = employRepo;
    //    }

    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    //    {
    //        var spec = new EmployeeWithDepartmentSpecifications();
    //        var employees =await _employRepo.GetAllWithSpecAsync(spec);

    //        return Ok(employees);
    //    }
    //    [HttpGet]
    //    public async Task<ActionResult<Employee>> GetEmployeesById( int id)
    //    {
    //        var spec = new EmployeeWithDepartmentSpecifications(id);
    //        var employee = await _employRepo.GetByIdWithSpecAsync(spec);

    //        return Ok(employee);
    //    }

    //}
}
 