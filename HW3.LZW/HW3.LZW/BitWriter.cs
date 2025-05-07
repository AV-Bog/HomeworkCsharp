public class BitWriter
{
    private readonly BinaryWriter writer;
    private byte buffer;
    private int bitsInBuffer;

    public BitWriter(BinaryWriter writer) => this.writer = writer;

    public void WriteBits(int value, int bitCount)
    {
        if (bitCount < 1 || bitCount > 32)
            throw new ArgumentOutOfRangeException(nameof(bitCount));

        for (int i = bitCount - 1; i >= 0; i--)
        {
            buffer = (byte)((buffer << 1) | ((value >> i) & 1));
            bitsInBuffer++;

            if (bitsInBuffer == 8)
            {
                writer.Write(buffer);
                buffer = 0;
                bitsInBuffer = 0;
            }
        }
    }

    public void Flush()
    {
        if (bitsInBuffer > 0)
        {
            buffer <<= (8 - bitsInBuffer);
            writer.Write(buffer);
            buffer = 0;
            bitsInBuffer = 0;
        }
    }
}