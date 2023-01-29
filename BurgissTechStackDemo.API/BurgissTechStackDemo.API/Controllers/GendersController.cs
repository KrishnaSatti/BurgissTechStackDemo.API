using AutoMapper;
using BurgissTechStackDemo.API.DataModels;
using BurgissTechStackDemo.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BurgissTechStackDemo.API.Controllers
{
    [ApiController]
    public class GendersController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;
        public GendersController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.employeeRepository= employeeRepository;
            this.mapper = mapper;   
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllGenders()
        {
            var genderList = await employeeRepository.GetGendersAsync();

            if(genderList == null || !genderList.Any())
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<Gender>>(genderList));
        }
    }
}
