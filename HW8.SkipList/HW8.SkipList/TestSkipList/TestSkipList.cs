using HW8.SkipList;

namespace TestSkipList;

public class Tests
{
    [TestFixture]
    public class SkipListTests
    {
        [Test]
        public void Constructor_WhenListIsCreated_ItShouldBeEmpty()
        {
            var list = new SkipList<int>();
            Assert.That(list.Count, Is.EqualTo(0));
            Assert.That(list.IsReadOnly, Is.False);
        }

        [Test]
        public void Add_WhenItemsAreAdded_TheyShouldBeInSortedOrder()
        {
            var list = new SkipList<int>();
            list.Add(3);
            list.Add(1);
            list.Add(2);

            Assert.That(list.ToArray(), Is.EqualTo(new[] { 1, 2, 3 }));
        }

        [Test]
        public void Add_WhenNullItemIsAdded_ItShouldThrowArgumentNullException()
        {
            var list = new SkipList<string>();
            Assert.Throws<ArgumentNullException>(() => list.Add(null!));
        }

        [Test]
        public void Contains_WhenItemExists_ItShouldReturnTrue()
        {
            var list = new SkipList<int> { 1, 2, 3 };
            Assert.That(list.Contains(2), Is.True);
        }

        [Test]
        public void Contains_WhenItemDoesNotExist_ItShouldReturnFalse()
        {
            var list = new SkipList<int> { 1, 2, 3 };
            Assert.That(list.Contains(4), Is.False);
        }

        [Test]
        public void Contains_WhenItemIsNull_ItShouldThrowArgumentNullException()
        {
            var list = new SkipList<string> { "a", "b", "c" };
            Assert.Throws<ArgumentNullException>(() => list.Contains(null!));
        }

        [Test]
        public void Remove_WhenItemExists_ItShouldRemoveAndReturnTrue()
        {
            var list = new SkipList<int> { 1, 2, 3 };
            var result = list.Remove(2);

            Assert.That(result, Is.True);
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list.ToArray(), Is.EqualTo(new[] { 1, 3 }));
        }

        [Test]
        public void Remove_WhenItemDoesNotExist_ItShouldReturnFalse()
        {
            var list = new SkipList<int> { 1, 2, 3 };
            var result = list.Remove(4);

            Assert.That(result, Is.False);
            Assert.That(list.Count, Is.EqualTo(3));
        }

        [Test]
        public void Remove_WhenItemIsNull_ItShouldThrowArgumentNullException()
        {
            var list = new SkipList<string> { "a", "b", "c" };
            Assert.Throws<ArgumentNullException>(() => list.Remove(null!));
        }

        [Test]
        public void Clear_WhenCalled_ItShouldRemoveAllItems()
        {
            var list = new SkipList<int> { 1, 2, 3 };
            list.Clear();

            Assert.That(list.Count, Is.EqualTo(0));
            Assert.That(list.ToArray(), Is.Empty);
        }

        [Test]
        public void CopyTo_WhenCalled_ItShouldCopyItemsToArray()
        {
            var list = new SkipList<int> { 1, 2, 3 };
            var array = new int[3];
            list.CopyTo(array, 0);

            Assert.That(array, Is.EqualTo(new[] { 1, 2, 3 }));
        }

        [Test]
        public void CopyTo_NullArray_ThrowsArgumentNullException()
        {
            var list = new SkipList<int> { 1, 2, 3 };
            Assert.Throws<ArgumentNullException>(() => list.CopyTo(null!, 0));
        }

        [Test]
        public void CopyTo_InvalidIndex_ThrowsArgumentOutOfRangeException()
        {
            var list = new SkipList<int> { 1, 2, 3 };
            var array = new int[3];
            Assert.Throws<ArgumentOutOfRangeException>(() => list.CopyTo(array, -1));
        }

        [Test]
        public void CopyTo_InsufficientSpace_ThrowsArgumentException()
        {
            var list = new SkipList<int> { 1, 2, 3 };
            var array = new int[2];
            Assert.Throws<ArgumentException>(() => list.CopyTo(array, 0));
        }

        [Test]
        public void IndexOf_WhenItemExists_ItShouldReturnCorrectIndex()
        {
            var list = new SkipList<int> { 1, 2, 3 };
            Assert.That(list.IndexOf(2), Is.EqualTo(1));
        }

        [Test]
        public void IndexOf_WhenItemDoesNotExist_ItShouldReturnNegativeOne()
        {
            var list = new SkipList<int> { 1, 2, 3 };
            Assert.That(list.IndexOf(4), Is.EqualTo(-1));
        }

        [Test]
        public void IndexOf_WhenItemIsNull_ItShouldThrowArgumentNullException()
        {
            var list = new SkipList<string> { "a", "b", "c" };
            Assert.Throws<ArgumentNullException>(() => list.IndexOf(null!));
        }

        [Test]
        public void RemoveAt_WhenValidIndex_ItShouldRemoveItemAtThatIndex()
        {
            var list = new SkipList<int> { 1, 2, 3 };
            list.RemoveAt(1);

            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list.ToArray(), Is.EqualTo(new[] { 1, 3 }));
        }

        [Test]
        public void RemoveAt_WhenInvalidIndex_ItShouldThrowArgumentOutOfRangeException()
        {
            var list = new SkipList<int> { 1, 2, 3 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(3));
        }

        [Test]
        public void Indexer_Get_WhenIndexIsValid_ItShouldReturnCorrectItem()
        {
            var list = new SkipList<int> { 1, 2, 3 };
            Assert.That(list[1], Is.EqualTo(2));
        }

        [Test]
        public void Indexer_Get_InvalidIndex_ThrowsArgumentOutOfRangeException()
        {
            var list = new SkipList<int> { 1, 2, 3 };
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = list[-1]);
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = list[3]);
        }

        [Test]
        public void Indexer_Set_WhenIndexIsValid_ItShouldUpdateItemAtThatIndex()
        {
            var list = new SkipList<int> { 1, 2, 3 };
            list[1] = 4;

            Assert.That(list.ToArray(), Is.EqualTo(new[] { 1, 4, 3 }));
        }

        [Test]
        public void Indexer_Set_WhenIndexIsInvalid_ItShouldThrowArgumentOutOfRangeException()
        {
            var list = new SkipList<int> { 1, 2, 3 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list[-1] = 0);
            Assert.Throws<ArgumentOutOfRangeException>(() => list[3] = 4);
        }

        [Test]
        public void Insert_ThrowsNotSupportedException()
        {
            var list = new SkipList<int> { 1, 2, 3 };
            Assert.Throws<NotSupportedException>(() => list.Insert(1, 4));
        }

        [Test]
        public void Enumerator_EnumeratesItemsInOrder()
        {
            var list = new SkipList<int> { 3, 1, 2 };
            var result = new List<int>();
            foreach (var item in list)
            {
                result.Add(item);
            }

            Assert.That(result, Is.EqualTo(new[] { 1, 2, 3 }));
        }

        [Test]
        public void MultipleOperations_WhenPerformedInSequence_TheyShouldProduceExpectedResults()
        {
            var list = new SkipList<int>();
            
            // Add items
            list.Add(5);
            list.Add(3);
            list.Add(7);
            list.Add(1);
            
            // Check initial state
            Assert.That(list.Count, Is.EqualTo(4));
            Assert.That(list.ToArray(), Is.EqualTo(new[] { 1, 3, 5, 7 }));
            
            // Remove item
            list.Remove(3);
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list.ToArray(), Is.EqualTo(new[] { 1, 5, 7 }));
            
            // Indexer
            list[1] = 4;
            Assert.That(list.ToArray(), Is.EqualTo(new[] { 1, 4, 7 }));
            
            // Contains
            Assert.That(list.Contains(4), Is.True);
            Assert.That(list.Contains(3), Is.False);
            
            // Clear
            list.Clear();
            Assert.That(list.Count, Is.EqualTo(0));
        }
    }
}