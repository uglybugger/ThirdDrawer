using System;
using System.Text;
using NUnit.Framework;
using Shouldly;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ThirdDrawer.UnitTests.StringExtensionTests
{
    [TestFixture]
    public class StringExtensions
    {
        [Test]
        public void TheResultShouldBeCorrect()
        {
            var testcase1Result = "foo".EqualCaseInsensitive("foO");
            testcase1Result.ShouldBe(true);

            var testcase2Result = "Foo".EqualCaseInsensitive("foO");
            testcase2Result.ShouldBe(true);

            var testcase3Result = "Fo0".EqualCaseInsensitive("foO");
            testcase3Result.ShouldBe(false);

        }

        [Test]
        public void EqualCaseInsensitiveShouldReturnTrueWhenPassingEmptryString()
        {
            var testcase1Result = string.Empty.EqualCaseInsensitive("");
            testcase1Result.ShouldBe(true);
        }

        [Test]
        public void HasValShouldReturnFalseWhenPassingEmptryString()
        {
            var testcase1Result = string.Empty.HasVal();
            testcase1Result.ShouldBe(false);
        }

        [Test]
        public void HasValShouldReturnFalseWhenPassingNull()
        {
            string param = null;
            var testcase1Result = param.HasVal();
            testcase1Result.ShouldBe(false);
        }

        [Test]
        public void NoValShouldReturnTrueWhenPassingEmptryString()
        {
            var testcase1Result = string.Empty.NoVal();
            testcase1Result.ShouldBe(true);
        }

        [Test]
        public void NoValShouldReturnTrueWhenPassingNull()
        {
            string param = null;
            var testcase1Result = param.NoVal();
            testcase1Result.ShouldBe(true);
        }

    }
}
