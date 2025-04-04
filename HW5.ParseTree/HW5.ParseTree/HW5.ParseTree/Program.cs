// See https://aka.ms/new-console-template for more information

using HW5.ParseTree;

if (args.Length == 0)
{
    Console.WriteLine("Please provide the input file path as an argument.");
    return;
}

string filePath = args[0];
try
{
    string expression = File.ReadAllText(filePath).Trim();
    Console.WriteLine($"Input expression: {expression}");

    Node tree = Node.Parse(expression);
    Console.WriteLine($"Parsed tree: {tree.Print()}");

    int result = ExpressionEvaluator.Evaluate(tree);
    Console.WriteLine($"Result: {result}");
}
catch (FileNotFoundException)
{
    Console.WriteLine($"Error: File not found - {filePath}");
}
catch (FormatException ex)
{
    Console.WriteLine($"Error: Invalid expression format - {ex.Message}");
}
catch (ExpressionEvaluationException ex)
{
    Console.WriteLine($"Error: Evaluation error - {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected error: {ex.Message}");
}