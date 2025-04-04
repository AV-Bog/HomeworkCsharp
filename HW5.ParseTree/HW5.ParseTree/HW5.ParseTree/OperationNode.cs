namespace HW5.ParseTree;

public abstract class OperationNode : Node
{
    public readonly Node _left;
    public readonly Node _right;
    
    protected OperationNode(Node left, Node right)
    {
        _left = left;
        _right = right;
    }

    public static Node Create(string op, string leftExpr, string rightExpr)
    {
        Node left = Parse(leftExpr);
        Node right = Parse(rightExpr);
        
        return op switch
        {
            "+" => new AddNode(left, right),
            "-" => new SubtractNode(left, right),
            "*" => new MultiplyNode(left, right),
            "/" => new DivideNode(left, right),
            _ => throw new FormatException($"Unknown operator: '{op}'")
        };
    }
    public override string Print() => $"({GetOperatorSymbol()} {_left.Print()} {_right.Print()})";
    protected abstract string GetOperatorSymbol();
}

public class AddNode : OperationNode
{
    public AddNode(Node left, Node right) : base(left, right) {}
    
    public override int Evaluate() => _left.Evaluate() + _right.Evaluate();
    protected override string GetOperatorSymbol() => "+";
}

public class SubtractNode : OperationNode
{
    public SubtractNode(Node left, Node right) : base(left, right) {}
    
    public override int Evaluate() => _left.Evaluate() - _right.Evaluate();
    protected override string GetOperatorSymbol() => "-";
}

public class MultiplyNode : OperationNode
{
    public MultiplyNode(Node left, Node right) : base(left, right) {}
    
    public override int Evaluate() => _left.Evaluate() * _right.Evaluate();
    protected override string GetOperatorSymbol() => "*";
}

public class DivideNode : OperationNode
{
    public DivideNode(Node left, Node right) : base(left, right) {}

    public override int Evaluate()
    {
        if (_right == null)
        {
            throw new DivideByZeroException();
        }
        return _left.Evaluate() / _right.Evaluate();
    }
    protected override string GetOperatorSymbol() => "/";
}