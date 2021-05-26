using Xunit;

namespace PlayfairCipher.UnitTests
{
    public class PlayfairMessageTests
    {
        [Fact]
        public void Given_a_valid_string_Then_value_parses_correctly()
        {
            // Act
            var result = PlayfairMessage.Create("HELLO WORLD");

            // Assert
            Assert.Equal("HELXLOWORLD", result.Value);
        }
        
        [Fact]
        public void Given_string_with_j_Then_replacing_character_is_i()
        {
            // Act
            var result = PlayfairMessage.Create("JAY");

            // Assert
            Assert.Equal("IAY", result.Value);
        }
        
        [Fact]
        public void Given_string_with_two_identical_neighbors_Then_x_is_inserted_inbetween()
        {
            // Act
            var result = PlayfairMessage.Create("WHOOP");

            // Assert
            Assert.Equal("WHOXOP", result.Value);
        }

    }
}