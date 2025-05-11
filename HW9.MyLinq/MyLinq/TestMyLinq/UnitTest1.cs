// <copyright file="MyLinq.cs" author="AV-Bog">
// under MIT License
// </copyright>

using HW9.MyLinq;

namespace TestMyLinq;

public class Tests
{
    [Test]
    public void GetPrimes_IsLazy_DoesNotHangOnInfiniteSequence()
    {
        var primes = MyLinq.GetPrimes().TakeFirst(101).ToArray();
        var prime = primes[100];  // 101-е простое число
        Assert.That(MyLinq.IsPrime(prime), Is.True);
    }
    
    [Test]
    public void TakeFirst_ReturnsCorrectNumberOfElements()
    {
        var source = new[] { 1, 2, 3, 4, 5 };
        var result = source.TakeFirst(2).ToArray();
        Assert.That(result, Is.EqualTo(new[] { 1, 2 }));
    }

    [Test]
    public void TakeFirst_WhenFewerElements_ReturnsAllAvailable()
    {
        var source = new[] { 1, 2 };
        var result = source.TakeFirst(5).ToArray();
        Assert.That(result, Is.EqualTo(new[] { 1, 2 }));
    }
    
    [Test]
    public void MySkip_SkipsCorrectNumberOfElements()
    {
        var source = new[] { 1, 2, 3, 4, 5 };
        var result = source.MySkip(2).ToArray();
        Assert.That(result, Is.EqualTo(new[] { 3, 4, 5 }));
    }

    [Test]
    public void MySkip_WhenSkipZero_ReturnsFullSequence()
    {
        var source = new[] { 1, 2, 3 };
        var result = source.MySkip(0).ToArray();
        Assert.That(result, Is.EqualTo(source));
    }
    
}
