namespace HW3.LZW;

using System.IO;
public class CCR
{
    public static double CalculateCompressionRatio(string inputFilePath, string outputFilePath)
    {
        
        long originalSize = new FileInfo(inputFilePath).Length;
        long compressedSize = new FileInfo(outputFilePath).Length;
        double compressionRatio = (double)originalSize / compressedSize;

        return compressionRatio;
    }
}