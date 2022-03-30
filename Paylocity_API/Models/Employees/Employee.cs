using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Paylocity_API.Utilities;

namespace Paylocity_API.Models.Employee
{
    public class Employee : EmployeeBenefitEligible, IContactInfo, IEmployee
    {
        //Not Ideal to have constructor with First Name but with current duck-tape version
        //of discount, no ideal way without revamp to factory
        public Employee(string firstName)
        {
            FirstName = firstName;
            //For this assessment I've done this for now, if this application was
            //going to production I would instead create a discount factory and implement that way.
            Discounts = new List<IDiscountable> { new ANameDiscount(FirstName) };
        }

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Dependant> Dependants { get; set; } = new List<Dependant>();
        public EmployeeCompensationType CompensationType { get; set; }
        [NotMapped]
        public double PretaxedPaycheckSalary { get; set; } = 2000;

        public double TaxedPaycheckSalary
        {
            //TODO: Make paycheck frequency dynamic with organization parent class
            get => PretaxedPaycheckSalary - Math.Round(CalculateTotalFamilyCost() / 26, 2);
        }

        public double CalculateTotalDependantCost()
        {
            double dependantCost = 0;

            Dependants.ForEach(dependant => dependantCost += dependant.CalculateTotalSelfCost());

            return dependantCost;
        }

        public double CalculateTotalFamilyCost() => Dependants.Count > 0 ? CalculateTotalSelfCost() + CalculateTotalDependantCost() : CalculateTotalSelfCost();


    }
}
