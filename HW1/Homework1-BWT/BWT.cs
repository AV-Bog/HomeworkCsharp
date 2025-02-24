namespace Homework1_BWT;

using System.Text;

public abstract class Bwt
{
    public static (string, int) Direct(string inputStr)
    {
        int length = inputStr.Length;
        string[] stringArray = new string [length];
        stringArray[0] = inputStr;
        for (int i = 1; i < length; i++)
        {
            stringArray[i] = string.Concat(stringArray[i - 1][1..], stringArray[i - 1][0]);
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
    
    public static string Reverse(string s, int k)
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
        
        StringBuilder result = new(length);
        int j = k;
        for (int i = 0; i < length; i++)
        {
            result.Append(firstColumn[j]);
            j = t[j];
        }
    
        return result.ToString();
    }
}
