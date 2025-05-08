// <copyright file="NumberNode.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace HW5.ParseTree;

/// <summary>
/// Represents a numeric constant node in a parse tree.
/// </summary>
public class NumberNode : Node
{
    private readonly int _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="NumberNode"/> class.
    /// </summary>
    /// <param name="value">The numeric value of the node.</param>
    public NumberNode(int value) => this._value = value;

    /// <inheritdoc cref="Node.Evaluate"/>
    public override int Evaluate() => this._value;

    /// <inheritdoc cref="Node.Print"/>
    public override string Print() => this._value.ToString();
}