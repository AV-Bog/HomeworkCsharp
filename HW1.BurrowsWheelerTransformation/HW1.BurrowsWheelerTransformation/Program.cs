using HW1.BurrowsWheelerTransformation;

if (!Tests.RunTests())
{
    return -1;
}

Console.Write("Enter the line: ");
string inputStr = Console.ReadLine();

var resultDirectBWT = BWT.Direct(inputStr);
string outputStr = resultDirectBWT.outputStr;
int outputNum = resultDirectBWT.position;

Console.WriteLine("The result after the direct transformation of Burrows-Wheeler: {0}, {1}", outputStr, outputNum);

string resultReverseBWT = BWT.Reverse(outputStr, outputNum);
Console.WriteLine("The result after the reverse transformation of Burrows-Wheeler: {0}", resultReverseBWT);

return 0;