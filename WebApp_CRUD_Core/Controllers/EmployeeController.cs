using System;
using Microsoft.AspNetCore.Mvc;
using WebApp_CRUD_Core.Model;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using WebApp_CRUD_Core.Repository;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApp_CRUD_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IEmployeeRepository _employee = null;

        //public EmployeeController(IWebHostEnvironment env,
        //   IEmployeeRepository employee)
        //{
        //    _env = env;
        //    _employee = employee ?? throw new ArgumentNullException(nameof(employee));
        //}

        public EmployeeController(IEmployeeRepository employee)
        {
            _employee = employee;
        }

        [HttpGet]
        [Route("GetEmployee")]
        public ActionResult Get()
        {
            return Ok( _employee.GetEmployees());
        }

        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> Post(Employee emp)
        {

            var result = await _employee.InsertEmployee(emp);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }

            return Ok("Record Added Successfully");
        }


        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> Put(Employee emp)
        {
            var result = await _employee.UpdateEmployee(emp);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Record Updated Successfully");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var result = _employee.DeleteEmployee(id);
            return new JsonResult("Record Deleted Successfully");
        }

    }

}
