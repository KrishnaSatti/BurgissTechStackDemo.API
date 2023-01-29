using BurgissTechStackDemo.API.DataModels;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace BurgissTechStackDemo.API.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeAsync(Guid employeeId);
        Task<List<Gender>> GetGendersAsync();
        Task<bool> Exists(Guid employeeId);
        Task<Employee> UpdateEmployee(Guid employeeId, Employee request);
        Task<Employee> DeleteStudent(Guid employeeId);
        Task <Employee> AddEmployee(Employee request);
        Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl);
    }
}
