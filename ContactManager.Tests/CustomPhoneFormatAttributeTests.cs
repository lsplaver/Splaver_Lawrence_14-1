using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ContactManager.Tests
{
    public class CustomPhoneFormatAttributeTests
    {
        [Fact]
        public void CustomPhoneFormatAttributeIsValid()
        {
            var value = "(123)-456-7890";
            var attribute = new CustomPhoneFormatAttribute();

            var result = attribute.IsValid(value);

            Assert.True(result);
        }

        [Fact]
        public void CustomPhoneFormatAttributeIsNotValidIncorrectFormat()
        {
            var value = "1234567890zxcv";
            var attribute = new CustomPhoneFormatAttribute();

            var result = attribute.IsValid(value);

            Assert.False(result);
        }

        [Fact]
        public void CustomPhoneFormatAttributeIsValidValueNull()
        {
            string value = null;
            var attribute = new CustomPhoneFormatAttribute();

            var result = attribute.IsValid(value);

            Assert.True(result);
        }

        [Fact]
        public void CustomPhoneFormatAttributeIsNotValidNonStringValue()
        {
            bool value = true;
            var attribute = new CustomPhoneFormatAttribute();

            var result = attribute.IsValid(value);

            Assert.False(result);
        }
    }
}
