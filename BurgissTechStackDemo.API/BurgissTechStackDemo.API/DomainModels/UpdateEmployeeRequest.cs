namespace BurgissTechStackDemo.API.DomainModels
{
    public class UpdateEmployeeRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public long Mobile { get; set; }
        public Guid GenderId { get; set; }
        public Guid DepartmentId { get; set; }

        public string? PhysicalAddress { get; set; }
        public string? PostalAddress { get; set; }
    }
}
