// <copyright file="BitReader.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace DefaultNamespace;

public class BitReader
{
    private readonly BinaryReader reader;
    private byte buffer;
    private int bitsInBuffer;

    public BitReader(BinaryReader reader) => this.reader = reader;

    public int ReadBits(int bitCount)
    {
        if (bitCount < 1 || bitCount > 32)
            throw new ArgumentOutOfRangeException(nameof(bitCount));

        int result = 0;
        for (int i = 0; i < bitCount; i++)
        {
            if (bitsInBuffer == 0)
            {
                if (reader.BaseStream.Position >= reader.BaseStream.Length)
                    throw new EndOfStreamException();
                
                buffer = reader.ReadByte();
                bitsInBuffer = 8;
            }

            result = (result << 1) | ((buffer >> (bitsInBuffer - 1)) & 1);
            bitsInBuffer--;
        }
        return result;
    }
}
