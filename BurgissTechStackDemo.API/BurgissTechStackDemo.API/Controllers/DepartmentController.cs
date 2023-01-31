using AutoMapper;
using BurgissTechStackDemo.API.DataModels;
using BurgissTechStackDemo.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BurgissTechStackDemo.API.Controllers
{
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;
        public DepartmentController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.employeeRepository= employeeRepository;
            this.mapper = mapper;   
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departmentList = await employeeRepository.GetDepartmentAsync();

            if(departmentList == null || !departmentList.Any())
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<Department>>(departmentList));
        }
    }
}
