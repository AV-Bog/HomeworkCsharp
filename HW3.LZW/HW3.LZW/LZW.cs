using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace HW3.LZW
{
    public static class LZW
    {
        public static void CompressFile(string inputFile, string outputFile)
        {
            byte[] inputData = File.ReadAllBytes(inputFile);
            List<ushort> compressedData = Compress(inputData);

            using (var writer = new BinaryWriter(File.Open(outputFile, FileMode.Create)))
            {
                writer.Write(compressedData.Count);
                foreach (ushort code in compressedData)
                {
                    writer.Write(code);
                }
            }
        }

        private static List<ushort> Compress(byte[] input)
        {
            if (input.Length == 0)
            {
                return new List<ushort>();
            }

            var trie = new Trie();
            var current = new List<byte>();
            var output = new List<ushort>();

            foreach (byte b in input)
            {
                var next = new List<byte>(current) { b };
                byte[] nextBytes = next.ToArray();

                if (trie.TryGetCode(nextBytes, out _))
                {
                    current = next;
                }
                else
                {
                    byte[] currentBytes = current.ToArray();
                    if (trie.TryGetCode(currentBytes, out ushort currentCode))
                    {
                        output.Add(currentCode);
                    }

                    trie.Add(nextBytes);
                    current = new List<byte> { b };
                }
            }

            if (current.Count > 0)
            {
                byte[] currentBytes = current.ToArray();
                if (trie.TryGetCode(currentBytes, out ushort currentCode))
                {
                    output.Add(currentCode);
                }
            }

            return output;
        }

        public static void DecompressFile(string inputFile, string outputFile)
        {
            List<ushort> compressedData;
            using (var reader = new BinaryReader(File.Open(inputFile, FileMode.Open)))
            {
                int count = reader.ReadInt32(); // кол-во кодов
                compressedData = new List<ushort>();
                for (int i = 0; i < count; i++)
                {
                    compressedData.Add(reader.ReadUInt16());
                }
            }

            byte[] decompressedData = Decompress(compressedData);
            File.WriteAllBytes(outputFile, decompressedData);
        }

        private static byte[] Decompress(List<ushort> compressed)
        {
            if (compressed == null || compressed.Count == 0)
            {
                return Array.Empty<byte>();
            }

            var dictionary = new Dictionary<ushort, byte[]>();
            for (ushort i = 0; i < 256; i++)
            {
                dictionary[i] = new byte[] { (byte)i };
            }

            var current = dictionary[compressed[0]];
            var output = new List<byte>(current);

            for (int i = 1; i < compressed.Count; i++)
            {
                ushort code = compressed[i];
                byte[] entry;

                if (dictionary.ContainsKey(code))
                {
                    entry = dictionary[code];
                }
                else if (code == dictionary.Count)
                {
                    entry = current.Concat(new byte[] { current[0] }).ToArray();
                }
                else
                {
                    throw new InvalidOperationException("Invalid compressed data.");
                }

                output.AddRange(entry);
                dictionary[(ushort)dictionary.Count] = current.Concat(new byte[] { entry[0] }).ToArray();
                current = entry;
            }

            return output.ToArray();
        
        }
    }
}