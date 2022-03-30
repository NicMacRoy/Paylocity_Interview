namespace Paylocity_API.Models
{
    public class ANameDiscount : IDiscountable
    {
        private string _Name { get; set; }

        public ANameDiscount(string name)
        {
            _Name = name;
        }

        public double GetAppliedDiscountValue(double initValue) => Math.Round(initValue * .9,2);

        public string GetDiscountCode() => "A001";

        public string GetDiscountDescription() => "10% Discount for Anyone whose name starts with ‘A'";

        public bool IsEligible()
        {
            if (String.IsNullOrEmpty(_Name))
                return false;

            return _Name[..1].ToUpper() == "A";
        } 
    }
}
