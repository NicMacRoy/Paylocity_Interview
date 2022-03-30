using System.ComponentModel.DataAnnotations.Schema;
using Paylocity_API.Utilities;

namespace Paylocity_API.Models
{
    public abstract class EmployeeBenefitEligible
    {
        [NotMapped]
        public double AnnualBenefitCost { get; set; } = 1000;
        [NotMapped]
        public ICollection<IDiscountable> Discounts { get; set; } = new List<IDiscountable>();
        public double PayCheckBenefitCost
        {
            //TODO: Make paycheck frequency dynamic with organization parent class
            get => CalculationUtilities.RoundSalaries(CalculateTotalSelfCost() / 26);
        }

        public double ApplyBenefitDiscount(double cost)
        {
            foreach (var discount in Discounts)
            {
                if (discount.IsEligible())
                    cost = discount.GetAppliedDiscountValue(cost);
            }

            return cost;
        }

        public double CalculateTotalSelfCost() => ApplyBenefitDiscount(AnnualBenefitCost);
    }
}
