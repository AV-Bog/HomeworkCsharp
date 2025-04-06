namespace HW7.Calculator;

public class CalculatorEngine
{
    private int _currentValue;
    private int _previousValue;
    private string _currentOperator;
    private bool _isNewNumber;
    private bool _hasStoredValue;
    
    public int CurrentValue => _currentValue;

    public void NumberProses(int digit)
    {
        if (_isNewNumber)
        {
            _previousValue = _currentValue;
            _currentValue = digit;
            _isNewNumber = false;
            _hasStoredValue = true;
        }
        else
        {
            _currentValue = _currentValue * 10 + digit;
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

    public int Calculate()
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
    
    public void Clear()
    {
        _currentValue = 0;
        _previousValue = 0;
        _currentOperator = "";
        _isNewNumber = false;
        _hasStoredValue = false;
    }
}
