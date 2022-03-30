using NUnit.Framework;
using Moq;
using Paylocity_API.Models;
using Paylocity_API.Models.Employee;
using System.Collections.Generic;

namespace Paylocity_Test
{
    [TestFixture]
    public class EmployeeTest
    {
        [TestCase("Sam", 2000, 1961.54)]
        [TestCase("Tony", 6000, 5961.54)]
        [Test]
        public void TaxedPaycheckSalary_With0Dependants_ReturnsSelfPrice(string employeeFirstName, double initialSalary, double taxedSalary)
        {
            //Arrange
            var employee = new Employee(employeeFirstName);
            employee.Discounts = new List<IDiscountable> { new ANameDiscount(employeeFirstName) };
            employee.PretaxedPaycheckSalary = initialSalary;
            employee.Dependants = new List<Dependant>();

            //Act
            var result = employee.TaxedPaycheckSalary;

            //Assert
            Assert.AreEqual(taxedSalary, result);
        }

        [TestCase("Alex", 2000, 1965.38)]
        [TestCase("Alex", 6000, 5965.38)]
        [Test]
        public void TaxedPaycheckSalary_With0Dependants_AndNameStartsA_ReturnsDiscountedSelfPrice(string employeeFirstName, double initialSalary, double taxedSalary)
        {
            //Arrange
            var employee = new Employee(employeeFirstName);
            employee.Discounts = new List<IDiscountable> { new ANameDiscount(employeeFirstName) };
            employee.PretaxedPaycheckSalary = initialSalary;
            employee.Dependants = new List<Dependant>();

            //Act
            var result = employee.TaxedPaycheckSalary;

            //Assert
            Assert.AreEqual(taxedSalary, result);
        }

        [TestCase("Anthony", "Tony", 2000, 1926.92)]
        [TestCase("Anthony", "winter", 6000, 5926.92)]
        [Test]
        public void TaxedPaycheckSalary_With2DependantsNotStartingWithA_AndNameStartsA_ReturnsDiscountedSelfPrice(string employeeFirstName,
                        string dependantFirstName, double initialSalary, double taxedSalary)
        { 
            //Arrange
            var employee = new Employee(employeeFirstName);
            employee.PretaxedPaycheckSalary = initialSalary;
            employee.Discounts = new List<IDiscountable> { new ANameDiscount(employeeFirstName) };
            employee.Dependants.Add(new Dependant(dependantFirstName));
            employee.Dependants[0].Discounts = new List<IDiscountable> { new ANameDiscount(dependantFirstName) };
            employee.Dependants.Add(new Dependant(dependantFirstName));
            employee.Dependants[1].Discounts = new List<IDiscountable> { new ANameDiscount(dependantFirstName) };

            //Act
            var result = employee.TaxedPaycheckSalary;

            //Assert
            Assert.AreEqual(taxedSalary, result);
        }

        [TestCase("Anthony", "aaron",2000, 1930.77)]
        [TestCase("alex", "Alice", 6000, 5930.77)]
        [Test]
        public void TaxedPaycheckSalary_With2DependantsStartingWithA_AndNameStartsA_ReturnsDiscountedSelfPrice(string employeeFirstName,
                        string dependantFirstName, double initialSalary, double taxedSalary)
        {
            //Arrange
            var employee = new Employee(employeeFirstName);
            employee.PretaxedPaycheckSalary = initialSalary;
            employee.Discounts = new List<IDiscountable> { new ANameDiscount(employeeFirstName) };
            employee.Dependants.Add(new Dependant(dependantFirstName));
            employee.Dependants[0].Discounts = new List<IDiscountable> { new ANameDiscount(dependantFirstName) };
            employee.Dependants.Add(new Dependant(dependantFirstName));
            employee.Dependants[1].Discounts = new List<IDiscountable> { new ANameDiscount(dependantFirstName) };


            //Act
            var result = employee.TaxedPaycheckSalary;

            //Assert
            Assert.AreEqual(taxedSalary, result);
        }
    }
}
