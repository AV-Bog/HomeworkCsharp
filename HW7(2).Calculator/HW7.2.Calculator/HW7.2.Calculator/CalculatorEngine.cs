namespace HW7._2.Calculator;

public class CalculatorEngine
{
    private double _currentValue;
    private double _previousValue;
    private string _currentOperator;
    private bool _isNewNumber;
    private bool _hasStoredValue;
    private bool _hasDot;
    private int _decimalPlaces;

    public double CurrentValue => _currentValue;

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
                    throw new ArgumentException($"Недопустимый ввод: '{input}'. Ожидалась цифра 0-9, точка или операция");
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
                _currentValue+= digit / Math.Pow(10, _decimalPlaces + 1);
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