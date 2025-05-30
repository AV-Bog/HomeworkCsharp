// <copyright file="CalculatorEngine.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace HW7._2.Calculator;

using System.Globalization;

/// <summary>
/// Represents the core logic of a calculator engine.
/// </summary>
public class CalculatorEngine
{
    private double _currentValue;
    private double _previousValue;
    private string _currentOperator;
    private bool _isNewNumber;
    private bool _hasStoredValue;
    private bool _hasDot;
    private int _decimalPlaces;

    /// <summary>
    /// Gets the current output string representation of the calculator state.
    /// </summary>
    public string Output
    {
        get
        {
            if (string.IsNullOrEmpty(this._currentOperator) && this._hasStoredValue)
            {
                return this._currentValue.ToString(CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrEmpty(this._currentOperator) && this._isNewNumber)
            {
                return $"{this._currentValue.ToString(CultureInfo.InvariantCulture)} {this._currentOperator}";
            }

            if (!string.IsNullOrEmpty(this._currentOperator))
            {
                return $"{this._previousValue.ToString(CultureInfo.InvariantCulture)} {this._currentOperator} {this._currentValue.ToString()}";
            }

            return this._currentValue.ToString(CultureInfo.InvariantCulture);
        }
    }

    /// <summary>
    /// Gets the current value stored in the calculator.
    /// </summary>
    public double CurrentValue => this._currentValue;

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
        if (this._isNewNumber)
        {
            this._previousValue = this._currentValue;
            this._currentValue = digit;
            this._isNewNumber = false;
            this._hasStoredValue = true;
            this._hasDot = false;
            this._decimalPlaces = 0;
        }
        else
        {
            if (this._hasDot)
            {
                this._currentValue += digit / Math.Pow(10, this._decimalPlaces + 1);
                this._decimalPlaces++;
            }
            else
            {
                this._currentValue = (this._currentValue * 10) + digit;
            }
        }
    }

    private void OperatorProses(string curOperator)
    {
        if (this._hasStoredValue && !this._isNewNumber)
        {
            this.Calculate();
        }

        this._currentOperator = curOperator;
        this._isNewNumber = true;
    }

    private void Calculate()
    {
        if (this._hasStoredValue && !string.IsNullOrEmpty(this._currentOperator))
        {
            switch (this._currentOperator)
            {
                case "+":
                    this._currentValue = this._previousValue + this._currentValue;
                    break;
                case "-":
                    this._currentValue = this._previousValue - this._currentValue;
                    break;
                case "*":
                    this._currentValue = this._previousValue * this._currentValue;
                    break;
                case "/":
                    if (this._currentValue == 0)
                    {
                        throw new DivideByZeroException("Деление на 0 не разрешено");
                    }

                    this._currentValue = this._previousValue / this._currentValue;
                    break;
            }

            this._previousValue = this._currentValue;
        }

        this._currentOperator = "";
        this._isNewNumber = true;
        this._hasStoredValue = true;
    }

    private void AddDot()
    {
        if (this._hasDot)
        {
            throw new InvalidOperationException("2 точки не могут быть введены");
        }

        this._hasDot = true;
        this._decimalPlaces = 0;
    }

    private void Clear()
    {
        this._currentValue = 0;
        this._previousValue = 0;
        this._currentOperator = "";
        this._isNewNumber = false;
        this._hasStoredValue = false;
        this._hasDot = false;
        this._decimalPlaces = 0;
    }
}