using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shouldly;
using ThirdDrawer.Extensions.CollectionExtensionMethods;

namespace ThirdDrawer.UnitTests.CollectionExtensionTests
{
    [TestFixture]
    public sealed class WhenPartitioningACollectionOfNumbersByParity
    {
        private class TestCases : IEnumerable<TestCaseData>
        {
            public IEnumerator<TestCaseData> GetEnumerator()
            {
                yield return new TestCaseData(new[] { 2, 3, 4, 5, 7 }, new[] { 2, 4 }, new[] { 3, 5, 7 });
                yield return new TestCaseData(new[] { 3, 5, 7 }, Enumerable.Empty<int>(), new[] { 3, 5, 7 });
                yield return new TestCaseData(new[] { 2, 4, 6 }, new[] { 2, 4, 6 }, Enumerable.Empty<int>());
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        [Test]
        [TestCaseSource(typeof(TestCases))]
        public void TheResultShouldBeCorrect(IEnumerable<int> source, IEnumerable<int> even, IEnumerable<int> odd)
        {
            var partition = source.Partition(i => i % 2 == 0);
            partition.Satisfies.ShouldBe(even);
            partition.DoesNotSatisfy.ShouldBe(odd);
        }

        public void ShouldThrowWhenSourceCollectionIsNull()
        {
            var ex = Should.Throw<ArgumentNullException>(() => ((IEnumerable<int>)null).Partition(i => i % 2 == 0));
            ex.ParamName.ShouldBe("source");
        }
    }
}
