// <copyright file="OperationNode.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace HW5.ParseTree;

/// <summary>
/// Abstract base class for operation nodes in a parse tree.
/// </summary>
public abstract class OperationNode : Node
{
    public readonly Node _left;
    public readonly Node _right;

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationNode"/> class.
    /// </summary>
    /// <param name="left">The left operand node.</param>
    /// <param name="right">The right operand node.</param>
    protected OperationNode(Node left, Node right)
    {
        this._left = left;
        this._right = right;
    }

    /// <summary>
    /// Creates an operation node from operator and operand expressions.
    /// </summary>
    /// <param name="op">The operator symbol (+, -, *, /).</param>
    /// <param name="leftExpr">The left operand expression.</param>
    /// <param name="rightExpr">The right operand expression.</param>
    /// <returns>The created operation node.</returns>
    /// <exception cref="FormatException">
    /// Thrown when the operator is unknown.
    /// </exception>
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
            _ => throw new FormatException($"Unknown operator: '{op}'"),
        };
    }

    /// <inheritdoc/>
    public override string Print() => $"({this.GetOperatorSymbol()} {this._left.Print()} {this._right.Print()})";

    /// <summary>
    /// Gets the symbol of the operation.
    /// </summary>
    /// <returns>The operator symbol.</returns>
    protected abstract string GetOperatorSymbol();
}

/// <summary>
/// Represents an addition operation node.
/// </summary>
public class AddNode(Node left, Node right) : OperationNode(left, right)
{
    /// <inheritdoc/>
    public override int Evaluate() => this._left.Evaluate() + this._right.Evaluate();

    /// <inheritdoc/>
    protected override string GetOperatorSymbol() => "+";
}

/// <summary>
/// Represents a subtraction operation node.
/// </summary>
public class SubtractNode(Node left, Node right) : OperationNode(left, right)
{
    /// <inheritdoc/>
    public override int Evaluate() => this._left.Evaluate() - this._right.Evaluate();

    /// <inheritdoc/>
    protected override string GetOperatorSymbol() => "-";
}

/// <summary>
/// Represents a multiplication operation node.
/// </summary>
public class MultiplyNode(Node left, Node right) : OperationNode(left, right)
{
    /// <inheritdoc/>
    public override int Evaluate() => this._left.Evaluate() * this._right.Evaluate();

    /// <inheritdoc/>
    protected override string GetOperatorSymbol() => "*";
}

/// <summary>
/// Represents a division operation node.
/// </summary>
public class DivideNode(Node left, Node right) : OperationNode(left, right)
{
    /// <inheritdoc/>
    public override int Evaluate()
    {
        if (this._right == null)
        {
            throw new DivideByZeroException();
        }

        return this._left.Evaluate() / this._right.Evaluate();
    }

    /// <inheritdoc/>
    protected override string GetOperatorSymbol() => "/";
}
