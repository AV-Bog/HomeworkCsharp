// <copyright file="CCR.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace HW3.LZW;

using System.IO;

/// <summary>
/// Provides methods for calculating compression ratios.
/// </summary>
public class CCR
{
    /// <summary>
    /// Calculates the compression ratio between original and compressed files.
    /// </summary>
    /// <param name="inputFilePath">Path to the original input file.</param>
    /// <param name="outputFilePath">Path to the compressed output file.</param>
    /// <returns>Compression ratio as a double value.</returns>
    public static double CalculateCompressionRatio(string inputFilePath, string outputFilePath)
    {
        long originalSize = new FileInfo(inputFilePath).Length;
        long compressedSize = new FileInfo(outputFilePath).Length;
        return (double)originalSize / compressedSize;
    }
}