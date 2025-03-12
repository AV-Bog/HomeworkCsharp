namespace HW3.LZW;

public static class LZW
{
    public static List<int> Compress(string input)
    {
        var trie = new Trie();
        var output = new List<int>();
        string current = "";

        foreach (var c in input)
        {
            string comdined = current + c;
            if (trie.TryGetCode(comdined, out _))
            {
                current = comdined;
            }
            else
            {
                trie.TryGetCode(current, out int code);
                output.Add(code);
                trie.Add(comdined);
                current = c.ToString();
            }
        }

        if (!string.IsNullOrEmpty(current))
        {
            trie.TryGetCode(current, out int code);
            output.Add(code);
        }
        
        return output;
    }

    public static string Decompress(List<int> compressed)
    {
        var dictionary = new Dictionary<int, string>();
        for (int i = 0; i < 256; i++)
        {
            dictionary[i] = ((char)i).ToString();
        }
        
        string current = dictionary[compressed[0]];
        string output = current;
        int nextCode = 256;
        
        for (int i = 1; i < compressed.Count; i++)
        {
            int code = compressed[i];
            string entry; //хз как подругому
            if (dictionary.ContainsKey(code))
            {
                entry = dictionary[compressed[i]];
            }
            else if (code == nextCode)
            {
                entry = current + current[0];
            }
            else
            {
                throw new Exception("Invalid code lololololol");
            }

            output += entry;
            dictionary[nextCode++] = current + entry[0];
            current = entry;
        }
        return output;
    }
}