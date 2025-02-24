using Homework1_BWT;
if (!Tests.RunTests())
{
    return -1;
}
Console.Write("Enter the line: ");
string? inputStr = Console.ReadLine();

while (string.IsNullOrWhiteSpace(inputStr))
{
    Console.WriteLine("Please enter a valid line.");
    inputStr = Console.ReadLine();
}

var resultDirectBwt = Bwt.Direct(inputStr);
string outputStr = resultDirectBwt.Item1;
int outputNum = resultDirectBwt.Item2;

Console.WriteLine("The result after the direct transformation of Burrows-Wheeler: {0}, {1}", outputStr, outputNum);

string resultReverseBwt = Bwt.Reverse(outputStr, outputNum);
Console.WriteLine("The result after the reverse transformation of Burrows-Wheeler: {0}", resultReverseBwt);


return 0;
