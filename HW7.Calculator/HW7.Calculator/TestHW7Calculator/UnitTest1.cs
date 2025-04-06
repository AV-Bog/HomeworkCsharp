using HW7.Calculator;
using NUnit.Framework;

namespace TestHW7Calculator;

public class TestsCalculatorEngine
{
    private CalculatorEngine calculator;
    
    [SetUp]
    public void Setup()
    {
        calculator = new CalculatorEngine();
        calculator.Clear();
    }

    [Test]
    public void Test1()
    {
        calculator.NumberProses(1);
        calculator.NumberProses(1);
        if (calculator.CurrentValue != 11)
        {
            throw new AssertionException($"Expected 11, but got {calculator.CurrentValue}");
        }
    }

    [Test]
    public void Test2()
    {
        calculator.NumberProses(7);
        calculator.OperatorProses('+');
        calculator.NumberProses(3);
        if (calculator.Calculate() != 10)
        {
            throw new AssertionException($"Expected 10, but got {calculator.CurrentValue}");
        }
    }
    
    [Test]
    public void Test3()
    {
        calculator.NumberProses(7);
        calculator.NumberProses(7);
        calculator.OperatorProses('+');
        calculator.NumberProses(2);
        calculator.NumberProses(3);
        if (calculator.Calculate() != 100)
        {
            throw new AssertionException($"Expected 100, but got {calculator.CurrentValue}");
        }
    }
    
    [Test]
    public void Test4()
    {
        calculator.NumberProses(8);
        calculator.OperatorProses('+');
        calculator.NumberProses(2);
        calculator.OperatorProses('*');
        calculator.NumberProses(3);
        if (calculator.Calculate() != 30)
        {
            throw new AssertionException($"Expected 30, but got {calculator.CurrentValue}");
        }
    }
    
    [Test]
    public void Test5()
    {
        calculator.NumberProses(8);
        calculator.OperatorProses('/');
        calculator.NumberProses(0);
        Assert.Throws<DivideByZeroException>(() => calculator.Calculate());
    }
}