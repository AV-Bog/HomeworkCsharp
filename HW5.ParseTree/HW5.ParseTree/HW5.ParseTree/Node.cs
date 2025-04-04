namespace HW5.ParseTree;

public abstract class Node
{
    public abstract int Evaluate();
    public abstract string Print();

    public static Node Parse(string expression)
    {
        expression = expression.Trim();

        if (expression.StartsWith("(") && expression.EndsWith(")"))
        {
            expression = expression.Substring(1, expression.Length - 2).Trim();
            
            int spaceIndex = expression.IndexOf(' ');
            if (spaceIndex == -1)
            {
                throw new FormatException("Invalid expression format");
            }
            
            string op = expression.Substring(0, spaceIndex);
            string rest = expression.Substring(spaceIndex + 1).Trim();

            var operands = SplitOperands(rest);
            
            return OperationNode.Create(op, operands.left, operands.right);
        }

        if (int.TryParse(expression, out int result))
        {
            return new NumberNode(result);
        }
        
        throw new FormatException("Unknown expression format");
    }

    private static (string left, string right) SplitOperands(string operands)
    {
        operands = operands.Trim();
        int brackets = 0;
        int splitPos = -1;

        for (int i = 0; i < operands.Length; i++)
        {
            brackets += operands[i] == '(' ? 1 : operands[i] == ')' ? -1 : 0;

            if (brackets == 0 && operands[i] == ' ')
            {
                splitPos = i;
                break;
            }
        }
        
        return (operands[..splitPos].Trim(), operands[splitPos..].Trim());
    }
}