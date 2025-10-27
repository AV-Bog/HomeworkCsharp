// <copyright file="ExpressionEvaluator.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace HW5.ParseTree;

/// <summary>
/// Provides functionality to evaluate mathematical expressions represented as parse trees.
/// </summary>
public class ExpressionEvaluator
{
    /// <summary>
    /// Evaluates the expression represented by the parse tree node.
    /// </summary>
    /// <param name="node">The root node of the parse tree to evaluate.</param>
    /// <returns>The result of the evaluated expression.</returns>
    /// <exception cref="ExpressionEvaluationException">
    /// Thrown when evaluation fails, such as division by zero.
    /// </exception>
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
