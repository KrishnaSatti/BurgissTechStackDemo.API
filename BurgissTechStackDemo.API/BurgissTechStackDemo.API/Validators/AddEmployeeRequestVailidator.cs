using BurgissTechStackDemo.API.DomainModels;
using BurgissTechStackDemo.API.Repositories;
using FluentValidation;

namespace BurgissTechStackDemo.API.Validators
{
    public class AddEmployeeRequestVailidator : AbstractValidator<AddEmployeeRequest>
    {
        public AddEmployeeRequestVailidator(IEmployeeRepository employeeRepository)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Mobile).GreaterThan(0);
            RuleFor(x => x.GenderId).NotEmpty().Must(id =>
            {
                var gender = employeeRepository.GetGendersAsync().Result.ToList()
                .FirstOrDefault(x => x.Id == id);

                if (gender != null)
                {
                    return true;
                }

                return false;
            }).WithMessage("Please select a valid Gender");
            RuleFor(x => x.DepartmentId).NotEmpty().Must(id =>
            {
                var department = employeeRepository.GetDepartmentAsync().Result.ToList()
                .FirstOrDefault(x => x.Id == id);

                if (department != null)
                {
                    return true;
                }

                return false;
            }).WithMessage("Please select a valid department");
            RuleFor(x => x.PhysicalAddress).NotEmpty();
        }
    }
}
