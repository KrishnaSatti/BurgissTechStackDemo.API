using BurgissTechStackDemo.API.DataModels;

namespace BurgissTechStackDemo.API.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeesAsync();
    }
}
