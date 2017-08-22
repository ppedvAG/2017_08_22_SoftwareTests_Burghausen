using System;
using Xunit;

namespace Calculator.XUnitTests
{
    public class CalculatorTests
    {
        [Fact]
        public void Sum_6_and_8_Result_14()
        {
            // Arrange
            Calculator calc = new Calculator();

            // Act
            var result = calc.Sum(6, 8);

            // Assert
            Assert.Equal(14, result);
        }

        [Fact(DisplayName = "SumMaxAnd1", Skip = "Not Implemented")]
        public void Sum_MAX_and_1_Result_OverflowException()
        {
            Calculator calc = new Calculator();

            Assert.Throws<OverflowException>(() => calc.Sum(int.MaxValue, 1));
        }

        [Theory]
        [InlineData(5, 9, 14)]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 2)]
        [InlineData(-5, 2, -3)]
        public void Sum_Tests(int a, int b, int expectedResult)
        {
            Calculator calc = new Calculator();

            var result = calc.Sum(a, b);

            Assert.Equal(expectedResult, result);
        }
    }
}
