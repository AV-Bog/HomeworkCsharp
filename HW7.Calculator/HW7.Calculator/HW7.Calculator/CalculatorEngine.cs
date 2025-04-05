namespace HW7.Calculator;

public class CalculatorEngine
{
    private int _currentValue;
    private int _previousValue;
    private string _currentOperator;
    private bool _isOperatorClicked;

    public void NumberProses(int digit)
    {
        if (_isOperatorClicked)
        {
            _currentValue = digit;
            _isOperatorClicked = false;
        }
        else
        {
            _currentValue = _currentValue * 10 + digit;
        }
    }
    public void OperatorProses(char curOperator)
    {
        if (!String.IsNullOrEmpty(_currentOperator))
        {
            Calculate();
        }

        _previousValue = _currentValue;
        _currentOperator = curOperator.ToString();
        _isOperatorClicked = true;
    }

    public int Calculate()
    {
        switch (_currentOperator)
        {
            case "+": _currentValue = _currentValue + _previousValue; break;
            case "-": _currentValue =  _currentValue - _previousValue; break;
            case "*": _currentValue = _currentValue * _previousValue; break;
            case "/": _currentValue = _currentValue / _previousValue; break;
        }
        _currentOperator = "";
        return _currentValue;
    }
    
    public void Clear()
    {
        _currentValue = 0;
        _previousValue = 0;
        _currentOperator = "";
        _isOperatorClicked = false;
    }
}
