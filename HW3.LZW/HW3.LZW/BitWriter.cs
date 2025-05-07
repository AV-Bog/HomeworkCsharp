// <copyright file="BitWriter.cs" author="AV-Bog">
// under MIT License
// </copyright>

/// <summary>
/// Provides bit-level writing capabilities to a binary stream
/// </summary>
public class BitWriter(BinaryWriter writer)
{
    private byte buffer;
    private int bitsInBuffer;

    /// <summary>
    /// Writes specified bits to the stream
    /// </summary>
    /// <param name="value">Integer value containing bits to write.</param>
    /// <param name="bitCount">Number of bits to write (1-32).</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when bitCount is outside valid range.</exception>
    public void WriteBits(int value, int bitCount)
    {
        if (bitCount < 1 || bitCount > 32)
        {
            throw new ArgumentOutOfRangeException(nameof(bitCount));
        }

        for (int i = bitCount - 1; i >= 0; i--)
        {
            this.buffer = (byte)((this.buffer << 1) | ((value >> i) & 1));
            this.bitsInBuffer++;

            if (this.bitsInBuffer == 8)
            {
                writer.Write(this.buffer);
                this.buffer = 0;
                this.bitsInBuffer = 0;
            }
        }
    }

    /// <summary>
    /// Flushes any remaining bits in the buffer to the stream
    /// </summary>
    public void Flush()
    {
        if (this.bitsInBuffer > 0)
        {
            this.buffer <<= 8 - this.bitsInBuffer;
            writer.Write(this.buffer);
            this.buffer = 0;
            this.bitsInBuffer = 0;
        }
    }
}
