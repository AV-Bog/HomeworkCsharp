namespace HW5.ParseTree;

public class NumberNode : Node
{
    private readonly int _value;
    
    public NumberNode(int value) => _value = value;
    
    public override int Evaluate() => _value;
    public override string Print() => _value.ToString();
}