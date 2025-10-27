// <copyright file="NodeTests.cs" author="AV-Bog">
// under MIT License
// </copyright>

using HW5.ParseTree;

namespace TestParseTree;

public class NodeTests
{
    [Test]
    public void NumberNode_Evaluate_ReturnsCorrectValue()
    {
        var node = new NumberNode(5);
        Assert.Equals(5, node.Evaluate());
    }

    [Test]
    public void NumberNode_Print_ReturnsCorrectString()
    {
        var node = new NumberNode(5);
        Assert.Equals("5", node.Print());
    }
}