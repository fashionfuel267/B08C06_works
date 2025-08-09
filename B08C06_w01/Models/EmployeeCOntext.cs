using Microsoft.EntityFrameworkCore;

namespace B08C06_w01.Models
{
    public class EmployeeCOntext: DbContext
    {
        public EmployeeCOntext(DbContextOptions<EmployeeCOntext> options) : base(options)
        {
        }
        public DbSet<EMployee> Employees { get; set; }
        
    }
   
}
