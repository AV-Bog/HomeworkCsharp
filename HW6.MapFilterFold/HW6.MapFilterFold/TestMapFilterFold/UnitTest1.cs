using HW6.MapFilterFold;
namespace TestMapFilterFold;

public class Tests
{
    [Test]
    public void Test()
    {
        List<int> list = new List<int> { 1, 2, 3 };
        var rez = Functions.Map(list, x => x * 2);
        
        Assert.That(rez, Is.EqualTo(new List<int> { 2, 4, 6 }));
    }
}