using Xunit;

namespace PlayfairCipher.UnitTests
{
    public class KeyTableTests
    {
        [Fact]
        public void Given_a_valid_string_Then_value_parses_correctly()
        {
            // Act
            var result = KeyTable.Create("Playfair");

            // Assert
            Assert.Equal("PLAYF", string.Concat(result.Value[0]));
            Assert.Equal("IRBCD", string.Concat(result.Value[1]));
            Assert.Equal("EGHKM", string.Concat(result.Value[2]));
            Assert.Equal("NOQST", string.Concat(result.Value[3]));
            Assert.Equal("UVWXZ", string.Concat(result.Value[4]));
        }
    }
}