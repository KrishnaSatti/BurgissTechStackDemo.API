namespace BurgissTechStackDemo.API.DataModels
{
    public class Address
    {
        public Guid Id { get; set; }
        public string? PhysicalAddress { get; set; }
        public string? PostalAddress { get; set; }
        
        //Navigation Property
        public Guid EmployeeID { get; set; }
    }
}
