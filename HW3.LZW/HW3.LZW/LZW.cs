// <copyright file="LZW.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace HW3.LZW;

/// <summary>
/// Provides LZW compression and decompression functionality.
/// </summary>
public static class LZW
{
    /// <summary>
    /// Compresses a file using LZW algorithm with optional BWT transformation.
    /// </summary>
    /// <param name="inputFile">Path to the input file to compress.</param>
    /// <param name="outputFile">Path to the output compressed file.</param>
    /// <param name="useBwt">Whether to use Burrows-Wheeler Transform before compression.</param>
    public static void CompressFile(string inputFile, string outputFile, bool useBwt = false)
    {
        byte[] inputData = File.ReadAllBytes(inputFile);
        int bwtPosition = 0;

        if (useBwt)
        {
            var (bwtData, position) = BWT.Direct(inputData);
            inputData = bwtData;
            bwtPosition = position;
        }

        List<int> compressedData = Compress(inputData);

        using (var writer = new BinaryWriter(File.Open(outputFile, FileMode.Create)))
        {
            var bitWriter = new BitWriter(writer);

            writer.Write(useBwt);
            if (useBwt)
            {
                writer.Write(bwtPosition);
            }

            int maxCode = compressedData.DefaultIfEmpty(255).Max();
            int bitLength = Math.Max(9, (int)Math.Log(maxCode, 2) + 1);

            writer.Write(bitLength);
            writer.Write(compressedData.Count);

            foreach (int code in compressedData)
            {
                bitWriter.WriteBits(code, bitLength);
            }

            bitWriter.Flush();
        }
    }

    private static List<int> Compress(byte[] input)
    {
        var result = new List<int>();
        if (input.Length == 0)
        {
            return result;
        }

        var dictionary = new Dictionary<string, int>();
        for (int i = 0; i < 256; i++)
        {
            dictionary[((char)i).ToString()] = i;
        }

        string current = string.Empty;
        foreach (byte b in input)
        {
            string next = current + (char)b;

            if (dictionary.ContainsKey(next))
            {
                current = next;
            }
            else
            {
                result.Add(dictionary[current]);

                if (dictionary.Count < 4096)
                {
                    dictionary[next] = dictionary.Count;
                }

                current = ((char)b).ToString();
            }
        }

        if (!string.IsNullOrEmpty(current))
        {
            result.Add(dictionary[current]);
        }

        return result;
    }

    /// <summary>
    /// Decompresses a file compressed with LZW algorithm.
    /// </summary>
    /// <param name="inputFile">Path to the compressed input file.</param>
    /// <param name="outputFile">Path to the output decompressed file.</param>
    public static void DecompressFile(string inputFile, string outputFile)
    {
        List<int> compressedData;
        bool useBwt;

        int bwtPosition = 0;
        using (var reader = new BinaryReader(File.Open(inputFile, FileMode.Open)))
        {
            var bitReader = new BitReader(reader);
            useBwt = reader.ReadBoolean();
            if (useBwt)
            {
                bwtPosition = reader.ReadInt32();
            }

            int bitLength = reader.ReadInt32();
            int count = reader.ReadInt32();
            compressedData = new List<int>(count);

            for (int i = 0; i < count; i++)
            {
                compressedData.Add(bitReader.ReadBits(bitLength));
            }
        }

        byte[] decompressedData = Decompress(compressedData);

        if (useBwt)
        {
            decompressedData = BWT.Reverse(decompressedData, bwtPosition);
        }

        File.WriteAllBytes(outputFile, decompressedData);
    }

    private static byte[] Decompress(List<int> compressed)
    {
        if (compressed.Count == 0)
        {
            return Array.Empty<byte>();
        }

        var dictionary = new Dictionary<int, List<byte>>();
        for (int i = 0; i < 256; i++)
        {
            dictionary[i] = new List<byte> { (byte)i };
        }

        List<byte> previous = dictionary[compressed[0]];
        var output = new List<byte>(previous);

        for (int i = 1; i < compressed.Count; i++)
        {
            int code = compressed[i];
            List<byte> entry;

            if (dictionary.ContainsKey(code))
            {
                entry = new List<byte>(dictionary[code]);
            }
            else if (code == dictionary.Count)
            {
                entry = new List<byte>(previous);
                entry.Add(previous[0]);
            }
            else
            {
                throw new InvalidOperationException($"Invalid compressed data: code {code} not found");
            }

            output.AddRange(entry);

            List<byte> newEntry = new List<byte>(previous);
            newEntry.Add(entry[0]);
            dictionary[dictionary.Count] = newEntry;

            previous = entry;
        }

        return output.ToArray();
    }
}