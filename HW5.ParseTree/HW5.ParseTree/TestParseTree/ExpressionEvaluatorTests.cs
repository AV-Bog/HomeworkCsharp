// <copyright file="ExpressionEvaluatorTests.cs" author="AV-Bog">
// under MIT License
// </copyright>

using HW5.ParseTree;

namespace TestParseTree;

public class ExpressionEvaluatorTests
{
    [Test]
    public void Evaluate_ValidExpression_ReturnsCorrectResult()
    {
        var node = Node.Parse("(+ (* 2 3) (/ 10 2))");
        Assert.That(11, Is.EqualTo(ExpressionEvaluator.Evaluate(node)));
    }

    [Test]
    public void Evaluate_DivideByZero_ThrowsException()
    {
        var node = Node.Parse("(/ 5 0)");
        Assert.Throws<ExpressionEvaluationException>(() => ExpressionEvaluator.Evaluate(node));
    }
}
