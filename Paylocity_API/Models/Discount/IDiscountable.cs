namespace Paylocity_API.Models
{
    public interface IDiscountable
    {
        string GetDiscountCode();
        string GetDiscountDescription();
        double GetAppliedDiscountValue(double initValue);
        bool IsEligible();
    }
}
