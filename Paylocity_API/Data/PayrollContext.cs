using Microsoft.EntityFrameworkCore;
using Paylocity_API.Models.Employee;
using Paylocity_API.Models;

namespace Paylocity_API { 
    public class PayrollContext : DbContext
    {

        public PayrollContext (DbContextOptions<PayrollContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dependant> Dependants { get; set; }

    }
}
