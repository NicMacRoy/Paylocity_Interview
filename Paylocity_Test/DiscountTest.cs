using NUnit.Framework;
using Moq;
using Paylocity_API.Models;
using System;

namespace Paylocity_Test
{
    [TestFixture]
    public class DiscountTests
    {
        [TestCase("Ariel")]
        [TestCase("aries")]
        [Test]
        public void ApplyDiscount_NameStartsWithA_ReturnsTrue(string firstName)
        {
            //Arrange
            var mockedDiscount = new Mock<IDiscountable>();

            //Act
            mockedDiscount
                .Setup(x => x.IsEligible())
                .Returns(true);

            //Assert
            var discount = new ANameDiscount(firstName);

            var result = discount.IsEligible();

            Assert.AreEqual(true, result);
        }


        [TestCase("sam")]
        [TestCase("Wise")]
        [TestCase("")]
        [Test]
        public void IsEligible_NameDoesNotStartWithA_ReturnsFalse(string firstName)
        {
            //Arrange
            var mockedDiscount = new Mock<IDiscountable>();

            //Act
            mockedDiscount
                .Setup(x => x.IsEligible())
                .Returns(false);

            //Assert
            var discount = new ANameDiscount(firstName);

            var result = discount.IsEligible();

            Assert.AreEqual(false, result);
        }

        [TestCase(2000)]
        [TestCase(1234)]
        [TestCase(0)]
        [Test]
        public void GetAppliedDiscountValue_ReturnsCorrectValue(double initialCost)
        {
            //Arrange
            var mockedDiscount = new Mock<IDiscountable>();

            //Act
            mockedDiscount
                .Setup(x => x.GetAppliedDiscountValue(initialCost))
                .Returns(Math.Round(initialCost * .9, 2));

            //Assert
            var discount = new ANameDiscount("Alex");

            var result = discount.GetAppliedDiscountValue(initialCost);

            Assert.AreEqual(Math.Round(initialCost * .9,2), result);
        }

        [TestCase(6500)]
        [TestCase(4321)]
        [TestCase(1)]
        [Test]
        public void GetAppliedDiscountValue_ReturnsExpectedBehavior(double initialCost)
        {
            //Arrange
            var mockedDiscount = new Mock<IDiscountable>();

            //Act
            mockedDiscount
                .Setup(x => x.GetAppliedDiscountValue(initialCost))
                .Returns(initialCost * .3);

            //Assert
            var discount = new ANameDiscount("test");

            var result = discount.GetAppliedDiscountValue(initialCost);

            Assert.AreNotEqual((initialCost * .3), result);
        }
    }
}