namespace HW7.Calculator;

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

    public void NumberProses(double digit)
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
    
    public void OperatorProses(char curOperator)
    {
        if (_hasStoredValue && !_isNewNumber)
        {
            Calculate();
        }
        _currentOperator = curOperator.ToString();
        _isNewNumber = true;
    }

    public double Calculate()
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
                        throw new DivideByZeroException("Дурак?");
                    }
                    _currentValue = _previousValue / _currentValue;
                    break;
            }
            _previousValue = _currentValue;
        }
        
        _currentOperator = "";
        _isNewNumber = true;
        _hasStoredValue = true;
        return _currentValue;
    }

    public void AddDot()
    {
        if (_hasDot)
        {
            throw new InvalidOperationException("2 точки? Серьезно?");
        }
        _hasDot = true;
        _decimalPlaces = 0;
    }
    
    public void Clear()
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
