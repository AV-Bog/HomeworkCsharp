// <copyright file="TestCalculator.cs" author="AV-Bog">
// under MIT License
// Free use, modification, and distribution are permitted,
// provided that the attribution and license notice are preserved.
// more detailed: https://github.com/AV-Bog/HomeworkCsharp/blob/main/LICENSE
// </copyright>

using HW72.Calculator;

namespace Test._2.Calculator;

using NUnit.Framework;

public class TestsCalculatorEngine
{
    private CalculatorEngine calculator;

    [SetUp]
    public void Setup()
    {
        this.calculator = new CalculatorEngine();
        this.calculator.EventHandler("C");
    }

    private const double Tolerance = 0;

    [Test]
    public void CheckingDigitsCombination()
    {
        this.calculator.EventHandler("1");
        this.calculator.EventHandler("1");
        if (Math.Abs(this.calculator.CurrentValue - 11) > Tolerance)
        {
            throw new AssertionException($"Expected 11, but got {this.calculator.CurrentValue}");
        }
    }

    [Test]
    public void CheckAdditionNumbersLessThan10()
    {
        this.calculator.EventHandler("7");
        this.calculator.EventHandler("+");
        this.calculator.EventHandler("3");
        this.calculator.EventHandler("=");
        if (Math.Abs(this.calculator.CurrentValue - 10) > Tolerance)
        {
            throw new AssertionException($"Expected 10, but got {this.calculator.CurrentValue}");
        }
    }

    [Test]
    public void CheckAdditionNumbersGreaterThan10()
    {
        this.calculator.EventHandler("7");
        this.calculator.EventHandler("7");
        this.calculator.EventHandler("+");
        this.calculator.EventHandler("2");
        this.calculator.EventHandler("3");
        this.calculator.EventHandler("=");
        if (Math.Abs(this.calculator.CurrentValue - 100) > Tolerance)
        {
            throw new AssertionException($"Expected 100, but got {this.calculator.CurrentValue}");
        }
    }

    [Test]
    public void CheckAdditionThenMultiplication()
    {
        this.calculator.EventHandler("8");
        this.calculator.EventHandler("+");
        this.calculator.EventHandler("2");
        this.calculator.EventHandler("*");
        this.calculator.EventHandler("3");
        this.calculator.EventHandler("=");
        if (Math.Abs(this.calculator.CurrentValue - 30) > Tolerance)
        {
            throw new AssertionException($"Expected 30, but got {this.calculator.CurrentValue}");
        }
    }

    [Test]
    public void CheckDivisionBy0()
    {
        this.calculator.EventHandler("8");
        this.calculator.EventHandler("/");
        this.calculator.EventHandler("0");
        Assert.Throws<DivideByZeroException>(() => this.calculator.EventHandler("="));
    }

    [Test]
    public void CheckTwoPoints()
    {
        this.calculator.EventHandler("1");
        this.calculator.EventHandler("1");
        this.calculator.EventHandler(".");
        this.calculator.EventHandler("1");
        Assert.Throws<InvalidOperationException>(() => this.calculator.EventHandler("."));
    }

    [Test]
    public void CheckingNonIntegerCalculations()
    {
        this.calculator.EventHandler("1");
        this.calculator.EventHandler("1");
        this.calculator.EventHandler(".");
        this.calculator.EventHandler("1");
        this.calculator.EventHandler("1");
        this.calculator.EventHandler("+");
        this.calculator.EventHandler("1");
        this.calculator.EventHandler("=");
        if (Math.Abs(this.calculator.CurrentValue - 12.11) > Tolerance)
        {
            throw new AssertionException($"Expected 12.11, but got {this.calculator.CurrentValue}");
        }
    }

    [Test]
    public void CheckIncorrectInput()
    {
        Assert.Throws<ArgumentException>(() => this.calculator.EventHandler("a"));
    }
}