using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using ThirdDrawer.Extensions.TypeExtensionMethods;

namespace ThirdDrawer.UnitTests.TypeExtensionTests
{
    [TestFixture]
    public class WhenLookingForClosedTypesOfAnOpenGeneric
    {
        private class TestCases : IEnumerable<TestCaseData>
        {
            public IEnumerator<TestCaseData> GetEnumerator()
            {
                yield return new TestCaseData(typeof (ISomeGenericInterface<>), typeof (ISomeGenericInterface<>), false);
                yield return new TestCaseData(typeof (ISomeGenericInterface<int>), typeof (ISomeGenericInterface<>), true);
                yield return new TestCaseData(typeof (ISomeGenericInterface<string>), typeof (ISomeGenericInterface<>), true);
                yield return new TestCaseData(typeof (SomeClass), typeof (ISomeGenericInterface<>), true);
                yield return new TestCaseData(typeof (SomeDerivedClass), typeof (ISomeGenericInterface<>), true);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public class SomeDerivedClass : SomeClass
        {
        }

        public class SomeClass : ISomeGenericInterface<int>
        {
        }

        public interface ISomeGenericInterface<T>
        {
        }

        [Test]
        [TestCaseSource(typeof (TestCases))]
        public void WeShouldGetTheRightAnswer(Type candidateType, Type openGenericType, bool expectedResult)
        {
            candidateType.IsClosedTypeOf(openGenericType).ShouldBe(expectedResult);
        }
    }
}