using NUnit.Framework;
using Moq;
using Paylocity_API.Models;
using System.Collections.Generic;

namespace Paylocity_Test
{
    [TestFixture]
    public class DependantTest
    {
        [TestCase("Sam", 19.23)]
        [TestCase("Wise", 19.23)]
        [Test]
        public void PayCheckBenefitCost_NameNotStartingWithA_ReturnsValidInput(string name, double expectedCost)
        {
            //Arrange
            var dependant = new Dependant(name);
            dependant.Discounts = new List<IDiscountable> { new ANameDiscount(name) };
            //Act
            var result = dependant.PayCheckBenefitCost;
            //Asert
            Assert.AreEqual(expectedCost, result);
        }

        [TestCase("aaron", 17.31)]
        [TestCase("Amos", 17.31)]
        [Test]
        public void PayCheckBenefitCost_NameStartingWithA_ReturnsValidInput(string name, double expectedCost)
        {
            //Arrange
            var dependant = new Dependant(name);
            dependant.Discounts = new List<IDiscountable> { new ANameDiscount(name) };
            //Act
            var result = dependant.PayCheckBenefitCost;
            //Asert
            Assert.AreEqual(expectedCost, result);
        }
    }
}
