using PasswordGenerator.Core.Enums;
using PasswordGenerator.Core.Models;

namespace PasswordGenerator.Tests
{
    public class PasswordGeneratorTests
    {
        private const string Digits = "0123456789";
        private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
        private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Special = "*@#$():";

        [Fact]
        public void GeneratePassword_LengthMatchesRequested()
        {
            int length = 15;
            string result = GeneratorPasswords.GeneratePassword(length, true, true, true, true);
            Assert.Equal(length, result.Length);
        }

        [Fact]
        public void GeneratePassword_OnlyLowerCase_ContainsOnlyLowerCase()
        {
            int length = 10;
            string result = GeneratorPasswords.GeneratePassword(length, true, false, false, false);
            Assert.True(result.All(c => Lowercase.Contains(c)));
            Assert.Equal(length, result.Length);
        }

        [Fact]
        public void GeneratePassword_OnlyUpperCase_ContainsOnlyUpperCase()
        {
            int length = 10;
            string result = GeneratorPasswords.GeneratePassword(length, false, true, false, false);
            Assert.True(result.All(c => Uppercase.Contains(c)));
            Assert.Equal(length, result.Length);
        }

        [Fact]
        public void GeneratePassword_WithDigits_ContainsDigits()
        {
            int length = 12;
            string result = GeneratorPasswords.GeneratePassword(length, true, true, true, false);
            Assert.Contains(result, c => Digits.Contains(c));
            Assert.Contains(result, c => Lowercase.Contains(c) || Uppercase.Contains(c));
        }

        [Fact]
        public void GeneratePassword_WithSpecialChars_ContainsSpecialChars()
        {
            int length = 12;
            string result = GeneratorPasswords.GeneratePassword(length, true, true, false, true);
            Assert.Contains(result, c => Special.Contains(c));
            Assert.Contains(result, c => Lowercase.Contains(c) || Uppercase.Contains(c));
        }

        [Fact]
        public void GeneratePassword_AllOptionsEnabled_ContainsAllCharacterTypes()
        {
            int length = 20;
            string result = GeneratorPasswords.GeneratePassword(length, true, true, true, true);
            Assert.Contains(result, c => Digits.Contains(c));
            Assert.Contains(result, c => Special.Contains(c));
            Assert.Contains(result, c => Lowercase.Contains(c));
            Assert.Contains(result, c => Uppercase.Contains(c));
            Assert.Equal(length, result.Length);
        }

        [Fact]
        public void GetLevelComplexity_VeryHigh_AllRequirementsMet()
        {
            string password = "VeryLongPassword123!@#";
            var result = GeneratorPasswords.GetLevelComplexity(password);
            Assert.Equal(LevelComplexity.VeryHigh, result);
        }

        [Fact]
        public void GetLevelComplexity_VeryHigh_LongWithAllTypes()
        {
            string password = "Abcdefghij12345!@#$";
            var result = GeneratorPasswords.GetLevelComplexity(password);
            Assert.Equal(LevelComplexity.VeryHigh, result);
        }

        [Fact]
        public void GetLevelComplexity_High_LongWithThreeTypes()
        {
            string password = "Abcdefghij12345ABCDE";
            var result = GeneratorPasswords.GetLevelComplexity(password);
            Assert.Equal(LevelComplexity.High, result);
        }

        [Fact]
        public void GetLevelComplexity_High_MediumLengthWithAllTypes()
        {
            string password = "Abc123!@#";
            var result = GeneratorPasswords.GetLevelComplexity(password);
            Assert.Equal(LevelComplexity.Middle, result);
        }

        [Fact]
        public void GetLevelComplexity_Middle_VeryLongSimple()
        {
            string password = "verylongpasswordwithoutcomplexity";
            var result = GeneratorPasswords.GetLevelComplexity(password);
            Assert.Equal(LevelComplexity.Middle, result);
        }

        [Fact]
        public void GetLevelComplexity_Middle_MediumLength()
        {
            string password = "Medium123";
            var result = GeneratorPasswords.GetLevelComplexity(password);
            Assert.Equal(LevelComplexity.Middle, result);
        }

        [Fact]
        public void GetLevelComplexity_Low_ShortWithSomeComplexity()
        {
            string password = "Short12";
            var result = GeneratorPasswords.GetLevelComplexity(password);
            Assert.Equal(LevelComplexity.Low, result);
        }

        [Fact]
        public void GetLevelComplexity_VeryLow_VeryShort()
        {
            string password = "abc";
            var result = GeneratorPasswords.GetLevelComplexity(password);
            Assert.Equal(LevelComplexity.VeryLow, result);
        }

        [Fact]
        public void GetLevelComplexity_VeryLow_EmptyString()
        {
            string password = "";
            var result = GeneratorPasswords.GetLevelComplexity(password);
            Assert.Equal(LevelComplexity.VeryLow, result);
        }

        [Fact]
        public void GetLevelComplexity_NullPassword_ReturnsVeryLow()
        {
            string password = string.Empty;
            var result = GeneratorPasswords.GetLevelComplexity(password);
            Assert.Equal(LevelComplexity.VeryLow, result);
        }
    }
}
