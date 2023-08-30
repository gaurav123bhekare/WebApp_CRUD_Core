using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_CRUD_Core.Model;

namespace WebApp_CRUD_Core.Repository
{
    public interface IEmployeeRepository
    {
        //Task<IEnumerable<Employee>> GetEmployees();
        List<Employee> GetEmployees();
        Task<Employee> GetEmployeeByID(int ID);
        Task<Employee> InsertEmployee(Employee objEmployee);
        Task<Employee> UpdateEmployee(Employee objEmployee);
        bool DeleteEmployee(int ID);
    }

    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly APIDbContext _appDBContext;      

        public EmployeeRepository(APIDbContext context)
        {
            _appDBContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Employee> GetEmployees()
        {
            return _appDBContext.Employees.ToList();
        }

        //public async Task<IEnumerable<Employee>> GetEmployees()
        //{
        //    return await _appDBContext.Employees.ToListAsync();
        //}


        public async Task<Employee> GetEmployeeByID(int ID)
        {
            return await _appDBContext.Employees.FindAsync(ID);
        }

        public async Task<Employee> InsertEmployee(Employee objEmployee)
        {
            var allUserList = from CurrentUser in _appDBContext.Employees.ToList()
                              select new
                              {
                                  Username = CurrentUser.FirstName + " " + CurrentUser.LastName                                  
                              };
            bool exist = false;
            string fullname = objEmployee.FirstName + " " + objEmployee.LastName;
            foreach (var item in allUserList)
            {
                if (item.Username == fullname)
                {
                    exist = true;
                }
            }
            if (objEmployee.FirstName != null && objEmployee.LastName != null && objEmployee.EmailId != null)
            {
                if (!exist)
                {
                    _appDBContext.Employees.Add(objEmployee);
                    await _appDBContext.SaveChangesAsync();
                }
                else
                {
                    return null;
                }

            }
            return objEmployee;
        }

        public async Task<Employee> UpdateEmployee(Employee objEmployee)
        {
            _appDBContext.Entry(objEmployee).State = EntityState.Modified;
            await _appDBContext.SaveChangesAsync();
            return objEmployee;
        }

        public bool DeleteEmployee(int ID)
        {
            bool result = false;
            var department = _appDBContext.Employees.Find(ID);
            if (department != null)
            {
                _appDBContext.Entry(department).State = EntityState.Deleted;
                _appDBContext.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
