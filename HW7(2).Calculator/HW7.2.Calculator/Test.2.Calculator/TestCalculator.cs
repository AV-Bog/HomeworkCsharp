// <copyright file="TestCalculator.cs" author="AV-Bog">
// under MIT License
// </copyright>

using HW7._2.Calculator;

namespace Test._2.Calculator;

using NUnit.Framework;

public class TestsCalculatorEngine
{
    private CalculatorEngine _calculator;

    [SetUp]
    public void Setup()
    {
        _calculator = new CalculatorEngine();
        _calculator.EventHandler("C");
    }

    private const double TOLERANCE = 0;

    [Test]
    public void CheckingDigitsCombination()
    {
        _calculator.EventHandler("1");
        _calculator.EventHandler("1");
        if (Math.Abs(_calculator.CurrentValue - 11) > TOLERANCE)
        {
            throw new AssertionException($"Expected 11, but got {_calculator.CurrentValue}");
        }
    }

    [Test]
    public void CheckAdditionNumbersLessThan10()
    {
        _calculator.EventHandler("7");
        _calculator.EventHandler("+");
        _calculator.EventHandler("3");
        _calculator.EventHandler("=");
        if (Math.Abs(_calculator.CurrentValue - 10) > TOLERANCE)
        {
            throw new AssertionException($"Expected 10, but got {_calculator.CurrentValue}");
        }
    }

    [Test]
    public void CheckAdditionNumbersGreaterThan10()
    {
        this._calculator.EventHandler("7");
        this._calculator.EventHandler("7");
        this._calculator.EventHandler("+");
        _calculator.EventHandler("2");
        _calculator.EventHandler("3");
        _calculator.EventHandler("=");
        if (Math.Abs(_calculator.CurrentValue - 100) > TOLERANCE)
        {
            throw new AssertionException($"Expected 100, but got {_calculator.CurrentValue}");
        }
    }

    [Test]
    public void CheckAdditionThenMultiplication()
    {
        _calculator.EventHandler("8");
        _calculator.EventHandler("+");
        _calculator.EventHandler("2");
        _calculator.EventHandler("*");
        _calculator.EventHandler("3");
        _calculator.EventHandler("=");
        if (Math.Abs(_calculator.CurrentValue - 30) > TOLERANCE)
        {
            throw new AssertionException($"Expected 30, but got {_calculator.CurrentValue}");
        }
    }

    [Test]
    public void CheckDivisionBy0()
    {
        _calculator.EventHandler("8");
        _calculator.EventHandler("/");
        _calculator.EventHandler("0");
        Assert.Throws<DivideByZeroException>(() => _calculator.EventHandler("="));
    }

    [Test]
    public void CheckTwoPoints()
    {
        _calculator.EventHandler("1");
        _calculator.EventHandler("1");
        _calculator.EventHandler(".");
        _calculator.EventHandler("1");
        Assert.Throws<InvalidOperationException>(() => _calculator.EventHandler("."));
    }

    [Test]
    public void CheckingNonIntegerCalculations()
    {
        _calculator.EventHandler("1");
        _calculator.EventHandler("1");
        _calculator.EventHandler(".");
        _calculator.EventHandler("1");
        _calculator.EventHandler("1");
        _calculator.EventHandler("+");
        _calculator.EventHandler("1");
        _calculator.EventHandler("=");
        if (_calculator.CurrentValue != 12.11)
        {
            throw new AssertionException($"Expected 12.11, but got {_calculator.CurrentValue}");
        }
    }

    [Test]
    public void CheckIncorrectInput()
    {
        Assert.Throws<ArgumentException>(() => _calculator.EventHandler("a"));
    }
}