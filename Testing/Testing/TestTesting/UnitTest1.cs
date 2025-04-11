namespace TestTesting;
using Testing;

public class Tests
{
    [Test]
    public void Test1()
    {
        var queue = new Queue<string>();
        queue.Enqueue("test", 1);
        Assert.That(queue.Empty, Is.False);
        Assert.That(queue.Dequeue(), Is.EqualTo("test"));
        Assert.That(queue.Empty, Is.True);
    }
    
    [Test]
    public void Test2()
    {
        var queue = new Queue<int>();
        Assert.That(queue.Empty, Is.True);
    }
    
    [Test]
    public void Test3()
    {
        var queue = new Queue<char>();
        queue.Enqueue('a', 10);
        queue.Enqueue('b', 333);
        queue.Enqueue('c', 220);
        
        Assert.That(queue.Dequeue(), Is.EqualTo('b'));
        Assert.That(queue.Dequeue(), Is.EqualTo('c'));
        Assert.That(queue.Dequeue(), Is.EqualTo('a'));
    }
    
    [Test]
    public void Test4()
    {
        var queue = new Queue<int>();
        queue.Enqueue(1, 1);
        queue.Enqueue(2, 1);
        queue.Enqueue(3, 1);
        
        Assert.That(queue.Dequeue(), Is.EqualTo(1));
        Assert.That(queue.Dequeue(), Is.EqualTo(2));
        Assert.That(queue.Dequeue(), Is.EqualTo(3));
    }
}