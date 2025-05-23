// <copyright file="Tests.cs" author="AV-Bog">
// under MIT License
// </copyright>

using Testing3;

namespace TestTesting3;

public class Tests
{
    [TestFixture]
    public class ZeroCountesTests
    {
        [Test]
        public void CountNulls_ListIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                ZeroCountes.CountNulls<int>(null, new IntNullChecker()));
        }

        [Test]
        public void CountNulls_NullCheckerIsNull_ThrowsArgumentNullException()
        {
            var list = new MyList<int>();
            Assert.Throws<ArgumentNullException>(() =>
                ZeroCountes.CountNulls(list, null));
        }
        
        [Test]
        public void CountNulls_IntListWithZeros_ReturnsCorrectCount()
        {
            var list = new MyList<int> { 1, 0, 2, 0, 0 };
            var checker = new IntNullChecker();

            var result = ZeroCountes.CountNulls(list, checker);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void CountNulls_StringListWithNulls_ReturnsCorrectCount()
        {
            var list = new MyList<string> { "a", null, "b", null, "c" };
            var checker = new StringNullChecker();

            var result = ZeroCountes.CountNulls(list, checker);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void CountNulls_EmptyList_ReturnsZero()
        {
            var list = new MyList<object>();
            var checker = new ObjectNullChecker();

            var result = ZeroCountes.CountNulls(list, checker);

            Assert.That(result, Is.EqualTo(0));
        }

        private class IntNullChecker : ZeroCountes.INullChecker<int>
        {
            public bool IsNull(int item) => item == 0;
        }

        private class StringNullChecker : ZeroCountes.INullChecker<string>
        {
            public bool IsNull(string item) => item == null;
        }

        private class ObjectNullChecker : ZeroCountes.INullChecker<object>
        {
            public bool IsNull(object item) => item == null;
        }
    }

    [TestFixture]
    public class MyListTests
    {
        [Test]
        public void Add_Items_IncreasesCount()
        {
            var list = new MyList<int>();

            list.Add(1);
            list.Add(2);

            Assert.That(list.Count, Is.EqualTo(2));
        }

        [Test]
        public void Indexer_ValidIndex_ReturnsItem()
        {
            var list = new MyList<string> { "test" };

            var result = list[0];

            Assert.That(result, Is.EqualTo("test"));
        }

        [Test]
        public void Indexer_NegativeIndex_ThrowsIndexOutOfRangeException()
        {
            var list = new MyList<int> { 1 };

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var _ = list[-1];
            });
        }

        [Test]
        public void Indexer_IndexEqualToCount_ThrowsIndexOutOfRangeException()
        {
            var list = new MyList<int> { 1 };

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var _ = list[1];
            });
        }

        [Test]
        public void GetEnumerator_EnumeratesAllItems()
        {
            var list = new MyList<int> { 1, 2, 3 };
            int sum = 0;

            foreach (var item in list)
            {
                sum += item;
            }

            Assert.That(sum, Is.EqualTo(6));
        }

        [Test]
        public void List_AutoResizesWhenCapacityReached()
        {
            var list = new MyList<int>();
            int initialCapacity = 4;

            for (int i = 0; i < initialCapacity + 1; i++)
            {
                list.Add(i);
            }

            Assert.That(initialCapacity + 1, Is.EqualTo(list.Count));
        }
    }
}