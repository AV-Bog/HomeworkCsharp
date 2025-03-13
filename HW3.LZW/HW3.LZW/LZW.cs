namespace HW3.LZW;

public static class LZW
{
    public static void CompressFile(string inputFile, string outputFile)
    {
        byte[] input = File.ReadAllBytes(inputFile);
        var (bvtOutput, bwtPosition) = BWT.Direct(input);
        var compressed = Compress(bvtOutput);

        using (var writer = new BinaryWriter(File.Open(outputFile, FileMode.Create)))
        {
            writer.Write(bwtPosition);
            foreach (int code in compressed)
            {
                writer.Write(code);
            }
        }
    }
    private static List<int> Compress(byte[] input)
    {
        var trie = new Trie();
        var output = new List<int>();
        int start = 0;

        while (start < input.Length)
        {
            int end = start + 1;
            while (end <= input.Length && trie.TryGetCode(input[start..end], out _))
            {
                end++;
            }

            if (end > start + 1)
            {
                trie.TryGetCode(input[start..(end - 1)], out int code);
                output.Add(code);
            }
            else
            {
                trie.TryGetCode(input[start..end], out int code);
                output.Add(code);
            }

            if (end <= input.Length)
            {
                trie.Add(input[start..end]);
            }

            start = end - 1;
        }
        
        return output;
    }

    public static void DecompressFile(string inputFile, string outputFile)
    {
        using (var reader = new BinaryReader(File.Open(outputFile, FileMode.Open)))
        {
            int bwtPosition = reader.ReadInt32();
            var compressed = new List<int>();
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                compressed.Add(reader.ReadInt32());
            }
            byte[] bwtOutput = LZW.Decompress(compressed);
            byte[] original = BWT.Reverse(bwtOutput, bwtPosition);
            File.WriteAllBytes(outputFile, original);
        }
    }
    private static byte[] Decompress(List<int> compressed)
    {
        var dictionary = new Dictionary<int, byte[]>();
        for (int i = 0; i < 256; i++)
        {
            dictionary[i] = new byte[] { (byte)i };
        }
        
        byte[] current = dictionary[compressed[0]];
        var output = new List<byte>(current);
        int nextCode = 256;
        
        for (int i = 1; i < compressed.Count; i++)
        {
            int code = compressed[i];
            byte[] entry; //хз как подругому
            
            if (dictionary.ContainsKey(code))
            {
                entry = dictionary[code];
            }
            else if (code == nextCode)
            {
                entry = current.Concat(new byte[] { current[0] }).ToArray();
            }
            else
            {
                throw new Exception("Invalid code lololololol");
            }

            output.AddRange(entry);
            dictionary[nextCode++] = current.Concat(new byte[] { entry[0] }).ToArray();
            current = entry;
        }
        return output.ToArray();
    }
}