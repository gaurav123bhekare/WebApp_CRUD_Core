using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
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
        public readonly Mock<IEmployeeRepository> engagementsService = new Mock<IEmployeeRepository>();


        public EmployeeControllerTest()
        {
            if (_repository ==null)
            {
                _controller = new EmployeeController(engagementsService.Object);
                _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            }            
        }
        [Fact]
        public void GetEmployee_Sucess()
        {  // Act 
            var result = _controller.Get();
           
            // Assert
            Assert.NotNull(result);            
        }

        [Fact]
        public void AddEmployee_Success()
        {
            Employee emp = new Employee()
            {
                EmployeeID = 0,
                FirstName = "Sanjay1",
                LastName = "Singh1",
                EmailId = "Sanjay1.1Singh02@gmail.com",
                Age = 23
            };
            var response = _controller.Post(emp);
            response.ToString().Equals(HttpStatusCode.OK);
        }
    }
}