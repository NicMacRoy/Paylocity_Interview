namespace Paylocity_API.Models
{
    public interface IBenefitEligible
    {
        double AnnualBenefitCost { get; set; }
        ICollection<IDiscountable> Discounts { get; set; }
        double CalculateTotalSelfCost();
        double ApplyBenefitDiscount(double cost);
    }
}
