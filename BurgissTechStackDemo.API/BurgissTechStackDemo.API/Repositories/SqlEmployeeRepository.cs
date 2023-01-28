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
        public async Task<List<Employee>> GetEmployeesAsync() 
        {
            return await context.Employee.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

    }
}
