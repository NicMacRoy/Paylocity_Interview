using Paylocity_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Paylocity_API.VM
{
    public class EmployeeViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public EmployeeCompensationType CompensationType { get; set; }
    }
}
