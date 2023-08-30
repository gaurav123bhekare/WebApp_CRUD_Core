using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebApp_CRUD_Core.Controllers;
using WebApp_CRUD_Core.Model;
using WebApp_CRUD_Core.Repository;
using Xunit;

namespace UnitTestCase.Test
{
    public class EmployeeControllerTest
    {
        EmployeeController _controller;
        IEmployeeRepository _repository = null;

     

        public EmployeeControllerTest()
        {
            if (_repository ==null)
            {
                _controller = new EmployeeController(_repository);
            }
            
        }
        [Fact]
        public void GetEmployee_Sucess()
        {
            // Arrage 

            // Act 
            var result = _controller.Get();
            //var resultType = result as OkObjectResult;
            //var resultList = resultType.Value as List<Employee>;

            // Assert
            Assert.NotNull(result);
            //Assert.IsType<List<Employee>>(resultType.Value);
            //Assert.Equal(3, resultList.Count);
        }

        [Fact]
        public async Task AddEmployee_Success()
        {
            Employee emp = new Employee()
            {
                EmployeeID = 5,
                FirstName = "Sanjay",
                LastName = "Singh",
                EmailId = "Sanjay.Singh02@gmail.com",
                Age = 21
            };

            var response =await _controller.Post(emp);
            Assert.IsType<CreatedAtActionResult>(response);

            var createdEmp = response as CreatedAtActionResult;
            Assert.IsType<Employee>(createdEmp.Value);
            var employeeItem = createdEmp.Value as Employee;

            Assert.Equal("Sanjay", emp.FirstName);
            Assert.Equal("Singh", emp.LastName);
            Assert.Equal("Sanjay.Singh02@gmail.com", emp.EmailId);
            Assert.Equal(21, emp.Age);


        }
    }
}