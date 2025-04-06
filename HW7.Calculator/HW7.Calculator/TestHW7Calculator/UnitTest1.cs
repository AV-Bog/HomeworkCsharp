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
    public void CheckingDigitsCombination()
    {
        calculator.NumberProses(1);
        calculator.NumberProses(1);
        if (calculator.CurrentValue != 11)
        {
            throw new AssertionException($"Expected 11, but got {calculator.CurrentValue}");
        }
    }

    [Test]
    public void CheckAdditionNumbersLessThan10()
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
    public void CheckAdditionNumbersGreaterThan10()
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
    public void CheckAdditionThenMultiplication()
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
    public void CheckDivisionBy0()
    {
        calculator.NumberProses(8);
        calculator.OperatorProses('/');
        calculator.NumberProses(0);
        Assert.Throws<DivideByZeroException>(() => calculator.Calculate());
    }
    
    [Test]
    public void CheckTwoPoints()
    {
        calculator.NumberProses(1);
        calculator.NumberProses(1);
        calculator.AddDot();
        calculator.NumberProses(1);
        Assert.Throws<InvalidOperationException>(() => calculator.AddDot());
    }
    
    [Test]
    public void CheckingNonIntegerCalculations()
    {
        calculator.NumberProses(1);
        calculator.NumberProses(1);
        calculator.AddDot();
        calculator.NumberProses(1);
        calculator.NumberProses(1);
        calculator.OperatorProses('+');
        calculator.NumberProses(1);
        if (calculator.Calculate() != 12.11)
        {
            throw new AssertionException($"Expected 12.11, but got {calculator.CurrentValue}");
        }
    }
}