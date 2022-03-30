using System.ComponentModel.DataAnnotations;

namespace Paylocity_API.Models
{
    public interface IDependant
    {
        [Key]
        int Id { get; set; }
        double AnnualBenefitCost { get; set; }
        ICollection<IDiscountable> Discounts { get; set; }
        int EmployeeId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DependantRelationship Relationship { get; set; }
        double CalculateTotalSelfCost();
    }
}