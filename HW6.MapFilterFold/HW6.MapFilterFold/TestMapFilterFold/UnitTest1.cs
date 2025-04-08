using HW6.MapFilterFold;
namespace TestMapFilterFold;

public class Tests
{
    [Test]
    public void Test1()
    {
        List<int> list = new List<int> { 1, 2, 3 };
        var rez = Functions.Map(list, x => x * 2);
        
        Assert.That(rez, Is.EqualTo(new List<int> { 2, 4, 6 }));
    }
    
    [Test]
    public void Test2()
    {
        List<int> list = new List<int> { 1, 2, 3, 4 };
        var rez = Functions.Filter(list, x => x % 2 == 0);
        
        Assert.That(rez, Is.EqualTo(new List<int> { 2, 4 }));
    }
    
    [Test]
    public void Test3()
    {
        var rez = Functions.Fold(new List<int>() { 1, 2, 3 }, 1, (acc, elem) => acc * elem);
        
        Assert.That(rez, Is.EqualTo(6));
    }
    
    [Test]
    public void Test4()
    {
        var rez = Functions.Fold(new List<string>() { "a", "bb", "ccc" }, "D", (acc, elem) => acc + elem);
        Assert.That(rez, Is.EqualTo(new string ("Dabbccc")));
    }
}