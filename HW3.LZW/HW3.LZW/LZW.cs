using System;
using System.Collections.Generic;
using System.Linq;

namespace HW3.LZW;
using System.IO;

public static class LZW
{
    public static void CompressFile(string inputFile, string outputFile)
    {
        byte[] inputData = File.ReadAllBytes(inputFile);
        List<int> compressedData = Compress(inputData);

        using (var writer = new BinaryWriter(File.Open(outputFile, FileMode.Create)))
        {
            writer.Write(compressedData.Count);
            foreach (int code in compressedData)
            {
                writer.Write(code);
            }
        }      
           
    }
    private static List<int> Compress(byte[] input)
    {
        /*if (input.Length == 0)
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
        }*/

        var dictionary = new Dictionary<string, int>();
        for (int i = 0; i < 256; i++)
        {
            dictionary[((char)i).ToString()] = i;
        }

        string current = string.Empty;
        var output = new List<int>();

        foreach (byte b in input)
        {
            string next = current + (char)b;
            if (dictionary.ContainsKey(next))
            {
                current = next;
            }
            else
            {
                output.Add(dictionary[current]);
                dictionary[next] = dictionary.Count;
                current = ((char)b).ToString();
            }

            if (dictionary.Count >= 4096)
            {
                dictionary.Clear();
                for (int i = 0; i < 256; i++)
                {
                    dictionary[((char)i).ToString()] = i;
                }
            }
        }

        if (!string.IsNullOrEmpty(current))
        {
            output.Add(dictionary[current]);
        }

        return output;
    }

    public static void DecompressFile(string inputFile, string outputFile)
    {
        List<int> compressedData;
        using (var reader = new BinaryReader(File.Open(inputFile, FileMode.Open)))
        {
            int count = reader.ReadInt32(); // кол-во кодов
            compressedData = new List<int>();
            for (int i = 0; i < count; i++)
            {
                compressedData.Add(reader.ReadInt32());
            }
        }

        byte[] decompressedData = Decompress(compressedData);
        File.WriteAllBytes(outputFile, decompressedData);
        /*using (var reader = new BinaryReader(File.Open(inputFile, FileMode.Open)))
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

                    compressed.Add(reader.ReadInt32());
                }

                byte[] original = Decompress(compressed);
                
                int position = compressed.Last();
                byte[] reversedData = BWT.Reverse(original, position);
                
                File.WriteAllBytes(outputFile, reversedData);
            }
            catch (EndOfStreamException)
            {
                throw new Exception("Invalid compressed data: unexpected end of file.");
            }
        }*/
    }
    private static byte[] Decompress(List<int> compressed)
    {
        /*if (compressed == null || compressed.Count == 0)
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

        for (int i = 1; i < compressed.Count - 1; i++)
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
                /*current = dictionary[compressed[++i]];
                output.AddRange(current);#1#
                current = null;
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

            if (nextCode < 65536)
            {
                byte[] newEntry = new byte[current.Length + 1];
                Array.Copy(current, newEntry, current.Length);
                newEntry[current.Length] = entry[0];
                dictionary[nextCode++] = newEntry;
            }
            current = entry;
        }
        return output.ToArray();
    }*/
        var dictionary = new Dictionary<int, string>();
        for (int i = 0; i < 256; i++)
        {
            dictionary[i] = ((char)i).ToString();
        }

        string current = dictionary[compressed[0]];
        var output = new List<byte>(current.Select(c => (byte)c));

        for (int i = 1; i < compressed.Count; i++)
        {
            int code = compressed[i];
            string entry;

            if (dictionary.ContainsKey(code))
            {
                entry = dictionary[code];
            }
            else if (code == dictionary.Count)
            {
                entry = current + current[0];
            }
            else
            {
                throw new InvalidOperationException("Invalid compressed data.");
            }

            output.AddRange(entry.Select(c => (byte)c));
            dictionary[dictionary.Count] = current + entry[0];
            current = entry;

            if (dictionary.Count >= 4096)
            {
                dictionary.Clear();
                for (int j = 0; j < 256; j++)
                {
                    dictionary[j] = ((char)j).ToString();
                }
            }
        }

        return output.ToArray();
    }
}