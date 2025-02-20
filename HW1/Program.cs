using System.Text;
if (!tests())
{
    return -1;
}
Console.Write("Enter the line: ");
string inputStr = Console.ReadLine();

var resultDirectBWT = DirectBWT(inputStr);
string outputStr = resultDirectBWT.Item1;
int outputNum = resultDirectBWT.Item2;

Console.WriteLine("The result after the direct transformation of Burrows-Wheeler: {0}, {1}", outputStr, outputNum);

string resultReverseBWT = ReverseBWT(outputStr, outputNum);
Console.WriteLine("The result after the reverse transformation of Burrows-Wheeler: {0}", resultReverseBWT);

return 0;

static (string, int) DirectBWT(string inputStr)
{
    int length = inputStr.Length;
    string[] stringArray = new string[length];
    stringArray[0] = inputStr;
    for (int i = 1; i < length; i++)
    {
        stringArray[i] = string.Concat(stringArray[i-1].Substring(1), stringArray[i-1][0]);
    }
    Array.Sort(stringArray, StringComparer.OrdinalIgnoreCase);

    string outputStr = "";
    int outputNum = 0;
    for (int i = 0; i < length; i++)
    {
        outputStr += stringArray[i][length - 1];
        if (inputStr == stringArray[i])
        {
            outputNum = i;
        }
    }
    return (outputStr, outputNum);
}

static string ReverseBWT(string s, int k)
{
    int length = s.Length;
    
    var count = new int[256];
    foreach (char c in s)
    {
        count[c]++;
    }
    
    int[] position = new int[256];
    int total = 0;
    for (int i = 0; i < 256; i++)
    {
        total += count[i];
        position[i] = total - count[i];
    }
    
    var firstColumn = new char[length];
    var t = new int[length];
    for (int i = 0; i < length; i++)
    {
        char c = s[i];
        firstColumn[position[c]] = c;
        t[position[c]] = i;
        position[c]++;
    }
    
    StringBuilder result = new StringBuilder(length);
    int j = k;
    for (int i = 0; i < length; i++)
    {
        result.Append(firstColumn[j]);
        j = t[j];
    }

    return result.ToString();
}

static bool tests()
{
    string strTest0 = "abracadabra";
    var resDir0 = DirectBWT(strTest0);
    if (resDir0.Item1 != "rdarcaaaabb" || resDir0.Item2 != 2)
    {
        Console.WriteLine("the direct conversion test failed on the standard example :(");
        return false;
    }

    if (ReverseBWT(resDir0.Item1, resDir0.Item2) != strTest0)
    {
        Console.WriteLine("the reverse conversion test failed on the standard example :(");
        return false;
    }

    string strTest1 = "AsdAsd";
    var resDir1 = DirectBWT(strTest1);
    if (resDir1.Item1 != "ddssAA" || resDir1.Item2 != 1)
    {
        Console.WriteLine("the direct conversion test failed on the example with capital letters :(");
        return false;
    }

    if (ReverseBWT(resDir1.Item1, resDir1.Item2) != strTest1)
    {
        Console.WriteLine("the reverse conversion test failed on the example with capital letters :(");
        return false;
    }

    string strTest2 = "123";
    var resDir2 = DirectBWT(strTest2);
    if (resDir2.Item1 != "312" || resDir2.Item2 != 0)
    {
        Console.WriteLine("the direct conversion test failed in the example with numbers :(");
        return false;
    }
    if (ReverseBWT(resDir2.Item1, resDir2.Item2) != strTest2)
    {
        Console.WriteLine("the reverse conversion test failed on the example with numbers :(");
        return false;
    }

    return true;
}