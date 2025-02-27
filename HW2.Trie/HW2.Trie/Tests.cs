namespace HW2.Trie;

public static class Tests
{
    public static bool TestsTrie()
    {
        return CheckFunctions();
    }
    
    private static bool CheckFunctions()
    {
        var trie = new Trie<int>();
        trie.Add("privet");
        trie.Add("prekrastno");
        trie.Add("mir");
        trie.Add("prok");
        trie.Add("mirnyi");
        trie.Add("mirk");
        

        if (!trie.Contains("prok"))
        {
            Console.WriteLine("Ошибка: не найдено имеющееся слово :(");
            return false;
        }
        trie.Add("prok");
        trie.Remove("mirk");
        if (trie.Contains("mirk"))
        {
            Console.WriteLine("Ошибка: найдено отсутствующее слово :(");
            return false;
        }

        if (trie.Size != 5)
        {
            Console.WriteLine("Ошибка: программа неверно считает количество уникальных слов :(");
            Console.WriteLine(trie.Size);
            return false;
        }
        
        return true;
    }
}