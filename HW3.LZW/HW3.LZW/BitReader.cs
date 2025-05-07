// <copyright file="BitReader.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace HW3.LZW;

/// <summary>
/// Provides bit-level reading capabilities from a binary stream
/// </summary>
public class BitReader(BinaryReader reader)
{
    private byte buffer;
    private int bitsInBuffer;

    /// <summary>
    /// Reads a specified number of bits from the stream
    /// </summary>
    /// <param name="bitCount">Number of bits to read (1-32).</param>
    /// <returns>The bits read as an integer.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when bitCount is outside valid range.</exception>
    /// <exception cref="EndOfStreamException">Thrown when attempting to read past the end of stream.</exception>
    public int ReadBits(int bitCount)
    {
        if (bitCount < 1 || bitCount > 32)
        {
            throw new ArgumentOutOfRangeException(nameof(bitCount));
        }

        int result = 0;
        for (int i = 0; i < bitCount; i++)
        {
            if (this.bitsInBuffer == 0)
            {
                if (reader.BaseStream.Position >= reader.BaseStream.Length)
                {
                    throw new EndOfStreamException();
                }

                this.buffer = reader.ReadByte();
                this.bitsInBuffer = 8;
            }

            result = (result << 1) | ((this.buffer >> (this.bitsInBuffer - 1)) & 1);
            this.bitsInBuffer--;
        }

        return result;
    }
}