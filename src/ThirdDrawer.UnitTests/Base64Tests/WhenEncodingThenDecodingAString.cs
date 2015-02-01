using System.Text;
using NUnit.Framework;
using Shouldly;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ThirdDrawer.UnitTests.Base64Tests
{
    [TestFixture]
    public class WhenEncodingThenDecodingAString
    {
        [Test]
        public void TheOutputShouldMatchTheInput()
        {
            const string message = "I've got a lovely bunch of coconuts!";

            var result = message.ToBase64(Encoding.UTF8).FromBase64(Encoding.UTF8);

            result.ShouldBe(message);
        }
    }
}