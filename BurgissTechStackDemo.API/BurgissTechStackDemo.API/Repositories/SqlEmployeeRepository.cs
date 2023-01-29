using BurgissTechStackDemo.API.DataModels;
using Microsoft.EntityFrameworkCore;

namespace BurgissTechStackDemo.API.Repositories
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeAdminContext context;
        public SqlEmployeeRepository(EmployeeAdminContext context) 
        {
            this.context = context;
        }
        public async Task<bool> Exists(Guid employeeId)
        {
            return await context.Employee.AnyAsync(x=> x.Id== employeeId);
        }

        public async Task<Employee> GetEmployeeAsync(Guid employeeId)
        {
            return await context.Employee.Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync(x => x.Id == employeeId);
        }

        public async Task<List<Employee>> GetEmployeesAsync() 
        {
            return await context.Employee.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await context.Gender.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Guid employeeId, Employee request)
        {
            var existingEmployee = await GetEmployeeAsync(employeeId);
            if (existingEmployee != null)
            {
                existingEmployee.FirstName = request.FirstName;
                existingEmployee.LastName = request.LastName;
                existingEmployee.DateOfBirth = request.DateOfBirth;
                existingEmployee.Email = request.Email;
                existingEmployee.Mobile = request.Mobile;
                existingEmployee.GenderId = request.GenderId;
                existingEmployee.Address.PostalAddress = request.Address.PostalAddress;
                existingEmployee.Address.PhysicalAddress = request.Address.PhysicalAddress;
                await context.SaveChangesAsync();
                return existingEmployee;
            }

            return null;

        }

        public async Task<Employee> DeleteStudent(Guid employeeId)
        {
            var employee = await GetEmployeeAsync(employeeId);
            if (employee != null)
            {
                context.Employee.Remove(employee);
                await context.SaveChangesAsync();
                return employee;
            }

            return null;
        }

        public async Task<Employee> AddEmployee(Employee request)
        {
            var employee = await context.Employee.AddAsync(request);
            await context.SaveChangesAsync();
            return employee.Entity;
        }
    }
}
