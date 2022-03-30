
namespace Paylocity_API.Models.Employee
{
    public interface IEmployee
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        double AnnualBenefitCost { get; set; }
        EmployeeCompensationType CompensationType { get; set; }
        List<Dependant> Dependants { get; set; }
        ICollection<IDiscountable> Discounts { get; set; }
        double CalculateTotalDependantCost();
        double CalculateTotalFamilyCost();
        
    }
}