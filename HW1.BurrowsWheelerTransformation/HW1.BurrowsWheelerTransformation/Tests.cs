namespace HW1.BurrowsWheelerTransformation;

public static class Tests
{
    public static bool RunTests()
    {
        return TestsBwt();
    }
    
    private static bool TestsBwt()
    {
        string strTest0 = "abracadabra";
        var resDir0 = BWT.Direct(strTest0);
        if (resDir0.outputStr != "rdarcaaaabb" || resDir0.position != 2)
        {
            Console.WriteLine("the direct conversion test failed on the standard example :(");
            return false;
        }

        if (BWT.Reverse(resDir0.outputStr, resDir0.position) != strTest0)
        {
            Console.WriteLine("the reverse conversion test failed on the standard example :(");
            return false;
        }

        string strTest1 = "AsdAsd";
        var resDir1 = BWT.Direct(strTest1);
        if (resDir1.outputStr != "ddssAA" || resDir1.position != 1)
        {
            Console.WriteLine("the direct conversion test failed on the example with capital letters :(");
            return false;
        }

        if (BWT.Reverse(resDir1.outputStr, resDir1.position) != strTest1)
        {
            Console.WriteLine("the reverse conversion test failed on the example with capital letters :(");
            return false;
        }

        string strTest2 = "123";
        var resDir2 = BWT.Direct(strTest2);
        if (resDir2.outputStr != "312" || resDir2.position != 0)
        {
            Console.WriteLine("the direct conversion test failed in the example with numbers :(");
            return false;
        }

        if (BWT.Reverse(resDir2.outputStr, resDir2.position) != strTest2)
        {
            Console.WriteLine("the reverse conversion test failed on the example with numbers :(");
            return false;
        }

        return true;
    }
}