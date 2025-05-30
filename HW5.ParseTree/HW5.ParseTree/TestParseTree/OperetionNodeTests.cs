// <copyright file="OperetionNodeTests.cs" author="AV-Bog">
// under MIT License
// </copyright>

using HW5.ParseTree;

namespace TestParseTree;

public class OperetionNodeTests
{
    [Test]
    public void AddNode_Evaluate_ReturnsSum()
    {
        var node = new AddNode(new NumberNode(2), new NumberNode(3));
        Assert.That(node.Evaluate(), Is.EqualTo(5));
    }

    [Test]
    public void MultiplyNode_Print_ReturnsCorrectString()
    {
        var node = new MultiplyNode(new NumberNode(2), new NumberNode(3));
        Assert.That(node.Print(), Is.EqualTo("(* 2 3)"));
    }
}