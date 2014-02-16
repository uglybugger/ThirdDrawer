using System.Text;
using NUnit.Framework;
using Shouldly;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ThirdDrawer.UnitTests.Base64Tests
{
    [TestFixture]
    public class WhenEncodingFoo
    {
        [Test]
        public void TheResultShouldBeCorrect()
        {
            var result = "foo".ToBase64(Encoding.UTF8);
            result.ShouldBe("Zm9v");
        }
    }
}