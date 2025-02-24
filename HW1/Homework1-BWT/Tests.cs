namespace Homework1_BWT;

public abstract class Tests
{
    public static bool RunTests()
    {
        return TestsBwt();
    }
    
    private static bool TestsBwt()
    {
        string strTest0 = "abracadabra";
        var resDir0 = Bwt.Direct(strTest0);
        if (resDir0.Item1 != "rdarcaaaabb" || resDir0.Item2 != 2)
        {
            Console.WriteLine("the direct conversion test failed on the standard example :(");
            return false;
        }

        if (Bwt.Reverse(resDir0.Item1, resDir0.Item2) != strTest0)
        {
            Console.WriteLine("the reverse conversion test failed on the standard example :(");
            return false;
        }

        string strTest1 = "AsdAsd";
        var resDir1 = Bwt.Direct(strTest1);
        if (resDir1.Item1 != "ddssAA" || resDir1.Item2 != 1)
        {
            Console.WriteLine("the direct conversion test failed on the example with capital letters :(");
            return false;
        }

        if (Bwt.Reverse(resDir1.Item1, resDir1.Item2) != strTest1)
        {
            Console.WriteLine("the reverse conversion test failed on the example with capital letters :(");
            return false;
        }

        string strTest2 = "123";
        var resDir2 = Bwt.Direct(strTest2);
        if (resDir2.Item1 != "312" || resDir2.Item2 != 0)
        {
            Console.WriteLine("the direct conversion test failed in the example with numbers :(");
            return false;
        }

        if (Bwt.Reverse(resDir2.Item1, resDir2.Item2) != strTest2)
        {
            Console.WriteLine("the reverse conversion test failed on the example with numbers :(");
            return false;
        }

        return true;
    }
}
