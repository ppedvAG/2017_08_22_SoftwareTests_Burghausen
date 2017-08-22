using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        [TestCategory("Unit")]
        public void Calculator_Sum_6and9_Result15()
        {
            // Arrange
            Calculator calc = new Calculator();

            // Act
            int result = calc.Sum(6, 9);

            // Assert
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void Calculator_Sum_0and0_Result0()
        {
            // Arrange
            Calculator calc = new Calculator();

            // Act
            int result = calc.Sum(0, 0);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Calculator_Sum_M5andM3_ResultM8()
        {
            // Arrange
            Calculator calc = new Calculator();

            // Act
            int result = calc.Sum(-5, -3);

            // Assert
            Assert.AreEqual(-8, result);
        }

        [TestMethod]
        public void Calculator_Sum_MAX_and_1_Result_OverflowException()
        {
            // Arrange
            Calculator calc = new Calculator();

            try
            {
                int result = calc.Sum(int.MaxValue, 1);
                Assert.Fail("No Overflow Exception was thrown.");
            }
            catch (OverflowException)
            { }
            catch (Exception)
            {
                Assert.Fail("No Overflow Exception was thrown.");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]  // Assert
        public void Calculator_Sum_MIN_and_M1_Result_OverflowException()
        {
            // Arrange
            Calculator calc = new Calculator();

            // Act
            int result = calc.Sum(int.MinValue, -1);
        }
    }
}
