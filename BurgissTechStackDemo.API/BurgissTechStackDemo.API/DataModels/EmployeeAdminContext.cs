using Microsoft.EntityFrameworkCore;

namespace BurgissTechStackDemo.API.DataModels
{
    public class EmployeeAdminContext : DbContext
    {
        public EmployeeAdminContext(DbContextOptions<EmployeeAdminContext> options) : base(options) { 
            
        }

        public DbSet<Employee> Employee { get; set;}
        public DbSet<Gender> Gender { get; set;}
        public DbSet<Address> Address { get; set;}
        public DbSet<Department> Department { get; set; }

    }
}
