// <copyright file="CalculatorEngine.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace HW7._2.Calculator;

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
            if (string.IsNullOrEmpty(_currentOperator) && _hasStoredValue)
                return _currentValue.ToString();

            if (!string.IsNullOrEmpty(_currentOperator) && _isNewNumber)
                return $"{_currentValue.ToString()} {_currentOperator}";

            if (!string.IsNullOrEmpty(_currentOperator))
                return $"{_previousValue.ToString()} {_currentOperator} {_currentValue.ToString()}";

            return _currentValue.ToString();
        }
    }

    /// <summary>
    /// Gets the current value stored in the calculator.
    /// </summary>
    public double CurrentValue => _currentValue;

    /// <summary>
    /// Handles input events for the calculator.
    /// </summary>
    /// <param name="input">The input string representing a digit, operator, or command.</param>
    /// <exception cref="ArgumentException">Thrown when the input is invalid.</exception>
    public void EventHandler(string input)
    {
        switch (input)
        {
            case "C": Clear(); break;
            case "=": Calculate(); break;
            case "+" or "-" or "/" or "*": OperatorProses(input); break;
            case ".": AddDot(); break;
            default:
            {
                if (input.Length == 1 && char.IsDigit(input[0]))
                {
                    int digit = int.Parse(input);
                    NumberProses(digit);
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
        if (_isNewNumber)
        {
            _previousValue = _currentValue;
            _currentValue = digit;
            _isNewNumber = false;
            _hasStoredValue = true;
            _hasDot = false;
            _decimalPlaces = 0;
        }
        else
        {
            if (_hasDot)
            {
                _currentValue += digit / Math.Pow(10, _decimalPlaces + 1);
                _decimalPlaces++;
            }
            else
            {
                _currentValue = _currentValue * 10 + digit;
            }
        }
    }

    private void OperatorProses(string curOperator)
    {
        if (_hasStoredValue && !_isNewNumber)
        {
            Calculate();
        }

        _currentOperator = curOperator;
        _isNewNumber = true;
    }

    private void Calculate()
    {
        if (_hasStoredValue && !string.IsNullOrEmpty(_currentOperator))
        {
            switch (_currentOperator)
            {
                case "+":
                    _currentValue = _previousValue + _currentValue;
                    break;
                case "-":
                    _currentValue = _previousValue - _currentValue;
                    break;
                case "*":
                    _currentValue = _previousValue * _currentValue;
                    break;
                case "/":
                    if (_currentValue == 0)
                    {
                        throw new DivideByZeroException("Деление на 0 не разрешено");
                    }

                    _currentValue = _previousValue / _currentValue;
                    break;
            }

            _previousValue = _currentValue;
        }

        _currentOperator = "";
        _isNewNumber = true;
        _hasStoredValue = true;
    }

    private void AddDot()
    {
        if (_hasDot)
        {
            throw new InvalidOperationException("2 точки не могут быть введены");
        }

        _hasDot = true;
        _decimalPlaces = 0;
    }

    private void Clear()
    {
        _currentValue = 0;
        _previousValue = 0;
        _currentOperator = "";
        _isNewNumber = false;
        _hasStoredValue = false;
        _hasDot = false;
        _decimalPlaces = 0;
    }
}