// <copyright file="Node.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace HW5.ParseTree;

/// <summary>
/// Abstract base class representing a node in a parse tree.
/// </summary>
public abstract class Node
{
    /// <summary>
    /// Evaluates the node's value.
    /// </summary>
    /// <returns>The result of the evaluation.</returns>
    public abstract int Evaluate();

    /// <summary>
    /// Returns a string representation of the node.
    /// </summary>
    /// <returns>The string representation of the node.</returns>
    public abstract string Print();

    /// <summary>
    /// Parses a string expression into a parse tree.
    /// </summary>
    /// <param name="expression">The string expression to parse.</param>
    /// <returns>The root node of the parse tree.</returns>
    /// <exception cref="FormatException">
    /// Thrown when the expression format is invalid.
    /// </exception>
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

            return OperationNode.Create(op, operands.Left, operands.Right);
        }

        if (int.TryParse(expression, out int result))
        {
            return new NumberNode(result);
        }

        throw new FormatException("Unknown expression format");
    }

    private static (string Left, string Right) SplitOperands(string operands)
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
