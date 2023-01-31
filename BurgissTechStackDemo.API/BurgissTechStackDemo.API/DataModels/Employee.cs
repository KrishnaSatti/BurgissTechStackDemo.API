namespace BurgissTechStackDemo.API.DataModels
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public long Mobile { get; set; }
        public string? ProfileImageUrl { get; set; }
        public Guid GenderId { get; set; }

        public Guid DepartmentId { get; set; }


        //Navigation Properties

        public Gender Gender { get; set; }
        public Department Department { get; set; }
        public Address Address { get; set; } 


    }
}
