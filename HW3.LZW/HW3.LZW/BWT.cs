namespace HW3.LZW;

using System.Text;

public static class BWT
{
    public static (byte[] output, int position) Direct(byte[] input)
    {
        int length = input.Length;
        byte[][] rotation = new byte[length][];
        for (int i = 0; i < length; i++)
        {
            rotation[i] = new byte[length];
            for (int j = 0; j < length; j++)
            {
                rotation[i][j] = input[(i + j) % length];
            }
        }

        Array.Sort(rotation, (a, b) =>
        {
            for (int i = 0; i < length; i++)
            {
                if (a[i] < b[i]) return -1;
                if (a[i] > b[i]) return 1;
            }

            return 0;
        });
    
        byte[] output = new byte[length];
        int position = 0;
        for (int i = 0; i < length; i++)
        {
            output[i] = rotation[i][length - 1];
            if (rotation[i].SequenceEqual(input))
            {
                position = i;
            }
        }
        return (output, position);
    }
    
    public static byte[] Reverse(byte[] input, int position)
    {
        int length = input.Length;
        
        var count = new int[256];
        var sum = new int[256];
        var next = new int[length];
        
        foreach (char b in input)
        {
            count[b]++;
        }
        
        for (int i = 0; i < 256; i++)
        {
            sum[i] = sum[i - 1] - count[i - 1];
        }
        
        for (int i = 0; i < length; i++)
        {
            byte b = input[i];
            next[sum[b]] = i;
            sum[b]++;
        }
        
        var result = new byte[length];
        int current = position;
        for (int i = 0; i < length; i++)
        {
            result[i] = input[current];
            current = next[current];
        }
    
        return result;
    }
}