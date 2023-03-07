namespace SE2_UnitTest
{
    public class UnitTest
    {
        [Theory]
        [InlineData("", 0)]
        [InlineData("1", 1)]
        [InlineData("1,2", 3)]
        [InlineData("1\n2", 3)]
        [InlineData("1,2\n3", 6)]
        [InlineData("1001,2", 2)]
        [InlineData("//;\n1;2", 3)]
        [InlineData("//[***]\n1***2***3", 6)]
        [InlineData("//[*][%]\n1*2%3", 6)]
        public void Add_ShouldReturnCorrectResult(string numbers, int expected)
        {
            // Arrange
            var calculator = new StringCalculator();

            // Act
            var result = calculator.Add(numbers);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("1,-2")]
        public void Add_ShouldThrowArgumentException_When_NegativeNumberIsPassed(string numbers)
        {
            // Arrange
            var calculator = new StringCalculator();

            // Act and Assert
            var ex = Assert.Throws<ArgumentException>(() => calculator.Add(numbers));
            Assert.Equal("negatives not allowed: -2", ex.Message);
        }
    }
}