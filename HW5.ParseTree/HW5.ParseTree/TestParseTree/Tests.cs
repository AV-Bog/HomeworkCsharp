using HW5.ParseTree;
using NUnit.Framework;

namespace TestParseTree
{
    [TestFixture]
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

    [TestFixture]
    public class OperationNodeTests
    {
        [Test]
        public void AddNode_Evaluate_ReturnsSum()
        {
            var node = new AddNode(new NumberNode(2), new NumberNode(3));
            Assert.Equals(5, node.Evaluate());
        }

        [Test]
        public void MultiplyNode_Print_ReturnsCorrectString()
        {
            var node = new MultiplyNode(new NumberNode(2), new NumberNode(3));
            Assert.Equals("(* 2 3)", node.Print());
        }
    }

    [TestFixture]
    public class ExpressionEvaluatorTests
    {
        [Test]
        public void Evaluate_ValidExpression_ReturnsCorrectResult()
        {
            var node = Node.Parse("(+ (* 2 3) (/ 10 2))");
            Assert.Equals(11, ExpressionEvaluator.Evaluate(node));
        }

        [Test]
        public void Evaluate_DivideByZero_ThrowsException()
        {
            var node = Node.Parse("(/ 5 0)");
            Assert.Throws<ExpressionEvaluationException>(() => ExpressionEvaluator.Evaluate(node));
        }
    }

    [TestFixture]
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
            Assert.Equals(true, addNode != null);
            Assert.Equals(true, addNode!._left is MultiplyNode);
            Assert.Equals(true, addNode._right is DivideNode);
        }
    }
}
