
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paylocity_API.Models
{
    public class Dependant : DependantBenefitEligible, IContactInfo, IDependant
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DependantRelationship Relationship { get; set; }
        public int EmployeeId { get; set; }

        //Not Ideal to have constructor with First Name but with current duck-tape version
        //of discount, no ideal way without revamp to factory
        public Dependant(string firstName)
        {
            FirstName = firstName;
            //For this assessment I've done this for now, if this application was
            //going to production I would instead create a discount factory and implement that way.
            Discounts = new List<IDiscountable> { new ANameDiscount(FirstName) };
        }
    }
}
