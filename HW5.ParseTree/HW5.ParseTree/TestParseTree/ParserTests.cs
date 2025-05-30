// <copyright file="ParserTests.cs" author="AV-Bog">
// under MIT License
// </copyright>

using HW5.ParseTree;

namespace TestParseTree;

public class ParserTests
{
    [Test]
    public void Parse_InvalidExpression_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(() => Node.Parse("invalid"));
    }

    [Test]
    public void Parse_NestedExpression_CreatesCorrectTree()
    {
        var node = Node.Parse("(+ (* 2 3) (/ 10 2))");
        var addNode = node as AddNode;
        Assert.That(addNode, Is.Not.Null);
        Assert.That(addNode!._left, Is.InstanceOf<MultiplyNode>());
        Assert.That(addNode._right, Is.InstanceOf<DivideNode>());
    }
}
