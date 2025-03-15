using System;
using System.Collections.Generic;
using System.Linq;

namespace HW3.LZW;
using System.IO;

public static class LZW
{
    public static void CompressFile(string inputFile, string outputFile)
    {
        using (var reader = new BinaryReader(File.Open(inputFile, FileMode.Open)))
        using (var writer = new BinaryWriter(File.Open(outputFile, FileMode.Create)))
        {
            var trie = new Trie();
            var compressed = new List<ushort>();
            long trieSize = 256; 
            const long maxTrieSize = 65536;
            
            byte[] buffer = new byte[8192]; // по 8КБ
            int bytesRead;

            while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
            {
                var compressedData = Compress(buffer.Take(bytesRead).ToArray(), ref trie, ref trieSize, maxTrieSize);
                compressed.AddRange(compressedData.Select(b => (ushort)b));
            }

            writer.Write((int)compressed.Count);
            foreach (ushort code in compressed)
            {
                writer.Write(code);
            }
        }        
           
    }
    private static List<int> Compress(byte[] input, ref Trie trie, ref long trieSize, long maxTrieSize)
    {
        var output = new List<int>();
        
        if (input.Length == 0)
        {
            return output; 
        }
        
        int start = 0;

        while (start < input.Length)
        {
            int end = start + 1;
            while (end <= input.Length && trie.TryGetCode(input.AsSpan(start, end - start).ToArray(), out _))
            {
                end++;
            }

            if (end > start + 1)
            {
                trie.TryGetCode(input.AsSpan(start, end - start - 1).ToArray(), out int code);
                output.Add(code);
            }
            else
            {
                trie.TryGetCode(input.AsSpan(start, end - start).ToArray(), out int code);
                output.Add(code);
            }

            if (end <= input.Length)
            {
                trie.Add(input.AsSpan(start, end - start).ToArray());
                trieSize += (end - start);
            }

            if (trieSize >= maxTrieSize)
            {
                trie = new Trie();
                trieSize = 256;
                output.Add(256); //значек сброса
            }
            start = end - 1;
        }
        
        return output;
    }

    public static void DecompressFile(string inputFile, string outputFile)
    {
        using (var reader = new BinaryReader(File.Open(inputFile, FileMode.Open)))
        {
            try
            {
                int count = reader.ReadInt32();

                var compressed = new List<int>();
                for (int i = 0; i < count; i++)
                {
                    if (reader.BaseStream.Position >= reader.BaseStream.Length)
                    {
                        throw new Exception("Invalid compressed data: unexpected end of file.");
                    }

                    compressed.Add(reader.ReadInt16());
                }

                byte[] original = Decompress(compressed);
                File.WriteAllBytes(outputFile, original);
            }
            catch (EndOfStreamException)
            {
                throw new Exception("Invalid compressed data: unexpected end of file.");
            }
        }
    }
    private static byte[] Decompress(List<int> compressed)
    {
        if (compressed == null || compressed.Count == 0)
        {
            return Array.Empty<byte>();
        }
        
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
            if (code == 256)
            {
                dictionary.Clear();
                for (int j = 0; j < 256; j++)
                {
                    dictionary[j] = new byte[] { (byte)j };
                }

                nextCode = 256;
                current = dictionary[compressed[++i]];
                output.AddRange(current);
                continue;
            }

            byte[] entry;

            if (dictionary.ContainsKey(code))
            {
                entry = dictionary[code];
            }
            else if (code == nextCode)
            {
                entry = new byte[current.Length + 1];
                Array.Copy(current, entry, current.Length);
                entry[current.Length] = current[0];
            }
            else
            {
                throw new Exception("Invalid code");
            }

            output.AddRange(entry);

            if (nextCode < 4096)
            {
                byte[] newEntry = new byte[current.Length + 1];
                Array.Copy(current, newEntry, current.Length);
                newEntry[current.Length] = entry[0];
                dictionary[nextCode++] = newEntry;
            }
            current = entry;
        }
        return output.ToArray();
    }
}