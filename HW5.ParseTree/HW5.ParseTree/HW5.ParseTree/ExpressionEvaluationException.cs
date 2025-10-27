// <copyright file="ExpressionEvaluationException.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace HW5.ParseTree;

/// <summary>
/// Evaluates the expression represented by the parse tree node.
/// </summary>
/// <returns>The result of the evaluated expression.</returns>
/// <exception cref="ExpressionEvaluationException">
/// Thrown when evaluation fails, such as division by zero.
/// </exception>
public class ExpressionEvaluationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionEvaluationException"/> class.
    /// Evaluates the expression represented by the parse tree node.
    /// </summary>
    /// <returns>The result of the evaluated expression.</returns>
    /// <exception cref="ExpressionEvaluationException">
    /// Thrown when evaluation fails, such as division by zero.
    /// </exception>
    public ExpressionEvaluationException(string message)
        : base(message)
    {
    }
}