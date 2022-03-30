using Paylocity_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Paylocity_API.VM
{
    public class DependantViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DependantRelationship Relationship { get; set; }
    }
}
