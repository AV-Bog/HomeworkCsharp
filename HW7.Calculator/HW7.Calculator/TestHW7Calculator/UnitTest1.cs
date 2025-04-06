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
        calculator.EventHandler("C");
    }

    [Test]
    public void CheckingDigitsCombination()
    {
        calculator.EventHandler("1");
        calculator.EventHandler("1");
        if (calculator.CurrentValue != 11)
        {
            throw new AssertionException($"Expected 11, but got {calculator.CurrentValue}");
        }
    }

    [Test]
    public void CheckAdditionNumbersLessThan10()
    {
        calculator.EventHandler("7");
        calculator.EventHandler("+");
        calculator.EventHandler("3");
        calculator.EventHandler("=");
        if (calculator.CurrentValue != 10)
        {
            throw new AssertionException($"Expected 10, but got {calculator.CurrentValue}");
        }
    }
    
    [Test]
    public void CheckAdditionNumbersGreaterThan10()
    {
        calculator.EventHandler("7");
        calculator.EventHandler("7");
        calculator.EventHandler("+");
        calculator.EventHandler("2");
        calculator.EventHandler("3");
        calculator.EventHandler("=");
        if (calculator.CurrentValue != 100)
        {
            throw new AssertionException($"Expected 100, but got {calculator.CurrentValue}");
        }
    }
    
    [Test]
    public void CheckAdditionThenMultiplication()
    {
        calculator.EventHandler("8");
        calculator.EventHandler("+");
        calculator.EventHandler("2");
        calculator.EventHandler("*");
        calculator.EventHandler("3");
        calculator.EventHandler("=");
        if (calculator.CurrentValue != 30)
        {
            throw new AssertionException($"Expected 30, but got {calculator.CurrentValue}");
        }
    }
    
    [Test]
    public void CheckDivisionBy0()
    {
        calculator.EventHandler("8");
        calculator.EventHandler("/");
        calculator.EventHandler("0");
        Assert.Throws<DivideByZeroException>(() => calculator.EventHandler("="));
    }
    
    [Test]
    public void CheckTwoPoints()
    {
        calculator.EventHandler("1");
        calculator.EventHandler("1");
        calculator.EventHandler(".");
        calculator.EventHandler("1");
        Assert.Throws<InvalidOperationException>(() => calculator.EventHandler("."));
    }
    
    [Test]
    public void CheckingNonIntegerCalculations()
    {
        calculator.EventHandler("1");
        calculator.EventHandler("1");
        calculator.EventHandler(".");
        calculator.EventHandler("1");
        calculator.EventHandler("1");
        calculator.EventHandler("+");
        calculator.EventHandler("1");
        calculator.EventHandler("=");
        if (calculator.CurrentValue != 12.11)
        {
            throw new AssertionException($"Expected 12.11, but got {calculator.CurrentValue}");
        }
    }
    
    [Test]
    public void CheckIncorrectInput()
    {
        Assert.Throws<ArgumentException>(() => calculator.EventHandler("a"));
    }
}