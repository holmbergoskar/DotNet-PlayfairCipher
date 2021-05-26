using Xunit;

namespace PlayfairCipher.UnitTests
{
    public class PlayfairCipherTests
    {
         [Theory]
         [InlineData("AQuickBrownFox", "IUQABLDPPVUNQV")]
         [InlineData("JumpedOverTheLazyDogs", "AQKRBFVTDOHICGFASMVOIY")]
         [InlineData("Playfair ciphers are fun", "QKSZNFSQLCWBDOATODNZLZ")]
         [InlineData("Hooray for cryptology", "TPPUSZEUQDYSOHQGVOZY")]
         public void Given_a_valid_key_without_space_Then_it_should_cipher_correctly(string message, string expected)
         {
             // Arrange
             const string key = "ThisIsATest";
             var playfairMessage = PlayfairMessage.Create(message);
             var cipher = new PlayfairCipher();
             cipher.SetKeyTable(KeyTable.Create(key));
             
             // Act
             var result = cipher.Encrypt(playfairMessage);
             
             // Assert
             Assert.Equal(expected, result);
         }
         
         [Theory]
         [InlineData("Meet me at eight o clock", "LOLELOIANTFEUEGXNDCB")]
         [InlineData("Signed Agent Double O Eight", "TUCEOGTKROUGMOXBRHNTFEIL")]
         public void Given_a_valid_key_with_space_Then_it_should_cipher_correctly(string message, string expected)
         {
             // Arrange
             const string key = "Just another example";
             var playfairMessage = PlayfairMessage.Create(message);
             var cipher = new PlayfairCipher();
             cipher.SetKeyTable(KeyTable.Create(key));
             
             // Act
             var result = cipher.Encrypt(playfairMessage);
             
             // Assert
             Assert.Equal(expected, result);
         }
         
         [Fact]
         public void Given_a_valid_key_and_empty_message_Then_it_should_cipher_correctly()
         {
             // Arrange
             const string key = "ThisIsATest";
             var playfairMessage = PlayfairMessage.Create(" ");
             var cipher = new PlayfairCipher();
             cipher.SetKeyTable(KeyTable.Create(key));
             
             // Act
             var result = cipher.Encrypt(playfairMessage);
             
             // Assert
             Assert.Equal("", result);
         }
        
    }
}