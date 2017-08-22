using NUnit.Framework;
using System;

namespace Calculator.NUnitTests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void Calculator_Sum_4_and_8_Result_12()
        {
            // Arrange
            Calculator calc = new Calculator();

            // Act
            var result = calc.Sum(4, 8);

            // Assert
            Assert.AreEqual(12, result);
        }

        [Test]
        public void Calculator_Sum_MAX_and_1_Result_OverflowException()
        {
            Calculator calc = new Calculator();

            Assert.Throws<OverflowException>(() => calc.Sum(int.MaxValue, 1));
        }
        [Test]
        public void Calculator_Sum_MIN_and_M1_Result_OverflowException()
        {
            Calculator calc = new Calculator();

            Assert.Throws<OverflowException>(() => calc.Sum(int.MinValue, -1));
        }

        [Test]
        public void Calculator_Sum_5_and_8_Result_13()
        {
            Calculator calc = new Calculator();

            var result = calc.Sum(5, 8);
            
            Assert.That(result, Is.EqualTo(13));
        }

        [TestCase(7, 8, 15)]
        [TestCase(0, 0, 0)]
        [TestCase(int.MaxValue-1, 1, int.MaxValue)]
        [TestCase(int.MinValue+1, -1, int.MinValue)]
        [TestCase(100, 100, 200)]
        public void Calculator_Sum_Tests(int zahl1, int zahl2, int expectedResult)
        {
            Calculator calc = new Calculator();

            var result = calc.Sum(zahl1, zahl2);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
