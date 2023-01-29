﻿using AutoMapper;
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
        private readonly IImageRepository imageRepository;

        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper, IImageRepository imageRepository)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
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

        [HttpPost]
        [Route("[controller]/{employeeId:guid}/upload-image")]
        public async Task<IActionResult> UploadImage([FromRoute] Guid employeeId, IFormFile profileImage)
        {
            var validExtensions = new List<string>
            {
               ".jpeg",
               ".png",
               ".gif",
               ".jpg"
            };

            if (profileImage != null && profileImage.Length > 0)
            {
                var extension = Path.GetExtension(profileImage.FileName);
                if (validExtensions.Contains(extension))
                {
                    if (await employeeRepository.Exists(employeeId))
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);

                        var fileImagePath = await imageRepository.Upload(profileImage, fileName);

                        if (await employeeRepository.UpdateProfileImage(employeeId, fileImagePath))
                        {
                            return Ok(fileImagePath);
                        }

                        return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");
                    }
                }

                return BadRequest("This is not a valid Image format");
            }

            return NotFound();
        }
    }
}
