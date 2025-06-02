// <copyright file="CalculatorEngine.cs" author="AV-Bog">
// under MIT License
// Free use, modification, and distribution are permitted,
// provided that the attribution and license notice are preserved.
// more detailed: https://github.com/AV-Bog/HomeworkCsharp/blob/main/LICENSE
// </copyright>

namespace HW72.Calculator;

using System.Globalization;

/// <summary>
/// Represents the core logic of a calculator engine.
/// </summary>
public class CalculatorEngine
{
    private double currentValue;
    private double previousValue;
    private string currentOperator;
    private bool isNewNumber;
    private bool hasStoredValue;
    private bool hasDot;
    private int decimalPlaces;

    /// <summary>
    /// Gets the current output string representation of the calculator state.
    /// </summary>
    public string Output
    {
        get
        {
            if (string.IsNullOrEmpty(this.currentOperator) && this.hasStoredValue)
            {
                return this.currentValue.ToString(CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrEmpty(this.currentOperator) && this.isNewNumber)
            {
                return $"{this.currentValue.ToString(CultureInfo.InvariantCulture)} {this.currentOperator}";
            }

            if (!string.IsNullOrEmpty(this.currentOperator))
            {
                return
                    $"{this.previousValue.ToString(CultureInfo.InvariantCulture)} {this.currentOperator} {this.currentValue.ToString(CultureInfo.InvariantCulture)}";
            }

            return this.currentValue.ToString(CultureInfo.InvariantCulture);
        }
    }

    /// <summary>
    /// Gets the current value stored in the calculator.
    /// </summary>
    public double CurrentValue => this.currentValue;

    /// <summary>
    /// Handles input events for the calculator.
    /// </summary>
    /// <param name="input">The input string representing a digit, operator, or command.</param>
    /// <exception cref="ArgumentException">Thrown when the input is invalid.</exception>
    public void EventHandler(string input)
    {
        switch (input)
        {
            case "C": this.Clear(); break;
            case "=": this.Calculate(); break;
            case "+" or "-" or "/" or "*": this.OperatorProses(input); break;
            case ".": this.AddDot(); break;
            default:
            {
                if (input.Length == 1 && char.IsDigit(input[0]))
                {
                    int digit = int.Parse(input);
                    this.NumberProses(digit);
                }
                else
                {
                    throw new ArgumentException(
                        $"Недопустимый ввод: '{input}'. Ожидалась цифра 0-9, точка или операция");
                }

                break;
            }
        }
    }

    private void NumberProses(int digit)
    {
        if (this.isNewNumber)
        {
            this.previousValue = this.currentValue;
            this.currentValue = digit;
            this.isNewNumber = false;
            this.hasStoredValue = true;
            this.hasDot = false;
            this.decimalPlaces = 0;
        }
        else
        {
            if (this.hasDot)
            {
                this.currentValue += digit / Math.Pow(10, this.decimalPlaces + 1);
                this.decimalPlaces++;
            }
            else
            {
                this.currentValue = (this.currentValue * 10) + digit;
            }
        }
    }

    private void OperatorProses(string curOperator)
    {
        if (this.hasStoredValue && !this.isNewNumber)
        {
            this.Calculate();
        }

        this.currentOperator = curOperator;
        this.isNewNumber = true;
    }

    private void Calculate()
    {
        if (this.hasStoredValue && !string.IsNullOrEmpty(this.currentOperator))
        {
            switch (this.currentOperator)
            {
                case "+":
                    this.currentValue = this.previousValue + this.currentValue;
                    break;
                case "-":
                    this.currentValue = this.previousValue - this.currentValue;
                    break;
                case "*":
                    this.currentValue = this.previousValue * this.currentValue;
                    break;
                case "/":
                    if (this.currentValue == 0)
                    {
                        throw new DivideByZeroException("Деление на 0 не разрешено");
                    }

                    this.currentValue = this.previousValue / this.currentValue;
                    break;
            }

            this.previousValue = this.currentValue;
        }

        this.currentOperator = "";
        this.isNewNumber = true;
        this.hasStoredValue = true;
    }

    private void AddDot()
    {
        if (this.hasDot)
        {
            throw new InvalidOperationException("2 точки не могут быть введены");
        }

        this.hasDot = true;
        this.decimalPlaces = 0;
    }

    private void Clear()
    {
        this.currentValue = 0;
        this.previousValue = 0;
        this.currentOperator = "";
        this.isNewNumber = false;
        this.hasStoredValue = false;
        this.hasDot = false;
        this.decimalPlaces = 0;
    }
}