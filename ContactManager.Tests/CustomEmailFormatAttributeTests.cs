using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ContactManager.Tests
{
    public class CustomEmailFormatAttributeTests
    {
        [Fact]
        public void CustomEmailFormatAttributeIsValid()
        {
            var value = "name@example.com";
            var attribute = new CustomEmailFormatAttribute();

            var result = attribute.IsValid(value);

            Assert.True(result);
        }

        [Fact]
        public void CustomEmailFormatAttributeIsNotValidIncorrectFormat1()
        {
            var value = "1234567890zxcv";
            var attribute = new CustomEmailFormatAttribute();

            var result = attribute.IsValid(value);

            Assert.False(result);
        }

        [Fact]
        public void CustomEmailFormatAttributeIsNotValidIncorrectFormat2()
        {
            var value = "name@";
            var attribute = new CustomEmailFormatAttribute();

            var result = attribute.IsValid(value);

            Assert.False(result);
        }

        [Fact]
        public void CustomEmailFormatAttributeIsNotValidIncorrectFormat3()
        {
            var value = "@example.com";
            var attribute = new CustomEmailFormatAttribute();

            var result = attribute.IsValid(value);

            Assert.False(result);
        }

        [Fact]
        public void CustomEmailFormatAttributeIsNotValidIncorrectFormat4()
        {
            var value = "example.com";
            var attribute = new CustomEmailFormatAttribute();

            var result = attribute.IsValid(value);

            Assert.False(result);
        }

        [Fact]
        public void CustomEmailFormatAttributeIsNotValidIncorrectFormat5()
        {
            var value = "@.";
            var attribute = new CustomEmailFormatAttribute();

            var result = attribute.IsValid(value);

            Assert.False(result);
        }

        [Fact]
        public void CustomEmailFormatAttributeIsNotValidIncorrectFormat6()
        {
            var value = "name@example";
            var attribute = new CustomEmailFormatAttribute();

            var result = attribute.IsValid(value);

            Assert.False(result);
        }

        [Fact]
        public void CustomEmailFormatAttributeIsNotValidIncorrectFormat7()
        {
            var value = "name@example.";
            var attribute = new CustomEmailFormatAttribute();

            var result = attribute.IsValid(value);

            Assert.False(result);
        }

        [Fact]
        public void CustomEmailFormatAttributeIsValidValueNull()
        {
            string value = null;
            var attribute = new CustomEmailFormatAttribute();

            var result = attribute.IsValid(value);

            Assert.True(result);
        }

        [Fact]
        public void CustomEmailFormatAttributeIsNotValidNonStringValue()
        {
            bool value = true;
            var attribute = new CustomEmailFormatAttribute();

            var result = attribute.IsValid(value);

            Assert.False(result);
        }
    }
}
