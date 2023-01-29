using AutoMapper;
using BurgissTechStackDemo.API.DomainModels;
using BurgissTechStackDemo.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

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

        [HttpGet]
        [Route("[controller]/{employeeId:guid}"), ActionName("GetEmployeeAsync")]
        public async Task<IActionResult> GetEmployeeAsync([FromRoute] Guid employeeId)
        {
            var employees = await employeeRepository.GetEmployeeAsync(employeeId);

            if (employees == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Employee>(employees));

        }

        [HttpPut]
        [Route("[controller]/{employeeId:guid}")]
        public async Task<IActionResult> updateEmployeeAsync([FromRoute] Guid employeeId, [FromBody] UpdateEmployeeRequest request)
        {
            if(await employeeRepository.Exists(employeeId))
            {
                var updatedStudent = await employeeRepository.UpdateEmployee(employeeId, mapper.Map<DataModels.Employee>(request));
                if(updatedStudent != null)
                {
                    return Ok(mapper.Map<Employee>(updatedStudent));
                }
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("[controller]/{employeeId:guid}")]
        public async Task<IActionResult> DeleteEmployeeAsync([FromRoute] Guid employeeId)
        {
            if(await employeeRepository.Exists(employeeId))
            {
                var employee = await employeeRepository.DeleteStudent(employeeId);
                return Ok(mapper.Map<Employee>(employee));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/add")]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] AddEmployeeRequest request)
        {
            var employee = await employeeRepository.AddEmployee(mapper.Map<DataModels.Employee>(request));
            return CreatedAtAction(nameof(GetEmployeeAsync), new {employeeId = employee.Id}, mapper.Map<Employee>(employee));
        }
    }
}
