using AutoMapper;
using BurgissTechStackDemo.API.DomainModels;
using BurgissTechStackDemo.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BurgissTechStackDemo.API.Controllers
{
    [ApiController]
    public class EmployeesController : Controller
    {
        private IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;

        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await employeeRepository.GetEmployeesAsync();

            return Ok(mapper.Map<List<Employee>>(employees));

        }
    }
}
