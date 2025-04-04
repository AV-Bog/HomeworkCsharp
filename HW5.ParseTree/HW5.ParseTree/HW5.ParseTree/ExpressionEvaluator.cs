namespace HW5.ParseTree;

public class ExpressionEvaluator
{
    public static int Evaluate(Node node)
    {
        try
        {
            return node.Evaluate();
        }
        catch (DivideByZeroException)
        {
            throw new ExpressionEvaluationException("Division by zero");
        }
    }
}

public class ExpressionEvaluationException : Exception
{
    public ExpressionEvaluationException(string message) : base(message) {}
}