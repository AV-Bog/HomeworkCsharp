using HW6.MapFilterFold;
namespace TestMapFilterFold;

public class Tests
{
    [Test]
    public void CheckFunctionMapWithNumbers()
    {
        List<int> list = new List<int> { 1, 2, 3 };
        var rez = Functions.Map(list, x => x * 2);
        Assert.That(rez, Is.EqualTo(new List<int> { 2, 4, 6 }));
    }
    
    [Test]
    public void CheckFunctionMapWithStrings()
    {
        List<string> list = new List<string> { "qwer", "Cruel", "Du" };
        var rez = Functions.Map(list, x => x + "ty");
        Assert.That(rez, Is.EqualTo(new List<string> { "qwerty", "Cruelty", "Duty" }));
    }
    
    [Test]
    public void CheckFunctionMapWithNullValue()
    {
        Assert.Throws<ArgumentNullException>(
            () => Functions.Map<int, int>(null, x => x)
        );
    }

    [Test]
    public void CheckFunctionMapWithNullFunction()
    {
        var list = new List<int> { 1, 2, 3 };
    
        Assert.Throws<DivideByZeroException>(
            () => Functions.Map(list, x => x / 0)
        );
    }
    
    [Test]
    public void CheckFunctionFilterWithNotIntegers()
    {
        List<double> list = new List<double> { 1.1 , 2.3, 2, 3, 4.0 };
        var rez = Functions.Filter(list, x => x % 2 == 0);
        Assert.That(rez, Is.EqualTo(new List<double> { 2, 4 }));
    }
    
    [Test]
    public void CheckFunctionFilterWithStrings()
    {
        List<string> list = new List<string> { "aa", "a", "bbb", "cc" };
        var rez = Functions.Filter(list, x => x.Contains("a") );
        Assert.That(rez, Is.EqualTo(new List<string> { "aa", "a" }));
    }
    
    [Test]
    public void CheckFunctionFilterWithNullList()
    {
        Assert.Throws<ArgumentNullException>(
            () => Functions.Filter<int>(null, x => x > 0)
        );
    }

    [Test]
    public void CheckFunctionFilterWithNullFilter()
    {
        var list = new List<string> { "a", "b", "c" };
        Assert.Throws<ArgumentNullException>(
            () => Functions.Filter(list, null)
        );
    }
    
    [Test]
    public void CheckFunctionFoldWithNumbers()
    {
        var rez = Functions.Fold(new List<int>() { 1, 2, 3 }, 1, (acc, elem) => acc * elem);
        Assert.That(rez, Is.EqualTo(6));
    }
    
    [Test]
    public void CheckFunctionFoldWithStrings()
    {
        var rez = Functions.Fold(new List<string>() { "a", "bb", "ccc" }, "D", (acc, elem) => acc + elem);
        Assert.That(rez, Is.EqualTo(new string ("Dabbccc")));
    }
    
    [Test]
    public void CheckFunctionFoldWithNullList()
    {
        Assert.Throws<ArgumentNullException>(
            () => Functions.Fold<int, int>(null, 0, (acc, x) => acc + x)
        );
    }

    [Test]
    public void CheckFunctionFoldWithNullFunction()
    {
        var list = new List<int> { 1, 2, 3 };
    
        Assert.Throws<ArgumentNullException>(
            () => Functions.Fold(list, 0, null)
        );
    }
}